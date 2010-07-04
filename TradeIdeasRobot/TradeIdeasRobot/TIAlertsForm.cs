using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NLog;

namespace TradeIdeasRobot
{
    public partial class TIAlertsForm : Form
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public bool IsOk = false;
        public BotForm BotForm;


        public TIAlertsForm()
        {
            InitializeComponent();
        }

        public TIAlertsForm(string strategyUrl)
            : this()
        {
            axTradeIdeasWindowX1.DataConfig = strategyUrl;
        }
        public TIAlertsForm(string strategyUrl, string strategyConfig)
            : this(strategyUrl)
        {
            axTradeIdeasWindowX1.StrategyConfig = strategyConfig;
        }

        public AxTradeIdeasWindow.AxTradeIdeasWindowX TIWindow
        {
            get { return this.axTradeIdeasWindowX1; }
        }

        public string DataConfig
        {
            get { return axTradeIdeasWindowX1.DataConfig; }
            set { axTradeIdeasWindowX1.DataConfig = value; }
        }
        public string StrategConfig
        {
            get { return axTradeIdeasWindowX1.StrategyConfig; }
            set { axTradeIdeasWindowX1.StrategyConfig = value; }
        }

        private void axTradeIdeasWindowX1_OnWindowNameChanged(object sender, EventArgs e)
        {
            this.Text = axTradeIdeasWindowX1.WindowName;
            log.Debug("Trade Ideas windo name changed, new value [" + axTradeIdeasWindowX1.WindowName + "], e=" + e);
        }

        //private void btnTrade_Click(object sender, EventArgs e)
        //{
        //    AutoTradeDefinition def = AutoTradeDefinition.CreateFromConfig(DataConfig, StrategConfig);
        //    BotForm.AddTradeDefinition(def);
        //}

        private void TIAlertsForm_Load(object sender, EventArgs e)
        {
            this.Text = axTradeIdeasWindowX1.WindowName;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.IsOk = false;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.IsOk = true;
            this.Close();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AutoTradeDefinition def = AutoTradeDefinition.CreateFromConfig(DataConfig, StrategConfig);
            BotForm.AddTradeDefinition(def);
        }

    }
}
