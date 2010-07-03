using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using TradeIdeasWindow;

using System.Data.Common;
using System.Xml.Serialization;
using NLog;
using System.Threading;

namespace TradeIdeasWindowApp
{
    public partial class BotForm : Form
    {
        private static Logger log = LogManager.GetCurrentClassLogger();

        private bool IsDirty = false;

        //List<TradeIdeasWindowX> tiWindows = null;
        DataTable stratData;
        //List<AutoTradeDefinition> tradeDefinitions;
        BotModel model;
        public BotController controller;
        public bool IsTwsSimConnected = false;
        public bool IsTwsLiveConnected = false;
        public bool IsTIConnected = false;

        private TIWindowLayout windowLayout;

        public BotForm() {
            InitializeComponent();
            CustomizeComponent();
            windowLayout = new TIWindowLayout();
            windowLayout.BotForm = this;
        }

        public BotController Controller
        {
            get { return controller;  }
            set { 
                controller = value;
                controller.ModelUpdated += new EventHandler<ModelEventArgs>(controller_ModelUpdated);
                controller.OnMessage += new EventHandler<MessageEventArgs>(controller_OnMessage);
                controller.Tws = this.axTws1;
                controller.TwsSim = this.axTwsSim;
            }
        }

        void addMessage(string message) {
            listBoxOutput.Items.Insert(0, DateTime.Now + ": " + message);
        }

        void controller_OnMessage(object sender, MessageEventArgs e)
        {
            addMessage(e.Message);
        }


        private void CustomizeComponent()
        {
            stratData = new DataTable("table");
            stratData.Columns.Add(new DataColumn("Enabled", typeof(bool)));
            stratData.Columns.Add(new DataColumn("Mode", Type.GetType("System.String")));
            stratData.Columns.Add(new DataColumn("Window Name", Type.GetType("System.String")));
            stratData.Columns.Add(new DataColumn("Tags", Type.GetType("System.String")));
            strategyGridView.DataSource = stratData;

            strategyGridView.Columns[0].Width = 30;
            strategyGridView.Columns[1].Width = 30;
            strategyGridView.Columns[2].Width = 200; 
            strategyGridView.Columns[3].Width = 200;

            // add the combo column
            DataGridViewComboBoxColumn modeCol = new DataGridViewComboBoxColumn();
            //modeCol.Items.AddRange(RunMode.Live, RunMode.Sim);
            modeCol.Items.AddRange("Live", "Sim");
            //modeCol.DefaultCellStyle.NullValue = "Null";
            modeCol.HeaderText = "Mode";
            modeCol.DataPropertyName = "Mode";
            modeCol.DropDownWidth = 100;
            modeCol.Width = 50;

            strategyGridView.Columns.Remove("Mode");
            strategyGridView.Columns.Insert(1, modeCol);


            for (int i = 2; i < 4; i++)
            {
                strategyGridView.Columns[i].ReadOnly = true;
            }

            strategyGridView.CellEndEdit += new DataGridViewCellEventHandler(strategyGridView_CellEndEdit);

            strategyGridView.AllowUserToAddRows = false;
            strategyGridView.RowHeadersVisible = false;
        }

