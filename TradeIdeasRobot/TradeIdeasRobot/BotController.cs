using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using System.Xml.Serialization;
using System.Globalization;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Net;

using TWSLib;
using AxTWSLib;
using NLog;

namespace TradeIdeasRobot
{
    class TickReq
    {
        public TickReq(string symb, DateTime time, AutoTradeDefinition tradeDef) {
            this.symbol = symb;
            this.entryTime = time;
            this.tradeDef = tradeDef;
        }
        public string symbol;
        public DateTime entryTime;
        public AutoTradeDefinition tradeDef;

        public double Bid = -1;
        public double Ask = -1;
        public double Last = -1;
    }

    class StratOrders
    {
        public AutoTradeDefinition tradeDefn;
        public string symbol;
        public IOrder entry;
        public IOrder stoploss;
        public IOrder profitTarget;
        public IOrder timestop;
    }

    public class BotController
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private AxTWSLib.AxTws twsLive;
        private AxTWSLib.AxTws twsSim;
        private BotModel model;

        private bool useSocket = false;

        private Dictionary<string, int> portfolio;  // ticker symbol & number of shares

        // used for date time formatting
        private DateTimeFormatInfo dtfi = new CultureInfo("en-US", false).DateTimeFormat;

        public event EventHandler<ModelEventArgs> ModelUpdated;
        public event EventHandler<MessageEventArgs> OnMessage;

        private string simExecs, liveExecs;
        private StreamWriter executionsSim;
        private StreamWriter executionsLive;
        private StreamWriter alertLog;



        private List<TickReq> tickRequests;
        //private Dictionary<String, List<TickReq>> iqTickRequests;
        private OrderManager orderManager;

        public BotController(BotModel model) {
            this.model = model;
            portfolio = new Dictionary<string, int>();
            tickRequests = new List<TickReq>();
            orderManager = new OrderManager();

            string timestamp = DateTime.Now.ToString("yyyyMMdd-HHmm");

            // debugging
            simExecs = "";
            liveExecs = "";
            executionsSim = new StreamWriter("sim-" + timestamp + ".txt");
            executionsLive = new StreamWriter("live-" + timestamp + ".txt");
            alertLog = new StreamWriter("alerts.log");
            try {
                alertLog.WriteLine("WindowName,Symbol,AlertPrice");
                alertLog.Flush();
            } catch(Exception ex) {
                log.Error("Could not write to alert file: " + ex);
            }
        }

        public AxTws Tws {
            get { return twsLive; }
            set { twsLive = value; }
        }
        public AxTws TwsSim {
            get { return twsSim; }
            set { twsSim = value; }
        }

        //private void handleQuoteMessage(string quoteData) {
        //    string[] cells = System.Text.RegularExpressions.Regex.Split(quoteData, ",");
        //    string symbol = cells[1];
        //    lock (iqTickRequests) {
        //        if (!iqTickRequests.ContainsKey(symbol)) {
        //            if (useSocket) {
        //            } else {
        //                iqFeed.RemoveSymbol(ref symbol);
        //            }
        //        } else {
        //        }
        //    }
        //}
        //private void handleIqSummaryMessage(string strSummaryData) {
        //    string[] cells = System.Text.RegularExpressions.Regex.Split(strSummaryData, ",");
        //    string symbol = cells[1];
        //    double last = Double.Parse(cells[3]);
        //    double bid = Double.Parse(cells[10]);
        //    double ask = Double.Parse(cells[11]);
        //    log.Debug("Got summary for " + symbol + ", last=" + last + ", bid=" + bid + ", ask=" + ask);
        //    //OnMessage(this, new MessageEventArgs("Got summary for " + symbol + ", last=" + last + ", bid=" + bid + ", ask=" + ask));

        //    List<TickReq> reqList = null;

        //    lock (iqTickRequests) {
        //        if (iqTickRequests.ContainsKey(symbol)) {
        //            reqList = iqTickRequests[symbol];
        //            iqTickRequests.Remove(symbol);
        //        }
        //        //bool res = iqFeed.RemoveSymbol(ref symbol);
        //        //if (!res) {
        //        //    log.Error("Call to RemoveSymbol for " + symbol + " was unsuccessful.");
        //        //}
        //        if (useSocket) {
        //            //iqServer.RemoveSymbol(symbol);
        //        } else {
        //            iqFeed.RemoveSymbol(ref symbol);
        //        }
        //    }

        //    if (reqList == null) {
        //        return;
        //    }

