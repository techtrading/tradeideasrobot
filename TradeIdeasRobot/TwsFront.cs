using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TWSLib;
using AxTWSLib;

namespace TradeIdeasWindowApp
{
    public class PositionEventArgs : EventArgs
    {
        public readonly PositionEventType Type;
        public readonly string Symbol;

        public PositionEventArgs(PositionEventType type, string symbol) {
            this.Type = type;
            this.Symbol = symbol;
        }
    }
    public enum PositionEventType { PositionOpened, PositionClosed };

    public class TickEventArgs : EventArgs
    {
        public readonly TickType Type;
        public readonly string Symbol;
        public readonly MktData Data;

        public TickEventArgs(TickType type, string symbol, MktData data) {
            this.Type = type;
            this.Symbol = symbol;
            this.Data = data;
        }
    }
    public enum TickType { Bid, Ask, Last };

    /**
     * A front-end or facade to TWS and its data to allow central source for data
     * subscriptions and unsubscribes, account details, outstanding orders and
     * other TWS-related features shared by many tools
     */
    public class TwsFront
    {
        // associates data request ids with the contract
        private int nextDataId = 100;
        private Dictionary<int, string> dataToTicker;

        // current data snapshot
        private Dictionary<string, MktData> allMktData;

        // current portfolio
        private Dictionary<string, Position> allPositions;

        // all outstanding orders

        // today's executions


        // The TWS Connection
        private AxTWSLib.AxTws tws;


        // events

        // position opened or closed
        public event EventHandler<PositionEventArgs> PositionChanged;

        // tick
        //public event EventHandler<TickEventArgs> TickEvent;
        // TWS bug, event handler only relays events to a single handler, so must

        public event AxTWSLib._DTwsEvents_tickPriceEventHandler tickPrice;

        public TwsFront(AxTWSLib.AxTws tws) {
            this.tws = tws;
            dataToTicker = new Dictionary<int, string>();
            allMktData = new Dictionary<string, MktData>();
            allPositions = new Dictionary<string, Position>();

            // initialize
            tws.updatePortfolioEx += new AxTWSLib._DTwsEvents_updatePortfolioExEventHandler(tws_updatePortfolioEx);
            tws.tickPrice += new AxTWSLib._DTwsEvents_tickPriceEventHandler(tws_tickPrice);

            //PostConnect();
            //tws.reqAccountUpdates(1, "DU15935");

            //// add dummy account entry
            //lock (this) {
            //    string symbol = "GOOG";
            //        // new position, so add & subscribe to data
            //        allPositions[symbol] = new Position();
            //        reqMktData(symbol);
            //        // fire notification event
            //        PositionChanged(this, new PositionEventArgs(PositionEventType.PositionOpened, symbol));
            //    allPositions[symbol].avgPrice = e.averageCost;
            //    allPositions[symbol].shares = e.position;
            //}

        }

        void tws_tickPrice(object sender, _DTwsEvents_tickPriceEvent e) {
            string symbol = dataToTicker[e.id];
            if( symbol == null ) {
                // could not locate id, pass it along.
                tickPrice(sender, e);
                // TODO: log error
                return;
            }
            if( allMktData.ContainsKey(symbol) ) {
                MktData data = allMktData[symbol];
                switch(e.tickType) {
                    case 1:
                        data.bid = e.price;
                        break;
                    case 2:
                        data.ask = e.price;
                        break;
                    case 4:
                        data.last = e.price;
                        break;
                }
            }
        }

        public Position GetPosition(string symbol) {
            return allPositions[symbol];
        }

        public List<string> GetAllPositions() {
            return (List<string>)allPositions.Keys.ToList();
        }

        public MktData GetMarketData(string symbol) {
            return allMktData[symbol];
        }

        void tws_updatePortfolioEx(object sender, _DTwsEvents_updatePortfolioExEvent e) {
            lock (this) {
                if (e.position != 0) {
                    if (!allPositions.ContainsKey(e.contract.symbol)) {
                        // new position, so add & subscribe to data
                        allPositions[e.contract.symbol] = new Position();
                        reqMktData(e.contract.symbol);
                        // fire notification event
                        PositionChanged(this, new PositionEventArgs(PositionEventType.PositionOpened, e.contract.symbol));
                    }
                    allPositions[e.contract.symbol].avgPrice = e.averageCost;
                    allPositions[e.contract.symbol].shares = e.position;
                } else {
                    // position is closed, unsubscribe to data
                    // TODO: need to check for outstanding orders
                    if (dataToTicker.ContainsValue(e.contract.symbol)) {
                        cancelMktData(e.contract.symbol);
                    }
                    if( allPositions.ContainsKey(e.contract.symbol) ) {
                        allPositions.Remove(e.contract.symbol);
                        PositionChanged(this, new PositionEventArgs(PositionEventType.PositionClosed, e.contract.symbol));
                    }
                }
            }
        }

        public void PostConnect() {
            tws.reqAccountUpdates(1, "DU15935");
        }

        //public void reconnectData() {
        //    lock (this) {
        //        foreach (MktData data in allMktData.Values) {
        //            tws.reqMktDataEx(data.reqId, CreateContract(dataToTicker[data.reqId]), "", 0);
        //        }
        //    }
        //}


        public void reqMktData(string ticker) {
            lock (this) {
                if (dataToTicker.ContainsValue(ticker)) {
                    return;
                }
                int reqId = nextDataId++;
                dataToTicker[reqId] = ticker;
                allMktData[ticker] = new MktData();
                allMktData[ticker].reqId = reqId;
                tws.reqMktDataEx(reqId, CreateContract(ticker), "", 0);
            }
        }

        public int NextDataId() {
            lock (this) {
                return nextDataId++;
            }
        }


        public void cancelMktData(string ticker) {
            lock (this) {
                if (!dataToTicker.ContainsValue(ticker)) {
                    return;
                }
                int id = allMktData[ticker].reqId;
                tws.cancelMktData(id);
                allMktData.Remove(ticker);
                dataToTicker.Remove(id);
            }
        }

        private IContract CreateContract(string ticker) {
            IContract contract = tws.createContract();
            contract.symbol = ticker;
            contract.secType = "STK";
            contract.exchange = "SMART";
            contract.currency = "USD";
            return contract;
        }

        public AxTWSLib.AxTws Tws {
            get { return tws; }
        }
    }

    public class MktData
    {
        public int reqId;
        public double bid, ask, last;
        //public long volume;
    }
    public class Position
    {
        public int shares;
        public double avgPrice;
    }
}
