using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace TradeIdeasWindowApp
{
    public partial class StrategyDefForm : Form
    {
        public bool IsOk = false;
        AutoTradeDefinition tradeDefinition;

        public StrategyDefForm()
        {
            InitializeComponent();
        }


        private void SwitchPointsPercent()
        {
            if (rbPoints.Checked)
            {
                lblTargetMove.Text = "";
                lblStoploss.Text = "";
                lblTargetDlr.Text = "$";
                lblStopDlr.Text = "$";
                lblOffsetStyle.Text = "$";
            }
            else
            {
                lblTargetMove.Text = "%";
                lblStoploss.Text = "%";
                lblTargetDlr.Text = "";
                lblStopDlr.Text = "";
                lblOffsetStyle.Text = "%";
            }
        }
        private void rbPoints_CheckedChanged(object sender, EventArgs e)
        {
            SwitchPointsPercent();
        }

        private void rbPercent_CheckedChanged(object sender, EventArgs e)
        {
            SwitchPointsPercent();
        }

        private void SwitchDirection()
        {
            if (rbUp.Checked)
            {
                cbTarget.Text = "Sell if price moves up";
                cbStoploss.Text = "Sell if price moves down";
            }
            else
            {
                cbTarget.Text = "Cover if price moves down";
                cbStoploss.Text = "Cover if price moves up";
            }
        }
        private void rbUp_CheckedChanged(object sender, EventArgs e)
        {
            SwitchDirection();
        }

        private void rbDown_CheckedChanged(object sender, EventArgs e)
        {
            SwitchDirection();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            IsOk = false;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            IsOk = true;
            tradeDefinition = new AutoTradeDefinition();
            tradeDefinition.EntryDirection = rbUp.Checked ? Direction.Long : Direction.Short;
            tradeDefinition.Units = rbPoints.Checked ? TradeUnit.Points : TradeUnit.Percent;
            try { tradeDefinition.DollarsValue = Convert.ToDouble(tbPoints.Text); } catch { }
            try { tradeDefinition.PercentsValue = Convert.ToDouble(tbPercent.Text); } catch { }

            tradeDefinition.UseProfitTarget = cbTarget.Checked;
            try { tradeDefinition.ProfitTarget = Convert.ToDouble(tbProfitTarget.Text); } catch { }
            tradeDefinition.UseStopLoss = cbStoploss.Checked;
            try { tradeDefinition.StopLoss = Convert.ToDouble(tbStoploss.Text); } catch { }
            tradeDefinition.UseStopWiggle = cbWiggle.Checked;

            if (rbPercTrailingStop.Checked){
                tradeDefinition.OtherExitCondition = ExitCondition.PercTrailStop;
            }else if (rbBarTrailStop.Checked){
                tradeDefinition.OtherExitCondition = ExitCondition.BarsTrailStop;
            }else if( rbAnotherAlert.Checked) {
                tradeDefinition.OtherExitCondition = ExitCondition.Alert;
            } else {
                tradeDefinition.OtherExitCondition = ExitCondition.None;
            }

            try { tradeDefinition.BarsTrailStop = Convert.ToDouble(tbBarTrailStop.Text); } catch { }
            try { tradeDefinition.PercTrailStop = Convert.ToDouble(tbTrailStop.Text); } catch { }

            if (rbExitMinsAfterEntry.Checked) {
                tradeDefinition.TimeExitType = ExitTimeType.MinsAfterEntry;
                try { tradeDefinition.MinsAfterEntry = Convert.ToInt32(tbExitMinsAfterEntry.Text); } catch { }
            } else if (rbExitMinsBeforeClose.Checked) {
                tradeDefinition.TimeExitType = ExitTimeType.MinsBeforeClose;
                try { tradeDefinition.MinsBeforeClose = Convert.ToInt32(tbExitMinsBeforeClose.Text); } catch { }
            } else if (rbExitAtNextOpen.Checked) {
                tradeDefinition.TimeExitType = ExitTimeType.AtNextOpen;
            }

            try { tradeDefinition.StartHrs = Convert.ToInt32(tbStartHrs.Text); } catch { }
            try { tradeDefinition.StartMins = Convert.ToInt32(tbStartMins.Text); } catch { }
            try { tradeDefinition.EndHrs = Convert.ToInt32(tbEndHrs.Text); } catch { }
            try { tradeDefinition.EndMins = Convert.ToInt32(tbEndMins.Text); } catch { }

            if (rbShares.Checked)
            {
                tradeDefinition.PositionSizing = PositionSizingType.FixedShares;
                try { tradeDefinition.Sizing = Convert.ToInt32(tbShares.Text); } catch { }
            }
            else if (rbPositionSize.Checked)
            {
                tradeDefinition.PositionSizing = PositionSizingType.FixedAmount;
                try { tradeDefinition.Sizing = Convert.ToInt32(tbPositionSize.Text); } catch { }
            }
            else if (rbDollarsRisked.Checked)
            {
                tradeDefinition.PositionSizing = PositionSizingType.FixedRisk;
                try { tradeDefinition.Sizing = Convert.ToInt32(tbDollarsRisked.Text); } catch { }
            }

            try { tradeDefinition.MaxShares = Convert.ToInt32(tbMaxShares.Text); } catch { }
            try { tradeDefinition.SharesIncrement = Convert.ToInt32(tbIncrement.Text); } catch { }
            try { tradeDefinition.MaxTradesPerStrategy = Convert.ToInt32(tbMaxOrders.Text); } catch { }
            tradeDefinition.Transmit = cbTransmit.Checked;

            if (rbMarketOrder.Checked)
            {
                tradeDefinition.EntryOrderType = OrderType.Market;
            }
            else if (rbLimitOrder.Checked)
            {
                tradeDefinition.EntryOrderType = OrderType.Limit;
                if (cbOffset.Checked)
                {
                    tradeDefinition.UseLimitOffset = true;
                }
                if (cbGoodFor.Checked)
                {
                    tradeDefinition.UseGoodForSecs = true;
                }
            }
            tradeDefinition.PriceRef = (PriceRef)Enum.Parse(typeof(PriceRef), cmbPricePoint.Text, true);
            try { tradeDefinition.LimitOffset = Convert.ToDouble(tbOffset.Text); } catch { }
            try { tradeDefinition.GoodForSecs = Convert.ToInt32(tbGoodForSecs.Text); } catch { }

            tradeDefinition.Tags = tbTags.Text;

            tradeDefinition.TiUrl = tbUrl.Text;
            tradeDefinition.WindowName = lblWindowName.Text;
            tradeDefinition.Tags = tbTags.Text;
            
            this.Close();
        }

        private void btnParse_Click(object sender, EventArgs e)
        {
            string windowName = AutoTradeDefinition.ParseWindowName(tbUrl.Text);
            if (windowName != null && windowName.Length > 0) {
                lblWindowName.Text = windowName;
            } else {
                lblWindowName.Text = "<Use Parse to Load>";
            }
        }


        #region attributes
        public AutoTradeDefinition TradeDefinition
        {
            get { return tradeDefinition; }
            set
            {
                tradeDefinition = value;

                // initialize fields
                rbUp.Checked = tradeDefinition.EntryDirection == Direction.Long;
                rbDown.Checked = tradeDefinition.EntryDirection == Direction.Short;
                rbPoints.Checked = value.Units == TradeUnit.Points;
                rbPercent.Checked = value.Units == TradeUnit.Percent;
                tbPercent.Text = Convert.ToString(value.PercentsValue);
                tbPoints.Text = Convert.ToString(value.DollarsValue);

                cbTarget.Checked = value.UseProfitTarget;
                tbProfitTarget.Text = Convert.ToString(value.ProfitTarget);
                cbStoploss.Checked = value.UseStopLoss;
                tbStoploss.Text = Convert.ToString(value.StopLoss);
                cbWiggle.Checked = value.UseStopWiggle;

                rbPercTrailingStop.Checked = value.OtherExitCondition == ExitCondition.PercTrailStop;
                tbTrailStop.Text = Convert.ToString(value.PercTrailStop);
                rbBarTrailStop.Checked = value.OtherExitCondition == ExitCondition.BarsTrailStop;
                tbBarTrailStop.Text = Convert.ToString(value.BarsTrailStop);
                rbNoOtherExit.Checked = value.OtherExitCondition == ExitCondition.None;
                
                rbExitMinsAfterEntry.Checked = value.TimeExitType == ExitTimeType.MinsAfterEntry;
                tbExitMinsAfterEntry.Text = Convert.ToString(value.MinsAfterEntry);
                rbExitMinsBeforeClose.Checked = value.TimeExitType == ExitTimeType.MinsBeforeClose;
                tbExitMinsBeforeClose.Text = Convert.ToString(value.MinsBeforeClose);
                rbExitAtNextOpen.Checked = value.TimeExitType == ExitTimeType.AtNextOpen;


                tbStartHrs.Text = Convert.ToString(value.StartHrs);
                tbStartMins.Text = Convert.ToString(value.StartMins);
                tbEndHrs.Text = Convert.ToString(value.EndHrs);
                tbEndMins.Text = Convert.ToString(value.EndMins);

                rbShares.Checked = value.PositionSizing == PositionSizingType.FixedShares;
                if (rbShares.Checked)
                {
                    tbShares.Text = Convert.ToString(value.Sizing);
                }
                rbPositionSize.Checked = value.PositionSizing == PositionSizingType.FixedAmount;
                if (rbPositionSize.Checked)
                {
                    tbPositionSize.Text = Convert.ToString(value.Sizing);
                }
                rbDollarsRisked.Checked = value.PositionSizing == PositionSizingType.FixedRisk;
                if (rbDollarsRisked.Checked)
                {
                    tbDollarsRisked.Text = Convert.ToString(value.Sizing);
                }

                tbMaxShares.Text = Convert.ToString(value.MaxShares);
                tbIncrement.Text = Convert.ToString(value.SharesIncrement);
                tbMaxOrders.Text = Convert.ToString(value.MaxTradesPerStrategy);
                cbTransmit.Checked = value.Transmit;

                rbMarketOrder.Checked = value.EntryOrderType == OrderType.Market;
                rbLimitOrder.Checked = value.EntryOrderType == OrderType.Limit;
                cbOffset.Checked = value.UseLimitOffset == true;
                tbOffset.Text = Convert.ToString(value.LimitOffset);
                cbGoodFor.Checked = value.UseGoodForSecs;
                tbGoodForSecs.Text = Convert.ToString(value.GoodForSecs);
                cmbPricePoint.Text = value.PriceRef.ToString();

                tbTags.Text = value.Tags;

                tbUrl.Text = value.TiUrl;
                lblWindowName.Text = value.WindowName;
                tbTags.Text = value.Tags;

            }
        }
        #endregion Attributes

        private void btnAlerts_Click(object sender, EventArgs e) {
            //TIAlertsWindow w = new TIAlertsWindow();
            //w.Show();
            //if( w.IsOk() ) {
            //this.tbUrl = w.DataConfig;
            //}
        }

    }
}