        void strategyGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e) {
            log.Debug("cell edited, setting dirty = true");
            IsDirty = true;
            if (e.ColumnIndex == 0) {
                model.Strategies[e.RowIndex].IsEnabled = (bool)strategyGridView.CurrentCell.Value;
            } else if( e.ColumnIndex == 1 ) {
                model.Strategies[e.RowIndex].Mode = (RunMode)Enum.Parse(typeof(RunMode), (string)strategyGridView.CurrentCell.Value, true);
            }
        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {

        }

        public void AddTradeDefinition(AutoTradeDefinition def) {
            //IsDirty = true;
            model.Strategies.Add(def);
            def.OnAlert += new EventHandler<AlertArgs>(def_OnAlert);
            // TODO: connect or not?
            def.Connect(model);

            DataRow row = stratData.NewRow();
            row["Enabled"] = def.IsEnabled;
            row["Mode"] = def.Mode;
            row["Window Name"] = def.WindowName;
            row["Tags"] = def.Tags;

            stratData.Rows.Add(row);
        }

        private void btnAddStrat_Click(object sender, EventArgs e){
            StrategyDefForm stratForm = new StrategyDefForm();
            stratForm.ShowDialog();
            if (stratForm.IsOk) {
                log.Debug("added strategy, setting dirty = true");
                IsDirty = true;
                AutoTradeDefinition def = stratForm.TradeDefinition;
                AddTradeDefinition(def);
                def.Connect(model);
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (strategyGridView.SelectedCells != null && strategyGridView.SelectedCells.Count <= 0){
                // no selection
                return;
            }
            int row = strategyGridView.SelectedCells[0].RowIndex;
            AutoTradeDefinition initDefn = model.Strategies[row];

            StrategyDefForm stratForm = new StrategyDefForm();
            stratForm.TradeDefinition = initDefn;
            stratForm.ShowDialog();
            if (stratForm.IsOk)
            {
                log.Debug("edited strategy, setting dirty = true");
                IsDirty = true;
                initDefn.Disconnect();
                initDefn.OnAlert -= new EventHandler<AlertArgs>(def_OnAlert);

                AutoTradeDefinition defn = stratForm.TradeDefinition;
                defn.Mode = initDefn.Mode;
                defn.OnAlert += new EventHandler<AlertArgs>(def_OnAlert);
                model.Strategies[row] = defn;
                // TODO: connect or not?
                defn.Connect(model);

                DataRow drow = stratData.Rows[row];
                drow["Enabled"] = model.Strategies[row].IsEnabled;
                drow["Mode"] = model.Strategies[row].Mode;
                drow["Window Name"] = model.Strategies[row].WindowName;
                drow["Tags"] = model.Strategies[row].Tags;
            }
        }

        private void saveSettings() {
            controller.SaveModel();
            IsDirty = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveSettings();
            
        }

        void controller_ModelUpdated(object sender, ModelEventArgs e)
        {
            // on 'model update' (ie: load), if connected, remove events for all existing models
            IsDirty = false;
            if (model != null) {
                foreach (AutoTradeDefinition def in model.Strategies) {
                    def.OnAlert -= new EventHandler<AlertArgs>(def_OnAlert);
                    def.Disconnect();
                }
            }
                
                
            model = e.Model;
            stratData.Rows.Clear();

            foreach (AutoTradeDefinition def in model.Strategies) {
                DataRow row = stratData.NewRow();
                row["Enabled"] = def.IsEnabled;
                row["Mode"] = def.Mode.ToString();
                row["Window Name"] = def.WindowName;
                row["Tags"] = def.Tags;

                stratData.Rows.Add(row);
                def.OnAlert += new EventHandler<AlertArgs>(def_OnAlert);

                // TODO: connect or not?
                def.Connect(model);
            }
            tbTIUser.Text = model.TradeIdeasUsername;
            tbTIPassword.Text = model.TradeIdeasPassword;
            tbIBHost.Text = model.IbHost;
            tbIBPortNum.Text = Convert.ToString(model.IbPort);
            tbQuoteTimeout.Text = Convert.ToString(model.IbQuoteTimeout);
            tbStartMins.Text = Convert.ToString(model.MktStartMins);
            tbStartHrs.Text = Convert.ToString(model.MktStartHrs);
            tbCloseMins.Text = Convert.ToString(model.MktCloseMins);
            tbCloseHrs.Text = Convert.ToString(model.MktCloseHrs);
            tbSimHost.Text = model.IbSimHost;
            tbSimPort.Text = Convert.ToString(model.IbSimPort);
            try { cbTransmit.Checked = model.TransmitOrders; } catch (Exception) { }
            try { cbDuplicates.Checked = model.AllowDuplicates; } catch (Exception) { }
            try { cbBeepOnOrder.Checked = model.BeepOnOrder; } catch (Exception) { }

        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            Controller.LoadModel();
        }

        private void ConnectTwsLive() {
            // todo: should wait for feedback from socket
            this.IsTwsLiveConnected = true;
            Controller.ConnectToTwsLive();
        }
        private void ConnectTwsSim() {
            // todo: should wait for feedback from socket
            this.IsTwsSimConnected = true;
            Controller.ConnectToTwsSim();
        }
        private void ConnectTI() {
            this.IsTIConnected = true;
            // listens to live and enabled alerts and sends out the alerts
            axTradeIdeasWindowX1.Connection.UserName = tbTIUser.Text;
            axTradeIdeasWindowX1.Connection.Password = tbTIPassword.Text;

            //if (tiWindows == null) {
                //tiWindows = new List<TradeIdeasWindowX>();
                foreach (AutoTradeDefinition def in model.Strategies) {
                    //def.OnAlert += new EventHandler<AlertArgs>(def_OnAlert);
                    if (def.IsEnabled) {
                        def.Connect(model);
                    }
                }
            //}
        }

        private void DisconnectTI() {
            // TODO
        }

        private void btnLoginAll_Click(object sender, EventArgs e)
        {
            ConnectTwsSim();
            ConnectTwsLive();
            ConnectTI();
        }

        private void fireDummyAlert() {
            AutoTradeDefinition def = new AutoTradeDefinition();
            def.IsEnabled = true;
            def.Mode = RunMode.Sim;
            def.EntryDirection = Direction.Long;
            def.Units = TradeUnit.Percent;
            def.OtherExitCondition = ExitCondition.None;
            def.TimeExitType = ExitTimeType.MinsAfterEntry;
            def.MinsAfterEntry = 20;
            def.StartHrs = 0;
            def.StartMins = 5;
            def.EndHrs = 0;
            def.EndMins = 25;
            def.PositionSizing = PositionSizingType.FixedShares;
            def.Sizing = 100;
            def.MaxShares = 1000;
            def.SharesIncrement = 10;
            def.EntryOrderType = OrderType.Market;
            def.UseGoodForSecs = true;
            def.GoodForSecs = 10;
            def.Tags = "ATS, test, dummy";
            Controller.HandleAlert("IBM", def);
        }
        void def_OnAlert(object sender, AlertArgs e) {
            AutoTradeDefinition def = (AutoTradeDefinition)sender;
            addMessage("new alert, window: " + def.WindowName
                + " symbol: " + e.alert.Symbol
                + ", type: " + e.alert.AlertType
                + ", desc: " + e.alert.Description
                + ", relVol: " + e.alert.RelVol
                + ", count: " + e.alert.Count);
            Controller.HandleAlert(e.alert.Symbol, def);
        }

        private void settingsTextChanged(object sender, EventArgs e) {
            log.Debug("settingsTextChanged, setting dirty = true, sender=" + sender);
            IsDirty = true;
            if( sender == tbQuoteTimeout )
                try { model.IbQuoteTimeout = Convert.ToInt32(tbQuoteTimeout.Text); } catch { }
            else if( sender == cbTransmit )
                model.TransmitOrders = cbTransmit.Checked;
            else if( sender == cbDuplicates ) 
                model.AllowDuplicates = cbDuplicates.Checked;
            else if (sender == cbBeepOnOrder)
                model.BeepOnOrder = cbBeepOnOrder.Checked;
            else if (sender == tbStartMins) 
                try { model.MktStartMins = Convert.ToInt32(tbStartMins.Text); } catch { }
            else if( sender == tbStartHrs ) 
                try { model.MktStartHrs = Convert.ToInt32(tbStartHrs.Text); } catch { }
            else if( sender == tbCloseMins ) 
                try { model.MktCloseMins = Convert.ToInt32(tbCloseMins.Text); } catch { }
            else if( sender == tbCloseHrs ) 
                try { model.MktCloseHrs = Convert.ToInt32(tbCloseHrs.Text); } catch { }
        }
        private void loginTextChanged(object sender, EventArgs e) {
            log.Debug("loginTextChanged, setting dirty = true");
            IsDirty = true;
            if (sender == tbTIUser)
                model.TradeIdeasUsername = tbTIUser.Text;
            if (sender == tbTIPassword)
                model.TradeIdeasPassword = tbTIPassword.Text;
            if (sender == tbIBHost)
                model.IbHost = tbIBHost.Text;
            if( sender == tbIBPortNum ) 
                try { model.IbPort = Convert.ToInt32(tbIBPortNum.Text); } catch { }
            if( sender == tbSimHost ) 
                model.IbSimHost = tbSimHost.Text;
            if( sender == tbSimPort ) 
                try { model.IbSimPort = Convert.ToInt32(tbSimPort.Text); } catch { }
        }

        private void BotForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (IsDirty) {
                DialogResult res = MessageBox.Show("Do you want to save your settings before exiting?",
                    "Confirm exit", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes) {
                    saveSettings();
                    IsDirty = false;
                }
                e.Cancel = (res == DialogResult.Cancel);
            }
            IsDirty = false;
            // exlicitly exit to kill any lingering threads which may still be running


            controller.DisconnectAll();
            Application.Exit();
        }


        private void newAlertMenuItem_Click(object sender, EventArgs e) {
            windowLayout.AddNewWindow();
        }

        private void openWindowsMenuItem_Click(object sender, EventArgs e) {
            windowLayout.OpenLayout();
        }

        private void saveWindowsMenuItem_Click(object sender, EventArgs e) {
            windowLayout.SaveLayout();
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if (strategyGridView.SelectedCells != null && strategyGridView.SelectedCells.Count <= 0) {
                MessageBox.Show("Error", "Please select a strategy to delete.");
                return;
            }
            int row = strategyGridView.SelectedCells[0].RowIndex;
            AutoTradeDefinition defn = model.Strategies[row];
            IsDirty = true;
            defn.IsEnabled = false;
            // todo: remove event listener
            defn.OnAlert -= new EventHandler<AlertArgs>(def_OnAlert);
            defn.Disconnect();
            model.Strategies.Remove(defn);
            stratData.Rows.RemoveAt(row);
        }

        private void btnOpenAlerts_Click(object sender, EventArgs e) {
            if (strategyGridView.SelectedCells != null && strategyGridView.SelectedCells.Count <= 0) {
                MessageBox.Show("Error", "Please select a strategy to open.");
                return;
            }
            int row = strategyGridView.SelectedCells[0].RowIndex;
            AutoTradeDefinition defn = model.Strategies[row];
            //windowLayout.AddNewWindow(defn);
            TIAlertsWindow w = new TIAlertsWindow();
            w.BotForm = this;
            w.DataConfig = defn.TiUrl;
            w.StrategConfig = defn.StrategyConfig;
            w.ShowDialog();
            if (w.IsOk) {
                defn.Disconnect();
                defn.IsEnabled = false;
                defn.TiUrl = w.DataConfig;
                defn.StrategyConfig = w.StrategConfig;
                // TODO: shouldn't have to manually set this!
                defn.WindowName = AutoTradeDefinition.ParseWindowName(w.DataConfig);

                //model.Strategies.Remove(defn);
                //stratData.Rows.RemoveAt(row);
                model.Strategies[row] = defn;
                // TODO: connect or not?
                //defn.Disconnect(model);

                DataRow drow = stratData.Rows[row];
                drow["Enabled"] = model.Strategies[row].IsEnabled;
                drow["Mode"] = model.Strategies[row].Mode;
                drow["Window Name"] = model.Strategies[row].WindowName;
                drow["Tags"] = model.Strategies[row].Tags;
            }
        }


        private void btnUp_Click(object sender, EventArgs e) {
            if (strategyGridView.SelectedCells != null && strategyGridView.SelectedCells.Count <= 0) {
                MessageBox.Show("Error", "Please select a strategy to open.");
                return;
            }
            int row = strategyGridView.SelectedCells[0].RowIndex;
            if( row == 0 ) { 
                // already at the top
                return;
            }
            swapRows(row, row - 1); 
        }

        private void updateDataRow(DataRow drow, AutoTradeDefinition a) {
            drow["Enabled"] = a.IsEnabled;
            drow["Mode"] = a.Mode;
            drow["Window Name"] = a.WindowName;
            drow["Tags"] = a.Tags;
        }

        private void btnDown_Click(object sender, EventArgs e) {
            if (strategyGridView.SelectedCells != null && strategyGridView.SelectedCells.Count <= 0) {
                MessageBox.Show("Error", "Please select a strategy to open.");
                return;
            }
            int row = strategyGridView.SelectedCells[0].RowIndex;
            if (row >= model.Strategies.Count - 1) {
                // already at the bottom
                return;
            }
            swapRows(row, row + 1);
        }

        private void swapRows(int idx1, int idx2) {
            AutoTradeDefinition a = model.Strategies[idx1];
            AutoTradeDefinition b = model.Strategies[idx2];
            
            model.Strategies[idx1] = b;
            model.Strategies[idx2] = a;
            updateDataRow(stratData.Rows[idx1], b);
            updateDataRow(stratData.Rows[idx2], a);
            this.strategyGridView.Rows[idx2].Selected = true;
            this.strategyGridView.CurrentCell = this.strategyGridView.Rows[idx2].Cells[0];
            this.strategyGridView.Rows[idx1].Selected = false;
        }

        private void BotForm_Load(object sender, EventArgs e) {
            IsDirty = false;
        }

		private void btnTest_Click(object sender, EventArgs e) {
			controller.RunTest();
		}

    }

