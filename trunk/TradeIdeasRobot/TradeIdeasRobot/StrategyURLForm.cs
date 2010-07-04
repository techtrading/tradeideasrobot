using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TradeIdeasRobot
{
    public partial class StrategyURLForm : Form
    {
        public string StrategyURL;

        public StrategyURLForm()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            StrategyURL = this.txtURL.Text;
            this.Close();
        }
    }
}
