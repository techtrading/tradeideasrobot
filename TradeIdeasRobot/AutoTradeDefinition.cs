using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.IO;

using TradeIdeasWindow;
using NLog;

namespace TradeIdeasWindowApp
{
    public enum Direction {Long, Short};
    public enum TradeUnit {Points, Percent};
    public enum ExitCondition {None, PercTrailStop, BarsTrailStop, Alert};
    public enum ExitTimeType {MinsAfterEntry, MinsBeforeClose, AtNextOpen};
    public enum PositionSizingType { FixedShares, FixedAmount, FixedRisk };
    public enum OrderType { Market, Limit };
    public enum RunMode { Sim, Live };
    public enum PriceRef { Last, Bid, Offer, Midpoint, GreatestOfferOrLast, LeastBidOrLast };

    public class AutoTradeDefinition
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public bool IsEnabled = false;
        public RunMode Mode = RunMode.Sim;

        public string TiUrl;
        public string WindowName;

        public Direction EntryDirection;
        public TradeUnit Units;
        public double DollarsValue;
        public double PercentsValue;

        public bool UseProfitTarget = false;
        public double ProfitTarget = 0;
        public bool UseStopLoss = false;
        public double StopLoss;
        public bool UseStopWiggle = false;

        public ExitCondition OtherExitCondition;
        public double PercTrailStop;
        public double BarsTrailStop;
        public string ExitConditionAlert;

        public ExitTimeType TimeExitType;
        public int MinsAfterEntry;
        public int MinsBeforeClose;

        public int StartHrs;
        public int StartMins;
        public int EndHrs;
        public int EndMins;

        public PositionSizingType PositionSizing;
        public int Sizing = 1;

        public int MaxShares = 500;
        public int SharesIncrement = 1;
        public int TradesPerSymbol = 0;  // 0 == unlimited
        public int MaxTradesPerStrategy = 0;  // 0 == unlimited
        public bool Transmit = true;

        public OrderType EntryOrderType;
        public bool UseLimitOffset;
        public double LimitOffset;
        public bool UseGoodForSecs;
        public int GoodForSecs;
        public PriceRef PriceRef = PriceRef.Last;

        public String Tags;

        [XmlIgnore]
        private TradeIdeasWindowX tiWindow = null;
        public event EventHandler<AlertArgs> OnAlert;
        [XmlIgnore]
        private BotModel model = null;

        public override bool Equals(object obj) {
            if (obj.GetType() != this.GetType()) {
                return false;
            }
            AutoTradeDefinition that = (AutoTradeDefinition)obj;
            return this.Mode == that.Mode
                && this.TiUrl == that.TiUrl
                && this.Tags == that.Tags
                && this.StrategyConfig == that.StrategyConfig;
        }

        public void Connect(BotModel model) {
            if (model != null || tiWindow != null) {
                Disconnect();
            }
            this.model = model;
            tiWindow = new TradeIdeasWindowX();
            tiWindow.OnNewAlert += new ITradeIdeasWindowXEvents_OnNewAlertEventHandler(tiWindow_OnNewAlert);
            tiWindow.NotifyOnEachAlert = true;
            tiWindow.DataConfig = TiUrl;

        }
        public void Disconnect() {
            this.model = null;
            if (tiWindow != null) {
                tiWindow.NotifyOnEachAlert = false;
                tiWindow.OnNewAlert -= new ITradeIdeasWindowXEvents_OnNewAlertEventHandler(tiWindow_OnNewAlert);
                tiWindow = null;
            }
        }

        void tiWindow_OnNewAlert(AlertData Alert) {
            if (CanTrade()) {
                log.Debug("Received alert from " + this.WindowName + ", symbol: " + Alert.Symbol);
                OnAlert(this, new AlertArgs(Alert));
            } else {
                //log.Debug("Received alert from " + this.WindowName + " but can't trade.  Symbol: " + Alert.Symbol);
            }
        }

        public bool CanTrade() {
            if (!IsEnabled) {
                return false;
            }
            // see if it's within range
            DateTime now = DateTime.Now;
            DateTime stratStart = new DateTime(now.Year, now.Month, now.Day,
                model.MktStartHrs, model.MktStartMins, 0);
            stratStart = stratStart.AddHours(StartHrs);
            stratStart = stratStart.AddMinutes(StartMins);
            log.Debug("TradeDefn start time: " + stratStart + ", now=" + now);
            

            DateTime stratEnd = new DateTime(now.Year, now.Month, now.Day,
                model.MktCloseHrs, model.MktCloseMins, 0);
            stratEnd = stratEnd.AddHours(-1 * EndHrs);
            stratEnd = stratEnd.AddMinutes(-1 * EndMins);

            if (now < stratStart || now > stratEnd) {
                // out of bounds
                //log.Debug("Alert received for " + this.WindowName + " which is out of time bounds, ignoring.");
                return false;
            }
            return true;
        }

        public static string ParseWindowName(string dataConfig) {
            try {
                if (dataConfig.Length < 5) {
                    return "";
                }
                string[] pairs = Regex.Split(dataConfig, @"\&");
                string wnPair = "";
                foreach (string pair in pairs) {
                    if (pair.StartsWith("WN=")) {
                        wnPair = pair;
                        break;
                    }
                }
                string winName = Regex.Split(wnPair, @"=")[1];
                return Uri.UnescapeDataString(winName).Replace('+', ' ');

            } catch (Exception) {
            }
            return "";
        }

