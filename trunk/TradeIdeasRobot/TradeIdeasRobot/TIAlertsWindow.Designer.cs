namespace TradeIdeasRobot
{
    partial class TIAlertsWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TIAlertsWindow));
            this.btnAddNew = new System.Windows.Forms.Button();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClipboard = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.axTradeIdeasWindowX1 = new AxTradeIdeasWindow.AxTradeIdeasWindowX();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAddNew
            // 
            this.btnAddNew.Location = new System.Drawing.Point(0, 9);
            this.btnAddNew.Name = "btnAddNew";
            this.btnAddNew.Size = new System.Drawing.Size(82, 26);
            this.btnAddNew.TabIndex = 1;
            this.btnAddNew.Text = "Add New";
            this.btnAddNew.UseVisualStyleBackColor = true;
            this.btnAddNew.Click += new System.EventHandler(this.btnTrade_Click);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClipboard);
            this.panelButtons.Controls.Add(this.btnCancel);
            this.panelButtons.Controls.Add(this.btnSave);
            this.panelButtons.Controls.Add(this.btnAddNew);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 469);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(338, 38);
            this.panelButtons.TabIndex = 3;
            // 
            // btnClipboard
            // 
            this.btnClipboard.Location = new System.Drawing.Point(105, 11);
            this.btnClipboard.Name = "btnClipboard";
            this.btnClipboard.Size = new System.Drawing.Size(36, 23);
            this.btnClipboard.TabIndex = 4;
            this.btnClipboard.Text = "Clip";
            this.btnClipboard.UseVisualStyleBackColor = true;
            this.btnClipboard.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(253, 9);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 26);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(165, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(82, 26);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // axTradeIdeasWindowX1
            // 
            this.axTradeIdeasWindowX1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.axTradeIdeasWindowX1.Location = new System.Drawing.Point(0, 0);
            this.axTradeIdeasWindowX1.Name = "axTradeIdeasWindowX1";
            this.axTradeIdeasWindowX1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axTradeIdeasWindowX1.OcxState")));
            this.axTradeIdeasWindowX1.Size = new System.Drawing.Size(338, 473);
            this.axTradeIdeasWindowX1.TabIndex = 0;
            this.axTradeIdeasWindowX1.OnWindowNameChanged += new System.EventHandler(this.axTradeIdeasWindowX1_OnWindowNameChanged);
            // 
            // TIAlertsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(338, 507);
            this.Controls.Add(this.axTradeIdeasWindowX1);
            this.Controls.Add(this.panelButtons);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TIAlertsWindow";
            this.Text = "TIAlertsWindow";
            this.Load += new System.EventHandler(this.TIAlertsWindow_Load);
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axTradeIdeasWindowX1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private AxTradeIdeasWindow.AxTradeIdeasWindowX axTradeIdeasWindowX1;
        private System.Windows.Forms.Button btnAddNew;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClipboard;
    }
}