using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using NLog;

namespace TradeIdeasWindowApp
{
    public partial class TIAlertsWindow : Form
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        public bool IsOk = false;
        //private string dataConfig;
        //private string strategyConfig;

        public BotForm BotForm;

        public TIAlertsWindow() {
            InitializeComponent();
            axTradeIdeasWindowX1.NotifyOnWindowNameChanged = true;
        }

        public TIAlertsWindow(string strategyUrl)
            : this() {
            axTradeIdeasWindowX1.DataConfig = strategyUrl;
        }
        public TIAlertsWindow(string strategyUrl, string strategyConfig)
            : this(strategyUrl) {
            axTradeIdeasWindowX1.StrategyConfig = strategyConfig;
        }

        public AxTradeIdeasWindow.AxTradeIdeasWindowX TIWindow {
            get { return this.axTradeIdeasWindowX1; }
        }

        public string DataConfig {
            get { return axTradeIdeasWindowX1.DataConfig; }
            set { axTradeIdeasWindowX1.DataConfig = value; }
        }
        public string StrategConfig {
            get { return axTradeIdeasWindowX1.StrategyConfig; }
            set { axTradeIdeasWindowX1.StrategyConfig = value; }
        }

        private void axTradeIdeasWindowX1_OnWindowNameChanged(object sender, EventArgs e) {
            this.Text = axTradeIdeasWindowX1.WindowName;
            log.Debug("Trade Ideas windo name changed, new value [" + axTradeIdeasWindowX1.WindowName + "], e=" + e);
        }

        private void btnTrade_Click(object sender, EventArgs e) {
            AutoTradeDefinition def = AutoTradeDefinition.CreateFromConfig(DataConfig, StrategConfig);
            BotForm.AddTradeDefinition(def);
        }

        private void TIAlertsWindow_Load(object sender, EventArgs e) {
            this.Text = axTradeIdeasWindowX1.WindowName;
        }

        private void button1_Click(object sender, EventArgs e) {
            string c = axTradeIdeasWindowX1.DataConfig;
            //string c = axTradeIdeasWindowX1.StrategyConfig;
            Clipboard.SetDataObject(c);
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.IsOk = false;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e) {
            this.IsOk = true;
            this.Close();
        }

    }
}