        //    foreach (TickReq req in reqList) {
        //        PriceRef pr = req.tradeDef.PriceRef;
        //        double price = 0;
        //        switch (pr) {
        //            case PriceRef.Bid:
        //                price = bid;
        //                break;
        //            case PriceRef.Offer:
        //                price = ask;
        //                break;
        //            case PriceRef.Last:
        //                price = last;
        //                break;
        //            case PriceRef.Midpoint:
        //                price = Math.Round((ask + bid) / 2, 2);
        //                break;
        //            case PriceRef.GreatestOfferOrLast:
        //                price = Math.Max(bid, last);
        //                break;
        //            case PriceRef.LeastBidOrLast:
        //                price = Math.Min(bid, last);
        //                break;
        //        }
        //        log.Debug("Got tick price: " + price + " for " + req.symbol + ", using=" + pr);
        //        SendAlertOrder(req.symbol, price, req.tradeDef);
        //    }
        //}

        public void DisconnectAll() {
            DisconnectFromTWS(twsLive);
            DisconnectFromTWS(twsSim);
        }

        public void DisconnectFromTWS(AxTws t) {
            try {
                t.tickPrice -= new AxTWSLib._DTwsEvents_tickPriceEventHandler(tws_tickPrice);
                t.nextValidId -= new AxTWSLib._DTwsEvents_nextValidIdEventHandler(tws_nextValidId);
                //t.updatePortfolioEx -= new AxTWSLib._DTwsEvents_updatePortfolioExEventHandler(tws_updatePortfolioEx);
                t.openOrderEx -= new AxTWSLib._DTwsEvents_openOrderExEventHandler(tws_openOrderEx);
                t.orderStatus -= new AxTWSLib._DTwsEvents_orderStatusEventHandler(tws_orderStatus);
                t.execDetails -= new AxTWSLib._DTwsEvents_execDetailsEventHandler(tws_execDetails);
                t.errMsg -= new AxTWSLib._DTwsEvents_errMsgEventHandler(tws_errMsg);

                t.disconnect();
            } catch (Exception) {
            }
        }

        public void ConnectToTwsLive() {
            ConnectToTws(twsLive, model.IbHost, model.IbPort);
        }
        public void ConnectToTwsSim() {
            ConnectToTws(twsSim, model.IbSimHost, model.IbSimPort);
        }

        private void ConnectToTws(AxTws t, string host, int port) {
            DisconnectFromTWS(t);
            try {
                t.tickPrice += new AxTWSLib._DTwsEvents_tickPriceEventHandler(tws_tickPrice);
                t.nextValidId += new AxTWSLib._DTwsEvents_nextValidIdEventHandler(tws_nextValidId);
                //t.updatePortfolioEx += new AxTWSLib._DTwsEvents_updatePortfolioExEventHandler(tws_updatePortfolioEx);
                t.openOrderEx += new AxTWSLib._DTwsEvents_openOrderExEventHandler(tws_openOrderEx);
                t.orderStatus += new AxTWSLib._DTwsEvents_orderStatusEventHandler(tws_orderStatus);
                t.execDetails += new AxTWSLib._DTwsEvents_execDetailsEventHandler(tws_execDetails);
                t.errMsg += new AxTWSLib._DTwsEvents_errMsgEventHandler(tws_errMsg);
                int clientId = new Random().Next(300) + 1;
                OnMessage(this, new MessageEventArgs("Trying to connect to IB using Host=[" + host + "], port=[" + port + "] with clientId=[" + clientId + "]"));
                //tws.reqAccountUpdates(1, "");
                //tws.reqAutoOpenOrders();  // only with client 0
                t.reqOpenOrders();


                // TODO: used fixed client id
                t.connect(host, port, clientId);
            } catch (Exception e) {
                string msg = "Could not connect to TWS.  Please check that it is running on port " + port
                    + ", Exception: " + e;
                log.Error(msg);
                OnMessage(this, new MessageEventArgs(msg));
            }
        }

