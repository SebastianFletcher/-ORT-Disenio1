namespace UserInterface.Controls
{
    partial class SentimentControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlData = new System.Windows.Forms.GroupBox();
            this.cboSentimentType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.lblWord = new System.Windows.Forms.Label();
            this.pnlSentiments = new System.Windows.Forms.GroupBox();
            this.lstSentiments = new System.Windows.Forms.ListBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlData.SuspendLayout();
            this.pnlSentiments.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(314, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(164, 41);
            this.lblTitle.TabIndex = 2;
            this.lblTitle.Text = "Sentiment";
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.cboSentimentType);
            this.pnlData.Controls.Add(this.lblType);
            this.pnlData.Controls.Add(this.txtWord);
            this.pnlData.Controls.Add(this.lblWord);
            this.pnlData.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlData.Location = new System.Drawing.Point(20, 75);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(295, 242);
            this.pnlData.TabIndex = 3;
            this.pnlData.TabStop = false;
            this.pnlData.Text = "Data";
            // 
            // cboSentimentType
            // 
            this.cboSentimentType.FormattingEnabled = true;
            this.cboSentimentType.Location = new System.Drawing.Point(96, 27);
            this.cboSentimentType.Name = "cboSentimentType";
            this.cboSentimentType.Size = new System.Drawing.Size(175, 23);
            this.cboSentimentType.TabIndex = 1;
            this.cboSentimentType.SelectedIndexChanged += new System.EventHandler(this.cboSentimentType_SelectedIndexChanged);
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(7, 30);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(38, 17);
            this.lblType.TabIndex = 11;
            this.lblType.Text = "Type:";
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(96, 71);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(175, 23);
            this.txtWord.TabIndex = 0;
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Location = new System.Drawing.Point(7, 74);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(42, 17);
            this.lblWord.TabIndex = 10;
            this.lblWord.Text = "Word:";
            // 
            // pnlSentiments
            // 
            this.pnlSentiments.Controls.Add(this.lstSentiments);
            this.pnlSentiments.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSentiments.Location = new System.Drawing.Point(331, 75);
            this.pnlSentiments.Name = "pnlSentiments";
            this.pnlSentiments.Size = new System.Drawing.Size(427, 242);
            this.pnlSentiments.TabIndex = 6;
            this.pnlSentiments.TabStop = false;
            this.pnlSentiments.Text = "Sentiments";
            // 
            // lstSentiments
            // 
            this.lstSentiments.FormattingEnabled = true;
            this.lstSentiments.ItemHeight = 15;
            this.lstSentiments.Location = new System.Drawing.Point(19, 22);
            this.lstSentiments.Name = "lstSentiments";
            this.lstSentiments.Size = new System.Drawing.Size(402, 184);
            this.lstSentiments.TabIndex = 0;
            this.lstSentiments.SelectedIndexChanged += new System.EventHandler(this.lstSentiments_SelectedIndexChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(364, 350);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(58, 17);
            this.lblMessage.TabIndex = 10;
            this.lblMessage.Text = "Message";
            this.lblMessage.Visible = false;
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(612, 385);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.TabIndex = 5;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(331, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(20, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(140, 35);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SentimentControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pnlSentiments);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.Name = "SentimentControl";
            this.Size = new System.Drawing.Size(780, 430);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.pnlSentiments.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox pnlData;
        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.GroupBox pnlSentiments;
        private System.Windows.Forms.ListBox lstSentiments;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ComboBox cboSentimentType;
        private System.Windows.Forms.Label lblType;
    }
}
