using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TWSLib;
using AxTWSLib;
using NLog;
//using MySql.Data;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Data;


namespace TradeIdeasRobot
{
    /*
     * Data we need:
     * - base order id (reference for all future requests)
     * - list of all active orders
     * - num of all orders & requests (tick requests + orders)
     * - associate an id with either a tick request or order
     * - for a given orderId, need:
     *   - contract (symbol)
     *   - RunMode
     *   - entry order ID
     *   - tags
     * - for a given TradeDefinition need:
     *   - number of trades placed today
     *   - number of outstanding trades
     * - for a given Contract/symbol need:
     *   - number of outstanding tradse/orders
     */

    public class OrderManager
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private long baseOrderId;

        private Dictionary<int, OrderData> orderData;

        private Dictionary<int, TradeData> tradeData;

        public OrderManager() {
            orderData = new Dictionary<int, OrderData>();
            tradeData = new Dictionary<int, TradeData>();
        }

        public long BaseOrderId {
            set { baseOrderId = Math.Max(baseOrderId, value); }
        }

        public long ReqNextOrderId() {
            long ret = baseOrderId;
            baseOrderId += 5;
            return ret;
        }

        public int CountActiveTrades(AutoTradeDefinition def) {
            List<int> allTrades = GetActiveTradeIds(def);
            return allTrades.Count;
        }
        public List<int> GetActiveTradeIds(AutoTradeDefinition def) {
            // TODO: add another hashmap for lookup?
            List<int> allTrades = new List<int>();
            foreach (OrderData odat in orderData.Values) {
                if (odat.IsActive && !allTrades.Contains(odat.ParentOrderId) && odat.TradeDef == def) {
                    allTrades.Add(odat.ParentOrderId);
                }
            }
            return allTrades;
        }

        public int CountActiveTrades(string symbol, RunMode mode) {
            return GetActiveTradeIds(symbol, mode).Count;
        }
        public List<int> GetActiveTradeIds(string symbol, RunMode mode) {
            // TODO: add another hashmap for lookup?
            List<int> allTrades = new List<int>();
            foreach (OrderData odat in orderData.Values) {
                if (odat.IsActive && odat.Contract.symbol == symbol && odat.TradeDef.Mode == mode 
                        && !allTrades.Contains(odat.ParentOrderId)) {
                    allTrades.Add(odat.ParentOrderId);
                }
                //if (odat.IsActive) {
                //    log.Debug("active: [" + odat.Contract.symbol + "], incoming symbol=" + symbol);
                //}
            }
            return allTrades;
        }

        public int CountTotalTrades(AutoTradeDefinition def) {
            // TODO: add another hashmap for lookup?
            List<int> allTrades = new List<int>();
            foreach (OrderData odat in orderData.Values) {
                if (odat.TradeDef == def && !allTrades.Contains(odat.ParentOrderId)) {
                    allTrades.Add(odat.ParentOrderId);
                }
            }
            return allTrades.Count;
        }
        
        public RunMode GetMode(int orderId) {
            if (!orderData.ContainsKey(orderId)) {
                log.Error("Could not locate Mode for " + orderId + ", returning Sim as default.");
                return RunMode.Sim;
            }
            return orderData[orderId].TradeDef.Mode;
        }
        
        public int GetEntryOrderId(int orderId) {
            if (!orderData.ContainsKey(orderId)) {
                return -1;
            }
            return orderData[orderId].ParentOrderId;
        }
        
        public string GetTags(int orderId) {
            if (!orderData.ContainsKey(orderId)) {
                return null;
            }
            return orderData[orderId].Tags;
        }
        public void SetTags(int orderId, string tags) {
            orderData[orderId].Tags = tags;
        }

        public string GetSymbol(int orderId) {
            if (!orderData.ContainsKey(orderId)) {
                return null;
            }
            return orderData[orderId].Contract.symbol;
        }

        public void RemoveOrder(int orderId) {
            if (!orderData.ContainsKey(orderId)) {
                return;
            }
            orderData[orderId].IsActive = false;
        }

        public void AddOrder(IOrder order, int parentOrderId, IContract contract,
                string tags, AutoTradeDefinition def) {
            OrderData odat = new OrderData();
            odat.OrderId = order.orderId;
            odat.ParentOrderId = parentOrderId;
            odat.TradeDef = def;
            odat.Contract = contract;
            odat.IsActive = true;
            orderData.Add(order.orderId, odat);
        }

        public void AddTrade(string tags, string symbol, int parentOrderId, IOrder[] so) {
            //IOrder[] orders = { so.entry, so.profitTarget, so.stoploss, so.timestop };

            TradeData td = new TradeData();
            td.Tags = tags;
            td.Symbol = symbol;
            td.EntryOrder = so[0];
            td.TargetOrder = so[1];
            td.PriceStopOrder = so[2];
            td.TimeStopOrder = so[3];

            tradeData[parentOrderId] = td;
        }
        public void SetPermId(int orderId, int permId) {
            int entryId = GetEntryOrderId(orderId);
            if (entryId <= 0) return;
            TradeData td = tradeData[entryId];
            if (td == null) return;

            if (td.IsSaved) return;

            if (td.EntryOrder.orderId == orderId) td.EntryPermId = permId;
            else if (td.TargetOrder != null && td.TargetOrder.orderId == orderId) td.TargetPermId = permId;
            else if (td.TimeStopOrder != null && td.TimeStopOrder.orderId == orderId) td.TimeStopPermId = permId;
            else if (td.PriceStopOrder != null && td.PriceStopOrder.orderId == orderId) td.PriceStopPermId = permId;

            if (td.IsComplete()) {
                td.IsSaved = true;
                //SaveTradeData("sim", td);
            }
        }
    }

    //enum ReqType { TypeOrder, TypeTick }

    class OrderData
    {
        public int OrderId;
        public int ParentOrderId;
        public string Tags;
        //public RunMode Mode;
        public AutoTradeDefinition TradeDef;
        public IContract Contract;
        public bool IsActive;
        // public string permId;
    }

    class TradeData
    {
        public string Tags;
        public string Symbol;
        //public int EntryOrderId = -1;
        //public int TargetOrderId = -1;
        //public int TimeStopOrderId = -1;
        //public int PriceStopOrderId = -1;
        public IOrder EntryOrder, TargetOrder, TimeStopOrder, PriceStopOrder;

        public int EntryPermId = -1;
        public int TargetPermId = -1;
        public int TimeStopPermId = -1;
        public int PriceStopPermId = -1;

        public bool IsSaved = false;  // set to true when written to DB

        public bool IsComplete() {
            return (EntryOrder != null ? EntryPermId > 0 : true)
                && (TargetOrder != null ? TargetPermId > 0 : true)
                && (TimeStopOrder != null ? TimeStopPermId > 0 : true)
                && (PriceStopOrder != null ? PriceStopPermId > 0 : true);
        }
    }
}