        void tws_execDetails(object sender, _DTwsEvents_execDetailsEvent e) {
            log.Debug("execDetails for e.id=" + e.id + ", symbol=" + e.symbol);
            lock (orderManager) {
                //string tags = orderTags[e.id];
                string tags = orderManager.GetTags(e.id);
                if (tags == null) {
                    log.Debug("Got execDetails message for unknown id [" + e.id + "], symbol=" + e.symbol);
                    return;
                }
                log.Debug("for e.id=" + e.id + ", symb=" + e.symbol + ", located tags = [" + tags + "]");
                //List<IOrder> orders = this.activeOrders[e.symbol];
                int parentOrderId = orderManager.GetEntryOrderId(e.id);
                //IOrder order = null;
                //for( int i = 0; i < orders.Count; i++ ) {
                //    if( orders[i].orderId == e.id ) {
                //        order = orders[i];
                //        break;
                //    }
                //}
                //log.Debug("for e.id=" + e.id + ", symb=" + e.symbol + ", matched order=" + order);
                //if( order == null ) {
                //    log.Error("execDetails received for an unknown order, symbol=" + e.symbol + ", id=" + e.id);
                //    return;
                //}
                if (parentOrderId < 0) {
                    log.Error("execDetails received for an unknown order, symbol=" + e.symbol + ", id=" + e.id);
                    return;
                }
                log.Debug("for e.id=" + e.id + ", symb=" + e.symbol + ", order not null, orderId=" + parentOrderId);
                int orderId = parentOrderId;
                string logmsg = "symbol=" + e.symbol + "\texecId=" + e.execId + "\tprice=" + e.price + "\tshares=" + e.shares
                        + "\tside=" + e.side + "\tbaseOrderId=" + orderId + "\ttime=" + DateTime.Now + "\ttags=" + tags;
                log.Debug("gathering exec details for orderId=" + orderId + ", e.id=" + e.id);
                try {
                    RunMode mode = orderManager.GetMode(e.id);
                    log.Debug("Calculated RunMode for " + e.id + " is " + mode + ", logmsg: " + logmsg);
                    if (mode == RunMode.Sim) {
                        //executionLogSim.WriteLine(logmsg);
                        //executionLogSim.Flush();
                        simExecs += "Exec: " + logmsg + "\r\n";
                        executionsSim.WriteLine("Exec: " + logmsg);
                        executionsSim.Flush();
                    } else {
                        //executionLogLive.WriteLine(logmsg);
                        //executionLogLive.Flush();
                        liveExecs += "Exec: " + logmsg + "\r\n";
                        executionsLive.WriteLine("Exec: " + logmsg);
                        executionsLive.Flush();
                    }
                } catch (Exception ex) {
                    log.Error("Exception trying to write to execution log, e.id=" + e.id + ", symbol=" + e.symbol + ", ex=" + ex, ex);
                }
            }
        }

        void tws_orderStatus(object sender, _DTwsEvents_orderStatusEvent e) {
            orderManager.SetPermId(e.id, e.permId);
            log.Debug("OrderStatus, id=" + e.id
                + ", status=" + e.status
                + ", remaining=" + e.remaining);
            if (e.remaining == 0 || e.status == "Cancelled" || e.status == "Filled") {  // || e.status == "Inactive"
                // remove order
                lock (orderManager) {
                    orderManager.RemoveOrder(e.id);
                }
            }
        }

        void tws_openOrderEx(object sender, _DTwsEvents_openOrderExEvent e) {
            /*
                        logger.Debug("OpenOrderEx orderid=" + e.orderId + ", symbol=" + e.contract.symbol
                            + ", state=" + e.orderState.status);
                        lock (activeOrders) {
                            if (!activeOrders.ContainsKey(e.contract.symbol)) {
                                activeOrders[e.contract.symbol] = new List<IOrder>();
                            }
                            bool found = false;
                            foreach (IOrder o in activeOrders[e.contract.symbol]) {
                                if (o.orderId == e.orderId) {
                                    found = true;
                                    break;
                                }
                            }
                            if (!found) {
                                activeOrders[e.contract.symbol].Add(e.order);
                                orderContractKeys[e.orderId] = e.contract;
                                logger.Debug("openOrderEx could not match symb=" + e.contract.symbol
                                    + ", orderId=" + e.orderId + ".  External order?  Force added.  Status is " + e.orderState.status
                                    + ".  Active orders = " + orderListString(activeOrders[e.contract.symbol]));
                            }
                        }
             */
        }

