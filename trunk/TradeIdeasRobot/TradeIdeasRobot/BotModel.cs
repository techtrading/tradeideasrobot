using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeIdeasRobot
{
    public class BotModel
    {
        public string TradeIdeasUsername;
        public string TradeIdeasPassword;

        public string IbHost;
        public int IbPort;
        public string IbSimHost;
        public int IbSimPort;

        public int IbQuoteTimeout;
        public bool TransmitOrders;
        public bool AllowDuplicates;
        public bool BeepOnOrder = false;

        public int MktStartHrs = 6;
        public int MktStartMins = 30;
        public int MktCloseHrs = 13;
        public int MktCloseMins = 0;
        public int MinTradeMins = 2;

        public List<AutoTradeDefinition> Strategies;

        public BotModel()
        {
            Strategies = new List<AutoTradeDefinition>();
        }

    }
}
