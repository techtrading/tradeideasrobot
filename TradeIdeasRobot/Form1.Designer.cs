namespace TradeIdeasWindowApp
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.axTws1 = new AxTWSLib.AxTws();
            this.axIQFeedY1 = new AxIQFEEDYLib.AxIQFeedY();
            this.axTradeIdeasWindowX1 = new AxTradeIdeasWindow.AxTradeIdeasWindowX();
            ((System.ComponentModel.ISupportInitialize)(this.axTws1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axIQFeedY1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).BeginInit();
            this.SuspendLayout();
            // 
            // axTws1
            // 
            this.axTws1.Enabled = true;
            this.axTws1.Location = new System.Drawing.Point(12, 12);
            this.axTws1.Name = "axTws1";
            this.axTws1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTws1.OcxState")));
            this.axTws1.Size = new System.Drawing.Size(100, 50);
            this.axTws1.TabIndex = 0;
            // 
            // axIQFeedY1
            // 
            this.axIQFeedY1.Enabled = true;
            this.axIQFeedY1.Location = new System.Drawing.Point(45, 181);
            this.axIQFeedY1.Name = "axIQFeedY1";
            this.axIQFeedY1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axIQFeedY1.OcxState")));
            this.axIQFeedY1.Size = new System.Drawing.Size(100, 50);
            this.axIQFeedY1.TabIndex = 2;
            // 
            // axTradeIdeasWindowX1
            // 
            this.axTradeIdeasWindowX1.Location = new System.Drawing.Point(178, 105);
            this.axTradeIdeasWindowX1.Name = "axTradeIdeasWindowX1";
            this.axTradeIdeasWindowX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTradeIdeasWindowX1.OcxState")));
            this.axTradeIdeasWindowX1.Size = new System.Drawing.Size(75, 23);
            this.axTradeIdeasWindowX1.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.axIQFeedY1);
            this.Controls.Add(this.axTradeIdeasWindowX1);
            this.Controls.Add(this.axTws1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.axTws1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axIQFeedY1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTWSLib.AxTws axTws1;
        private AxTradeIdeasWindow.AxTradeIdeasWindowX axTradeIdeasWindowX1;
        private AxIQFEEDYLib.AxIQFeedY axIQFeedY1;
    }
}