        //void tws_updatePortfolioEx(object sender, _DTwsEvents_updatePortfolioExEvent e) {
        //    log.Debug("UpdatePortfolio symbol=" + e.contract.symbol
        //        + ", value=" + e.marketValue);
        //    if (!portfolio.ContainsKey(e.contract.symbol)) {
        //        log.Debug("A new stock received [" + e.contract.symbol + "], adding to portfolio");
        //        // brand new
        //        portfolio[e.contract.symbol] = e.position;
        //        if (e.position != 0) {
        //            // add new position
        //            log.Debug("New stock [" + e.contract.symbol + "] has " + e.position + ", adding to QT");
        //            //qtserver.AddStockDetailsToPortfolio(QTServer.PORTFOLIO_ID_ATS, e.contract.symbol, e.position, e.averageCost, 1);
        //        } else {
        //            log.Debug("New stock [" + e.contract.symbol + "] has " + e.position + ", NOT adding to QT");
        //        }
        //    } else if (portfolio[e.contract.symbol] != e.position) {
        //        log.Debug("Stock [" + e.contract.symbol + "] changed position, curr != new, " +
        //            portfolio[e.contract.symbol] + "!=" + e.position +
        //            ", new mktVal: " + e.marketValue + ", price=" + e.marketPrice + ", cost=" + e.averageCost);
        //        // a change so delete & re-add
        //        portfolio[e.contract.symbol] = e.position;
        //        //qtserver.RemoveStockFromPortfolio(QTServer.PORTFOLIO_ID_ATS, e.contract.symbol);
        //        if (e.position != 0) {
        //            //qtserver.AddStockDetailsToPortfolio(QTServer.PORTFOLIO_ID_ATS, e.contract.symbol, e.position, e.averageCost, 0);
        //        }
        //    }
        //}

        void tws_errMsg(object sender, _DTwsEvents_errMsgEvent e) {
            switch (e.errorCode) {
                case 202:
                    // order cancelled
                    log.Error("Got error 202, for id=" + e.id + ", order cancelled: " + e.errorMsg);
                    break;
                case 404:  // shorts not available
                    log.Error("Got error 404, for id=" + e.id + ", shorts not available: " + e.errorMsg);
                    break;
                default:
                    logMessage("Error [code=" + e.errorCode + "], msg=[" + e.errorMsg + "] for id=" + e.id);
                    log.Info("TWS error [code=" + e.errorCode + "], msg=[" + e.errorMsg + "] for id=" + e.id);
                    break;
            }
        }

        void tws_nextValidId(object sender, _DTwsEvents_nextValidIdEvent e) {
            orderManager.BaseOrderId = e.id;

            logMessage("Got base order id=" + e.id);
            log.Info("Got base order id=" + e.id);
        }

        void tws_tickPrice(object sender, _DTwsEvents_tickPriceEvent e) {
            TickReq req = tickRequests[e.id];
            PriceRef pr = req.tradeDef.PriceRef;
            log.Debug("For symbol " + req.symbol + ", tickType=" + e.tickType + ", priceRef = " + pr);
            bool isComplete = false;
            // 1 == bid, 2 == ask, 4 == last
            switch (e.tickType) {
                case 1:
                    //log.Debug("bid price for " + req.symbol + " = " + e.price );
                    req.Bid = e.price;
                    isComplete = (pr == PriceRef.Bid || (pr == PriceRef.Midpoint && req.Ask > 0));
                    break;
                case 2:
                    //log.Debug("ask price for " + req.symbol + " = " + e.price);
                    req.Ask = e.price;
                    isComplete = (pr == PriceRef.Offer || (pr == PriceRef.Midpoint && req.Bid > 0));
                    break;
                case 4:
                    //log.Debug("last price for " + req.symbol + " = " + e.price);
                    req.Last = e.price;
                    isComplete = (pr == PriceRef.Last);
                    break;
            }
            if (isComplete) {
                TimeSpan timeDiff = DateTime.Now - req.entryTime;
                long millis = timeDiff.Milliseconds;
                if (model.IbQuoteTimeout >= millis) {
                    double price = e.price;
                    if (pr == PriceRef.Midpoint) {
                        price = Math.Round((req.Ask + req.Bid) / 2, 2);
                    }
                    log.Debug("Got tick price: " + price + " for " + req.symbol + ", millis=" + millis + ", using=" + pr);
                    SendAlertOrder(req.symbol, price, req.tradeDef);
                } else {
                    logMessage("Time expired for tick price for " + req.symbol + ", millis=" + millis);
                    log.Debug("Time expired for tick price for " + req.symbol + ", millis=" + millis);
                }
            }
        }

        // for debugging only
        private string orderListString(List<IOrder> orders) {
            string det = "";
            foreach (IOrder o in orders) {
                det = det + "[id=" + o.orderId + ", act=" + o.action + ", type=" + o.orderType + "]";
            }
            return det;
        }
        private string orderIdListString(List<int> orders) {
            string det = "";
            foreach (int o in orders) {
                det = det + "[id=" + o + "]";
            }
            return det;
        }

        private void logMessage(string message) {
            OnMessage(this, new MessageEventArgs(message));
        }

        private void Notify(string message) {
            //Notification notification1 = new Notification();
            //notification1.Message = message;
            //notification1.Show();
        }

        private AxTws getTws(AutoTradeDefinition def) {
            return def.Mode == RunMode.Live ? twsLive : twsSim;
        }