    public class TIWindowLayout
    {
        public List<TIWindowState> AlertStates;

        [XmlIgnore]
        private List<TIAlertsWindow> alertWindows;
        private static Logger log = LogManager.GetCurrentClassLogger();

        [XmlIgnore]
        public BotForm BotForm;

        public TIWindowLayout() {
            AlertStates = new List<TIWindowState>();
            alertWindows = new List<TIAlertsWindow>();
        }

        public TIAlertsWindow AddNewWindow() {
            TIAlertsWindow w = new TIAlertsWindow();
            w.BotForm = BotForm;
            alertWindows.Add(w);
            w.Show();
            return w;
        }
        public void AddNewWindow(AutoTradeDefinition def) {
            TIAlertsWindow w = new TIAlertsWindow();
            w.BotForm = BotForm;
            w.DataConfig = def.TiUrl;
            w.StrategConfig = def.StrategyConfig;
            alertWindows.Add(w);
            w.Show();
        }

        public void OpenLayout() {
            foreach (TIAlertsWindow window in alertWindows) {
                if (window.Visible) {
                    window.Close();
                }
            }
            alertWindows.Clear();

            FileInfo f = new FileInfo("alertLayout.xml");
            TIWindowLayout tmp;
            try {
                XmlSerializer x = new XmlSerializer(this.GetType());
                tmp = (TIWindowLayout)x.Deserialize(f.OpenRead());
                log.Debug("Loaded TIWindowLayout");
            } catch (Exception ex) {
                log.Error("Could not load " + f.FullName + ": " + ex);
                tmp = new TIWindowLayout();
            }
            foreach (TIWindowState state in tmp.AlertStates) {
                TIAlertsWindow w = new TIAlertsWindow();
                w.BotForm = BotForm;
                w.DataConfig = state.DataConfig;
                w.StrategConfig = state.StrategyConfig;
                w.Size = state.Size;
                w.Location = state.Location;
                //w.Size.Width = state.Width;
                //w.Size.Height = state.Height;
                //w.Location.X = state.X;
                //w.Location.Y = state.Y;
                alertWindows.Add(w);
                w.Show();
            }
        }
        public void SaveLayout() {
            AlertStates = new List<TIWindowState>();
            foreach (TIAlertsWindow w in alertWindows) {
                TIWindowState state = new TIWindowState();
                state.DataConfig = w.DataConfig;
                state.StrategyConfig = w.StrategConfig;
                state.Location = w.Location;
                state.Size = w.Size;
                //state.X = w.Location.X;
                //state.Y = w.Location.Y;
                //state.Height = w.Size.Height;
                //state.Width = w.Size.Width;
                AlertStates.Add(state);
            }
            try {
                XmlSerializer x = new XmlSerializer(this.GetType());
                FileInfo f = new FileInfo("alertLayout.xml");
                x.Serialize(f.Create(), this);
                log.Debug("Successfully saved TIWindowStates to " + f.FullName);
                MessageBox.Show("The window states were saved successfully", "Success");
            } catch (Exception ex) {
                MessageBox.Show("Save error", "There was a problem saving the windows:\n" + ex.Message);
                log.Error("Could not save the TIWindowStates: " + ex);
            }
        }
    }

    public class TIWindowState
    {
        public string DataConfig;
        public string StrategyConfig;
        public Size Size;
        public Point Location;
        //public int X, Y, Width, Height;
    }
  
}
