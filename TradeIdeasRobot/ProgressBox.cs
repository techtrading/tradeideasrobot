using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradeIdeasWindowApp
{
    public partial class ProgressBox : Form
    {
        bool cancelled = false;
        DateTime startTime = DateTime.Now;

        public ProgressBox() {
            InitializeComponent();
        }

        private void SetInfo() {
            if (progressBar1.Value > 0) {
                TimeSpan span = DateTime.Now - startTime;
                double estPer = ((double)span.Milliseconds) / ((double)progressBar1.Value);
                double estTotal = estPer * ((double)progressBar1.Maximum);
                TimeSpan totSpan = new TimeSpan((long)estTotal);
                this.lblInfo.Text = "Trial " + progressBar1.Value + " of " + progressBar1.Maximum +
                    ", estimated completion: " + (startTime + totSpan).ToLongTimeString();
            } else {
                this.lblInfo.Text = "Trial " + progressBar1.Value + " of " + progressBar1.Maximum;
            }
        }

        public int Maximum {
            get { return this.progressBar1.Maximum; }
            set { 
                this.progressBar1.Maximum = value;
                SetInfo();                
            }
        }

        public int Value {
            get { return this.progressBar1.Value; }
            set { 
                this.progressBar1.Value = value;
                SetInfo();
            }
        }
        public bool IsCancelled {
            get { return this.cancelled; }
            set { this.cancelled = value; }
        }

        private void btnCancel_Click(object sender, EventArgs e) {
            this.lblInfo.Text = "Cancelling...";
            this.cancelled = true;
        }

    }
}