        private bool CanPlaceTrade(string symbol, double last, AutoTradeDefinition def) {
            //lock (this.activeOrders) {
            lock (orderManager) {
                if (!model.AllowDuplicates) {
                    // distinguish between Sim and Live!
                    if (orderManager.CountActiveTrades(symbol, def.Mode) > 0) {
                        // duplicate so ignore
                        logMessage("Concurrent alert for " + symbol + " sent from " + def.Mode + ": " + def.WindowName + ", ignoring.");
                        log.Info("Concurrent alert for " + symbol + " sent from " + def.Mode + ": " + def.WindowName + ", ignoring.");
                        List<int> det = orderManager.GetActiveTradeIds(symbol, def.Mode);
                        log.Debug("activeOrders[" + symbol + "] == " + orderIdListString(det));
                        return false;
                    }
                }
                // check for existing trade with same strategy & symbol
                foreach (int id in orderManager.GetActiveTradeIds(def)) {
                    if (orderManager.GetSymbol(id) == symbol) {
                        log.Info("Duplicate alert for " + symbol + " sent from " + def.WindowName + ", ignoring.");
                        return false;
                    }
                }
                if (def.MaxTradesPerStrategy > 0) {
                    int orderCount = orderManager.CountTotalTrades(def);
                    //int orderCount = 0;
                    //foreach (StratOrders so3 in this.stratOrders) {
                    //    if (so3.tradeDefn == def) {
                    //        orderCount++;
                    //    }
                    //}
                    if (orderCount > def.MaxTradesPerStrategy) {
                        string maxMsg = "Already sent out " + orderCount + " orders ( > " + def.MaxTradesPerStrategy + ") for " +
                            def.WindowName + ", ignoring.";
                        log.Info(maxMsg);
                        logMessage(maxMsg);
                        return false;
                    }
                }
                if (!def.CanTrade()) {
                    log.Info("SendAlertOrder called where CanTrade = false");
                    return false;
                }

            }
            return true;
        }