        public string StrategyConfig {
            set {
                ApplyStrategyConfig(value);
            }
            get {
                return ConstructStrategyConfig();
            }
        }

        public static AutoTradeDefinition CreateFromConfig(string dataConfig, string strategyConfig) {
            AutoTradeDefinition def = new AutoTradeDefinition();
            def.TiUrl = dataConfig;
            def.WindowName = ParseWindowName(dataConfig);

            def.ApplyStrategyConfig(strategyConfig);

            return def;
        }

        private string ConstructStrategyConfig() {
            string config = ""
                + "PriceMoveDirection=" + (EntryDirection == Direction.Long ? 0 : 1)
                + "&MoveType=" + (Units == TradeUnit.Points ? 0 : 1)
                + "&DollarsValue=" + DollarsValue
                + "&PercentsValue=" + PercentsValue
                + "&UseProfitTarget=" + (UseProfitTarget ? 1 : 0)
                + "&ProfitTarget=" + ProfitTarget
                + "&UseStopLoss=" + (UseStopLoss ? 1 : 0)
                + "&UseStopLossWiggle=" + (UseStopWiggle ? 1 : 0)
                + "&StopLoss=" + StopLoss
                + "&ExitConditionType=" + (OtherExitCondition == ExitCondition.None ? "0" :
                    (OtherExitCondition == ExitCondition.PercTrailStop ? "1" :
                        (OtherExitCondition == ExitCondition.BarsTrailStop ? "2" : "4")))
                + "&ExitConditionPercent=" + PercTrailStop
                + "&ExitConditionBars=" + BarsTrailStop
                + "&EntryHoursStart=" + StartHrs
                + "&EntryMinutesStart=" + StartMins
                + "&EntryHoursEnd=" + EndHrs
                + "&EntryMinutesEnd=" + EndMins
                + "&ExitTimeType=" + (TimeExitType == ExitTimeType.MinsAfterEntry ? "0" :
                    (TimeExitType == ExitTimeType.MinsBeforeClose ? "1" : "2"))
                + "&ExitTimeMinutesAfter=" + MinsAfterEntry
                + "&ExitTimeMinutesBefore=" + MinsBeforeClose;
            return config;
        }

        private void ApplyStrategyConfig(string strategyConfig) {
            string[] pairs = Regex.Split(strategyConfig, @"\&");
            foreach (string pair in pairs) {
                string[] keyval = Regex.Split(pair, @"=");
                string key = keyval[0];
                string strValue = keyval[1];
                double dVal = 0;
                try { dVal = Convert.ToDouble(strValue); } catch (Exception) { }
                int value = 0;
                try { value = Convert.ToInt32(strValue); } catch (Exception) { }
                switch (key) {
                    case "PriceMoveDirection":
                        EntryDirection = (value == 0 ? Direction.Long : Direction.Short);
                        break;
                    case "MoveType":
                        Units = (value == 0 ? TradeUnit.Points : TradeUnit.Percent);
                        break;
                    case "DollarsValue":
                        DollarsValue = dVal;
                        break;
                    case "PercentsValue":
                        PercentsValue = dVal;
                        break;
                    case "UseProfitTarget":
                        UseProfitTarget = value == 1;
                        break;
                    case "ProfitTarget":
                        ProfitTarget = dVal;
                        break;
                    case "UseStopLoss":
                        UseStopLoss = value == 1;
                        break;
                    case "UseStopLossWiggle":
                        UseStopWiggle = value == 1;
                        break;
                    case "StopLoss":
                        StopLoss = dVal;
                        break;
                    case "ExitConditionType":
                        switch (value) {
                            case 0:
                                OtherExitCondition = ExitCondition.None;
                                break;
                            case 1:
                                OtherExitCondition = ExitCondition.PercTrailStop;
                                break;
                            case 2:
                                OtherExitCondition = ExitCondition.BarsTrailStop;
                                break;
                            case 4:
                                OtherExitCondition = ExitCondition.Alert;
                                break;
                            default:
                                OtherExitCondition = ExitCondition.None;
                                break;
                        }
                        break;
                    case "ExitConditionAlert":
                        ExitConditionAlert = strValue;
                        break;
                    case "ExitConditionPercent":
                        PercTrailStop = dVal;
                        break;
                    case "ExitConditionBars":
                        BarsTrailStop = dVal;
                        break;
                    case "EntryHoursStart":
                        StartHrs = value;
                        break;
                    case "EntryMinutesStart":
                        StartMins = value;
                        break;
                    case "EntryHoursEnd":
                        EndHrs = value;
                        break;
                    case "EntryMinutesEnd":
                        EndMins = value;
                        break;
                    case "ExitTimeType":
                        switch (value) {
                            case 0:
                                TimeExitType = ExitTimeType.MinsAfterEntry;
                                break;
                            case 1:
                                TimeExitType = ExitTimeType.MinsBeforeClose;
                                break;
                            case 2:
                                TimeExitType = ExitTimeType.AtNextOpen;
                                break;
                            default:
                                TimeExitType = ExitTimeType.MinsAfterEntry;
                                break;
                        }
                        break;
                    case "ExitTimeMinutesAfter":
                        MinsAfterEntry = value;
                        break;
                    case "ExitTimeMinutesBefore":
                        MinsBeforeClose = value;
                        break;
                }

            }
        }


    }
    public class AlertArgs : EventArgs
    {
        public readonly AlertData alert;

        public AlertArgs(AlertData alert) {
            this.alert = alert;
        }
    }

}
