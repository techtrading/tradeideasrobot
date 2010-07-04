namespace TradeIdeasRobot
{
    partial class PositionsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.dataGridPositions = new System.Windows.Forms.DataGridView();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnPartials = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.rbMarket = new System.Windows.Forms.RadioButton();
            this.rbBid = new System.Windows.Forms.RadioButton();
            this.rbAsk = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnOfferOrder = new System.Windows.Forms.Button();
            this.btnBidOrder = new System.Windows.Forms.Button();
            this.btnMarketOrder = new System.Windows.Forms.Button();
            this.dgOrderDetails = new System.Windows.Forms.DataGridView();
            this.btnCancelOrder = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPositions)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderDetails)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridPositions
            // 
            this.dataGridPositions.AllowUserToAddRows = false;
            this.dataGridPositions.AllowUserToDeleteRows = false;
            this.dataGridPositions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridPositions.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridPositions.Location = new System.Drawing.Point(6, 19);
            this.dataGridPositions.Name = "dataGridPositions";
            this.dataGridPositions.ReadOnly = true;
            this.dataGridPositions.RowHeadersVisible = false;
            this.dataGridPositions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridPositions.Size = new System.Drawing.Size(472, 176);
            this.dataGridPositions.TabIndex = 2;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(497, 19);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnPartials
            // 
            this.btnPartials.Location = new System.Drawing.Point(497, 48);
            this.btnPartials.Name = "btnPartials";
            this.btnPartials.Size = new System.Drawing.Size(75, 23);
            this.btnPartials.TabIndex = 7;
            this.btnPartials.Text = "Exit 1/2";
            this.btnPartials.UseVisualStyleBackColor = true;
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(497, 77);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Exit Full";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // rbMarket
            // 
            this.rbMarket.AutoSize = true;
            this.rbMarket.Checked = true;
            this.rbMarket.Location = new System.Drawing.Point(498, 106);
            this.rbMarket.Name = "rbMarket";
            this.rbMarket.Size = new System.Drawing.Size(58, 17);
            this.rbMarket.TabIndex = 9;
            this.rbMarket.TabStop = true;
            this.rbMarket.Text = "Market";
            this.rbMarket.UseVisualStyleBackColor = true;
            // 
            // rbBid
            // 
            this.rbBid.AutoSize = true;
            this.rbBid.Location = new System.Drawing.Point(498, 129);
            this.rbBid.Name = "rbBid";
            this.rbBid.Size = new System.Drawing.Size(40, 17);
            this.rbBid.TabIndex = 10;
            this.rbBid.Text = "Bid";
            this.rbBid.UseVisualStyleBackColor = true;
            // 
            // rbAsk
            // 
            this.rbAsk.AutoSize = true;
            this.rbAsk.Location = new System.Drawing.Point(498, 152);
            this.rbAsk.Name = "rbAsk";
            this.rbAsk.Size = new System.Drawing.Size(43, 17);
            this.rbAsk.TabIndex = 11;
            this.rbAsk.Text = "Ask";
            this.rbAsk.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridPositions);
            this.groupBox1.Controls.Add(this.rbAsk);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.rbBid);
            this.groupBox1.Controls.Add(this.btnPartials);
            this.groupBox1.Controls.Add(this.rbMarket);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Location = new System.Drawing.Point(9, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(587, 201);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Positions";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnOfferOrder);
            this.groupBox2.Controls.Add(this.btnBidOrder);
            this.groupBox2.Controls.Add(this.btnMarketOrder);
            this.groupBox2.Controls.Add(this.dgOrderDetails);
            this.groupBox2.Controls.Add(this.btnCancelOrder);
            this.groupBox2.Location = new System.Drawing.Point(9, 220);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(587, 140);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Order Details";
            // 
            // btnOfferOrder
            // 
            this.btnOfferOrder.Location = new System.Drawing.Point(498, 106);
            this.btnOfferOrder.Name = "btnOfferOrder";
            this.btnOfferOrder.Size = new System.Drawing.Size(75, 23);
            this.btnOfferOrder.TabIndex = 6;
            this.btnOfferOrder.Text = "Offer";
            this.btnOfferOrder.UseVisualStyleBackColor = true;
            // 
            // btnBidOrder
            // 
            this.btnBidOrder.Location = new System.Drawing.Point(497, 77);
            this.btnBidOrder.Name = "btnBidOrder";
            this.btnBidOrder.Size = new System.Drawing.Size(75, 23);
            this.btnBidOrder.TabIndex = 5;
            this.btnBidOrder.Text = "Bid";
            this.btnBidOrder.UseVisualStyleBackColor = true;
            // 
            // btnMarketOrder
            // 
            this.btnMarketOrder.Location = new System.Drawing.Point(497, 48);
            this.btnMarketOrder.Name = "btnMarketOrder";
            this.btnMarketOrder.Size = new System.Drawing.Size(75, 23);
            this.btnMarketOrder.TabIndex = 4;
            this.btnMarketOrder.Text = "Market";
            this.btnMarketOrder.UseVisualStyleBackColor = true;
            // 
            // dgOrderDetails
            // 
            this.dgOrderDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOrderDetails.Location = new System.Drawing.Point(6, 19);
            this.dgOrderDetails.Name = "dgOrderDetails";
            this.dgOrderDetails.Size = new System.Drawing.Size(472, 110);
            this.dgOrderDetails.TabIndex = 2;
            // 
            // btnCancelOrder
            // 
            this.btnCancelOrder.Location = new System.Drawing.Point(497, 19);
            this.btnCancelOrder.Name = "btnCancelOrder";
            this.btnCancelOrder.Size = new System.Drawing.Size(75, 23);
            this.btnCancelOrder.TabIndex = 3;
            this.btnCancelOrder.Text = "Cancel";
            this.btnCancelOrder.UseVisualStyleBackColor = true;
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // PositionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 374);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "PositionsForm";
            this.Text = "PositionsForm";
            this.Shown += new System.EventHandler(this.PositionsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridPositions)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOrderDetails)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridPositions;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnPartials;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.RadioButton rbMarket;
        private System.Windows.Forms.RadioButton rbBid;
        private System.Windows.Forms.RadioButton rbAsk;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgOrderDetails;
        private System.Windows.Forms.Button btnCancelOrder;
        private System.Windows.Forms.Button btnOfferOrder;
        private System.Windows.Forms.Button btnBidOrder;
        private System.Windows.Forms.Button btnMarketOrder;
        private System.Windows.Forms.Timer timer;

    }
}