        private StratOrders CreateStratOrders(AutoTradeDefinition def, string symbol, double price) {
            AxTws tws = getTws(def);
            DateTime now = DateTime.Now;

            StratOrders so = new StratOrders();
            so.tradeDefn = def;
            so.symbol = symbol;
            so.entry = tws.createOrder();
            so.timestop = tws.createOrder();

            switch (def.PositionSizing) {
                case PositionSizingType.FixedAmount:
                    so.entry.totalQuantity = Convert.ToInt32(def.Sizing / price);
                    break;
                case PositionSizingType.FixedRisk:
                    if (!def.UseStopLoss) {
                        // error
                    }
                    if (def.Units == TradeUnit.Percent) {
                        so.entry.totalQuantity = (int)(def.Sizing / (price * (def.StopLoss / 100.0)));
                    } else {
                        so.entry.totalQuantity = (int)(def.Sizing / def.StopLoss);
                    }
                    break;
                case PositionSizingType.FixedShares:
                    so.entry.totalQuantity = def.Sizing;
                    break;
            }
            so.entry.totalQuantity = Math.Min(so.entry.totalQuantity, def.MaxShares);
            so.entry.totalQuantity -= so.entry.totalQuantity % def.SharesIncrement;

            if (def.EntryOrderType == OrderType.Limit) {
                so.entry.orderType = "LMT";
                so.entry.lmtPrice = price;
                if (def.UseLimitOffset) {
                    if (def.EntryDirection == Direction.Long) {
                        so.entry.lmtPrice = def.Units == TradeUnit.Percent ? 
                            price * (1.0 + (def.LimitOffset / 100.0)) :
                            price + def.LimitOffset;
                        so.entry.lmtPrice = Math.Round(so.entry.lmtPrice, 2);
                    } else {
                        so.entry.lmtPrice = def.Units == TradeUnit.Percent ?
                            price * (1.0 - (def.LimitOffset / 100.0)) :
                            price - def.LimitOffset;
                        so.entry.lmtPrice = Math.Round(so.entry.lmtPrice, 2);
                    }
                }
            } else if (def.EntryOrderType == OrderType.Market) {
                so.entry.orderType = "MKT";
            }

            so.entry.timeInForce = "DAY";  // also GTC, IOC, GTD

            //int count = stratOrders.Count;
            DateTime mktCloseTime = new DateTime(now.Year, now.Month, now.Day,
                model.MktCloseHrs, model.MktCloseMins, 0);
            DateTime gtd;
            if (def.UseGoodForSecs) {
                gtd = DateTime.Now.AddSeconds(def.GoodForSecs);
                if (gtd >= mktCloseTime) {
                    gtd = mktCloseTime.AddMinutes(-1);
                }
            } else {
                gtd = mktCloseTime.AddMinutes(-1);
            }
            so.entry.goodTillDate = gtd.ToString("yyyyMMdd HH:mm:ss");
            so.entry.timeInForce = "GTD";


            so.profitTarget = def.UseProfitTarget ? tws.createOrder() : null;
            so.stoploss = def.UseStopLoss ? tws.createOrder() : null;
            so.entry.action = (def.EntryDirection == Direction.Long) ? "BUY" : "SELL";
            if (so.profitTarget != null) {
                so.profitTarget.action = (def.EntryDirection == Direction.Long) ? "SELL" : "BUY";
                so.profitTarget.orderType = "LMT";

                double offset = 0;
                offset = (def.Units == TradeUnit.Percent) ? price * (def.ProfitTarget / 100.0) : def.ProfitTarget;
                so.profitTarget.lmtPrice = def.EntryDirection == Direction.Long ? price + offset : price - offset;
                so.profitTarget.lmtPrice = Math.Round(so.profitTarget.lmtPrice, 2);
            }
            if (so.stoploss != null) {
                so.stoploss.action = (def.EntryDirection == Direction.Long) ? "SELL" : "BUY";
                so.stoploss.orderType = "STP";

                double offset = 0;
                offset = (def.Units == TradeUnit.Percent) ? price * (def.StopLoss / 100.0) : def.StopLoss;
                offset = def.EntryDirection == Direction.Long ? offset * -1 : offset;
                so.stoploss.auxPrice = Math.Round(price + offset, 2);
                log.Debug("Calculating stoploss offset, units=" + def.Units
                    + ", stoploss=" + def.StopLoss
                    + ", auxPrice=" + so.stoploss.auxPrice
                    + ", offset=" + offset);
            }
            if (so.timestop != null) {
                so.timestop.action = (def.EntryDirection == Direction.Long) ? "SELL" : "BUY";
                so.timestop.orderType = "MKT";
                DateTime exitTime = mktCloseTime;
                if (def.TimeExitType == ExitTimeType.MinsAfterEntry) {
                    exitTime = DateTime.Now.AddMinutes(def.MinsAfterEntry);
                } else if (def.TimeExitType == ExitTimeType.MinsBeforeClose) {
                    exitTime = mktCloseTime.AddMinutes(def.MinsBeforeClose * -1);
                } else if (def.TimeExitType == ExitTimeType.AtNextOpen) {
                    // TODO: support this!
                    throw new NotSupportedException("at next open not yet supported");
                }
                if (exitTime != null) {
                    if (def.TimeExitType == ExitTimeType.MinsBeforeClose && def.MinsBeforeClose == 0) {
                        // MOC/LOC exit
                        so.timestop.orderType = "MOC";
                    } else {
                        if (mktCloseTime <= exitTime) {
                            // exit time is at or after market close, so use a MOC for exit
                            //so.timestop.orderType = "MOC";
                            //so.timestop.timeInForce = "DAY";

                            exitTime = mktCloseTime.AddMinutes(-1);
                        }
                        so.timestop.goodAfterTime = exitTime.ToString("yyyyMMdd HH:mm:ss");

                        // make sure entry is before exit
                        if (gtd >= exitTime) {
                            gtd = exitTime.AddMinutes(-1);
                            so.entry.goodTillDate = gtd.ToString("yyyyMMdd HH:mm:ss");
                        }
                    }
                }

            }
            return so;
        }

