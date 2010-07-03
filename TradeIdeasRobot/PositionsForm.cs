using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//using AdvancedDataGridView;

namespace TradeIdeasWindowApp
{
    public partial class PositionsForm : Form
    {
        DataTable positionData;

        private TwsFront twsFront;

        public PositionsForm( ) {
            InitializeComponent();
            CustomizeComponent();
        }


        private void CustomizeComponent() {
            positionData = new DataTable("table");
            positionData.Columns.Add(new DataColumn("Symbol", Type.GetType("System.String")));
            positionData.Columns.Add(new DataColumn("Shares", typeof(int)));
            positionData.Columns.Add(new DataColumn("Real", typeof(double)));
            positionData.Columns.Add(new DataColumn("Unreal", typeof(double)));
            positionData.Columns.Add(new DataColumn("Avg$", typeof(double)));
            positionData.Columns.Add(new DataColumn("%", typeof(double)));
            positionData.Columns.Add(new DataColumn("Bid", typeof(double)));
            positionData.Columns.Add(new DataColumn("Ask", typeof(double)));
            positionData.Columns.Add(new DataColumn("Last", typeof(double)));
            dataGridPositions.DataSource = positionData;

            int[] widths = { 50, 50, 50, 50, 50, 50, 50, 50, 50 };
            for (int i = 0; i < widths.Length; i++) {
                dataGridPositions.Columns[i].Width = widths[i];
            }

            // testing row
            //DataRow row = positionData.NewRow();
            //row["Symbol"] = "GOOG";
            //row["Shares"] = 100;
            //row["Real"] = 0;
            //row["Unreal"] = -129.30;
            //row["Avg$"] = 300.29;
            //row["%"] = -0.5;
            //row["Bid"] = 295.27;
            //row["Ask"] = 295.80;
            //row["Last"] = 295.27;
            //positionData.Rows.Add(row);

            //row = positionData.NewRow();
            //row["Symbol"] = "FSLR";
            //row["Shares"] = 200;
            //row["Real"] = 0;
            //row["Unreal"] = 230.30;
            //row["Avg$"] = 290.29;
            //row["%"] = 1.5;
            //row["Bid"] = 322.27;
            //row["Ask"] = 322.80;
            //row["Last"] = 322.27;
            //positionData.Rows.Add(row);

        }

        private void PositionsForm_Shown(object sender, EventArgs e) {
        }

        private void timer_Tick(object sender, EventArgs e) {
            foreach (DataRow row in positionData.AsEnumerable() ) {
                UpdateRow(row);
            }
        }


        public TwsFront TwsFront {
            get { return twsFront; }
            set { 
                this.twsFront = value;
                // subscribe to data
                twsFront.PositionChanged += new EventHandler<PositionEventArgs>(twsFront_PositionChanged);

                // add current positions
                foreach (string symbol in twsFront.GetAllPositions()) {
                    DataRow row = positionData.NewRow();
                    row["Symbol"] = symbol;
                    positionData.Rows.Add(row);
                    UpdateRow(row);
                }
            }
        }

        private void UpdateRow(DataRow row) {
            string symbol = (string)row["Symbol"];
            MktData mktData = twsFront.GetMarketData(symbol);
            Position pos = twsFront.GetPosition(symbol);
            row["Shares"] = pos.shares;
            row["Real"] = 0;
            row["Unreal"] = 0;
            row["Avg$"] = pos.avgPrice;
            double change = pos.shares > 0 ? (mktData.last-pos.avgPrice)/pos.avgPrice : (pos.avgPrice-mktData.last)/pos.avgPrice;
            row["%"] = change * 100.0;
            row["Bid"] = mktData.bid;
            row["Ask"] = mktData.ask;
            row["Last"] = mktData.last;
        }

        void twsFront_PositionChanged(object sender, PositionEventArgs e) {
            if (e.Type == PositionEventType.PositionOpened) {
                // add
                DataRow row = positionData.NewRow();
                row["Symbol"] = e.Symbol;
                positionData.Rows.Add(row);
                UpdateRow(row);

            } else {
                // remove
            }
        }

    }
}