        /// <summary>
        /// Builds and sends an order to fulfill the order
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="tradeDef"></param>
        private void SendAlertOrder(string symbol, double last, AutoTradeDefinition def) {
            log.Debug("Sending alert order for symbol=" + symbol + ", ref price=" + last + ", windowName=" + def.WindowName);
            if (!CanPlaceTrade(symbol, last, def)) {
                return;
            }

            Notify("New order (" + orderManager.CountTotalTrades(def) + ") placed for " + symbol + " for strategy: \n" + def.WindowName);
            logMessage("New order (" + orderManager.CountTotalTrades(def) + ") placed for " + symbol + " for strategy: " + def.WindowName);
            log.Info("New order placed for " + symbol + " for strategy: " + def.WindowName);

            DateTime now = DateTime.Now;

            AxTws tws = getTws(def);

            IContract contract = tws.createContract();
            contract.symbol = symbol;
            contract.secType = "STK";
            contract.exchange = "SMART";
            contract.currency = "USD";

            IContract nyseContract = tws.createContract();
            nyseContract.symbol = symbol;
            nyseContract.secType = "STK";
            nyseContract.exchange = "SMART";  // why does "NYSE" not work yet?
            nyseContract.currency = "USD";

            StratOrders so = CreateStratOrders(def, symbol, last);

            long startOrderId = 0;
            lock (orderManager) {
                startOrderId = orderManager.ReqNextOrderId();
                //startOrderId = this.baseOrderId + (long)(ORDER_INCREMENT * orderManager.CountTotalTrades());

                //stratOrders.Add(so);
            }
            //so.entry.ocaGroup = symbol + count;
            so.entry.ocaType = 2;  // remaining orders proportionately reduced in size with block
            IOrder[] orders = { so.entry, so.profitTarget, so.stoploss, so.timestop };
            for (int i = 0; i < orders.Count(); i++) {
                if (orders[i] == null) continue;

                if (i > 0) {
                    orders[i].totalQuantity = so.entry.totalQuantity;

                    // MOC orders can't belong to an OCA group???
                    // we get error 202, reason "Invalid OCA handling method"
                    if (orders[i].orderType != "MOC") {
                        orders[i].ocaType = so.entry.ocaType;
                        orders[i].parentId = orders[0].orderId;
                    }
                }
                orders[i].orderId = (int)startOrderId + i;
                // only the last non-MOC order in the OCA group gets 'transmit = true'
                int lastNonMoc = orders.Count() - 1;
                while (orders[lastNonMoc].orderType == "MOC") {
                    lastNonMoc--;
                }
                bool isTransmit = model.TransmitOrders && def.Transmit &&
                    (orders[i].orderType == "MOC" || i == lastNonMoc);
                orders[i].transmit = isTransmit ? 1 : 0;
                IContract cont = contract;
                if (orders[i].orderType == "MOC") {
                    cont = nyseContract;
                }
                //if (isTransmit) {
                //    Thread.Sleep(300);
                //}
                tws.placeOrderEx(orders[i].orderId, cont, orders[i]);
                Thread.Sleep(300);
                lock (orderManager) {
                    orderManager.AddOrder(orders[i], orders[0].orderId, cont,
                        "", def);
                }
                log.Info("placed order [id=" + orders[i].orderId
                    + ", action=" + orders[i].action
                    + ", qty=" + orders[i].totalQuantity
                    + ", symb=" + contract.symbol
                    + ", parentId=" + orders[i].parentId
                    + ", limitPrice=" + orders[i].lmtPrice
                    + ", orderType=" + orders[i].orderType
                    + ", tif= " + orders[i].timeInForce
                    + ", transmit=" + orders[i].transmit
                    + ", goodAfterTime=" + orders[i].goodAfterTime
                    + ", goodTillDate=" + orders[i].goodTillDate
                    + ", auxPrice=" + orders[i].auxPrice
                    + ", trailStopPrice=" + orders[i].trailStopPrice
                    + "], activeOrders=" + orderManager.GetActiveTradeIds(symbol, def.Mode));
                //orderMode[orders[i].orderId] = def.Mode;
            }
            orderManager.AddTrade(def.Tags, symbol, so.entry.orderId, orders);


            string tags = def.Tags;
            //orderTags[so.entry.orderId] = def.Tags;
            if (def.Mode == RunMode.Sim) {
                if (def.Tags.Length > 0) {
                    tags += ", ";
                }
                if (def.Mode == RunMode.Sim) {
                    tags += "Sim";
                } else {
                    tags += "Live";
                }
            }
            orderManager.SetTags(so.entry.orderId, tags);
            if (so.profitTarget != null) orderManager.SetTags(so.profitTarget.orderId, "Profit Target");
            if (so.stoploss != null) orderManager.SetTags(so.stoploss.orderId, "Stoploss");
            if (so.timestop != null) orderManager.SetTags(so.timestop.orderId, "Timestop");

            string orderDesc = "symbol=" + symbol + "\ttime=" + now
                + "\ttags=" + def.Tags
                + "\talert price=" + last
                + "\tquantity=" + so.entry.totalQuantity
                + "\torderId=" + so.entry.orderId;
            if (so.stoploss != null) {
                orderDesc += "\tamt risked=" + Math.Round(Math.Abs((so.stoploss.auxPrice - last) * so.entry.totalQuantity), 2);
                orderDesc += "\texit price=" + so.stoploss.auxPrice;
            }
            if (so.timestop != null) {
                orderDesc += "\ttimestop=" + so.timestop.goodAfterTime;
            }
            if (def.Mode == RunMode.Sim) {
                simExecs += "Order: " + orderDesc + "\r\n";
                executionsSim.WriteLine("Order: " + orderDesc);
                executionsSim.Flush();
            } else {
                liveExecs += "Order: " + orderDesc + "\r\n";
                executionsLive.WriteLine("Order: " + orderDesc);
                executionsLive.Flush();
            }

            if (model.BeepOnOrder) {
                System.Media.SystemSounds.Beep.Play();
            }
        }

        public void LoadModel() {
            FileInfo f = new FileInfo("config.xml");
            try {
                XmlSerializer x = new XmlSerializer(model.GetType());
                this.model = (BotModel)x.Deserialize(f.OpenRead());
            } catch (Exception ex) {
                log.Error("Could not load " + f.FullName + ": " + ex);
                this.model = new BotModel();
            }
            log.Debug("Right after deserialize, TransmitOrders = " + model.TransmitOrders + ", AllowDuplicates = " + model.AllowDuplicates);
            ModelUpdated(this, new ModelEventArgs(model));
            logMessage("Successfully loaded config from " + f.FullName);
            log.Debug("Successfully loaded config from " + f.FullName);
            log.Debug("After ModelUpdated, TransmitOrders = " + model.TransmitOrders + ", AllowDuplicates = " + model.AllowDuplicates);
        }

        public void SaveModel() {
            // save the state
            try {
                XmlSerializer x = new XmlSerializer(model.GetType());
                FileInfo f = new FileInfo("config.xml");
                //x.Serialize(f.OpenWrite(), model);
                x.Serialize(f.Create(), model);
                log.Debug("Successfully saved model to " + f.FullName);
                logMessage("Successfully saved model to " + f.FullName);
                MessageBox.Show("The layout was saved successfully", "Success");
            } catch (Exception ex) {
                MessageBox.Show("There was a problem saving the state:\n" + ex.Message, "Save error");
                log.Error("Could not save the model", ex);
            }
        }

		public void RunTest() {
			string symbol = "GOOG";
            AutoTradeDefinition defn = new AutoTradeDefinition();
            defn.EntryDirection = Direction.Long;
            defn.EntryOrderType = OrderType.Market;
            defn.MaxShares = 100;
            defn.MinsAfterEntry = 6;
            defn.Mode = RunMode.Sim;
            defn.PositionSizing = PositionSizingType.FixedShares;
            defn.Sizing = 100;
            defn.Tags = "";
            defn.IsEnabled = true;

            HandleAlert(symbol, defn);
		}

        public void HandleAlert(String symbol, AutoTradeDefinition defn) {
            log.Debug("handling alert for symbol=" + symbol + ", from " + defn.WindowName);
            try {
                alertLog.WriteLine(defn.WindowName + "," + symbol + ",0.0" );
                alertLog.Flush();
            } catch (Exception ex) {
                log.Error("Could not log alert for WN=" + defn.WindowName + ", symbol=" + symbol + ", error=" + ex);
            }
            //if (defn.Tags.Contains("dummy")) {
            //    // only meant to be logged, no need for tick requests
            //    return;
            //}

            //lock (iqTickRequests) {
            //    if (!iqTickRequests.Keys.Contains(symbol)) {
            //        log.Debug("No one currently watching " + symbol + ", adding iqFeed watch");
            //        iqTickRequests[symbol] = new List<TickReq>();
            //        log.Debug("Watching symbol (" + symbol + ")");
            //        iqFeed.WatchSymbol(ref symbol);
            //    }
            //    iqTickRequests[symbol].Add(new TickReq(symbol, DateTime.Now, defn));
            //}
            int reqId = 0;
            lock (tickRequests)
            {
                reqId = tickRequests.Count();
                tickRequests.Add(new TickReq(symbol, DateTime.Now, defn));
            }
            AxTws tws = getTws(defn);

            IContract contract = tws.createContract();
            contract.symbol = symbol;
            contract.secType = "STK";
            contract.exchange = "SMART";
            contract.currency = "USD";
            log.Debug("Sending market data request for " + symbol + ", defn=" + defn.WindowName);
            tws.reqMktDataEx(reqId, contract, "", 1);  // 1 for "true"?  Use snapshot, so don't need to cancel
        }
    }

    public class ModelEventArgs : EventArgs
    {
        public readonly BotModel Model;

        public ModelEventArgs(BotModel model) {
            Model = model;
        }
    }
    public class MessageEventArgs : EventArgs
    {
        public readonly string Message;

        public MessageEventArgs(string message) {
            Message = message;
        }
    }
}
