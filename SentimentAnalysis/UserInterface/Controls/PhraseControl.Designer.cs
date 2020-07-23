namespace UserInterface.Controls
{
    partial class PhraseControl
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
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlSentiments = new System.Windows.Forms.GroupBox();
            this.lstPhrases = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlData = new System.Windows.Forms.GroupBox();
            this.cboAuthor = new System.Windows.Forms.ComboBox();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cboSentimentType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.txtEntity = new System.Windows.Forms.TextBox();
            this.lblEntity = new System.Windows.Forms.Label();
            this.txtWord = new System.Windows.Forms.TextBox();
            this.lblWord = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cboGrade = new System.Windows.Forms.ComboBox();
            this.lblGrade = new System.Windows.Forms.Label();
            this.pnlSentiments.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(331, 350);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(58, 17);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Text = "Message";
            this.lblMessage.Visible = false;
            // 
            // pnlSentiments
            // 
            this.pnlSentiments.Controls.Add(this.lstPhrases);
            this.pnlSentiments.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlSentiments.Location = new System.Drawing.Point(290, 75);
            this.pnlSentiments.Name = "pnlSentiments";
            this.pnlSentiments.Size = new System.Drawing.Size(470, 271);
            this.pnlSentiments.TabIndex = 13;
            this.pnlSentiments.TabStop = false;
            this.pnlSentiments.Text = "Phrases";
            // 
            // lstPhrases
            // 
            this.lstPhrases.FormattingEnabled = true;
            this.lstPhrases.ItemHeight = 15;
            this.lstPhrases.Location = new System.Drawing.Point(7, 22);
            this.lstPhrases.Name = "lstPhrases";
            this.lstPhrases.Size = new System.Drawing.Size(457, 229);
            this.lstPhrases.TabIndex = 0;
            this.lstPhrases.SelectedIndexChanged += new System.EventHandler(this.lstPhrases_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(603, 385);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(139, 35);
            this.btnDelete.TabIndex = 16;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.cboGrade);
            this.pnlData.Controls.Add(this.lblGrade);
            this.pnlData.Controls.Add(this.cboAuthor);
            this.pnlData.Controls.Add(this.lblAuthor);
            this.pnlData.Controls.Add(this.dtpDate);
            this.pnlData.Controls.Add(this.lblDate);
            this.pnlData.Controls.Add(this.cboSentimentType);
            this.pnlData.Controls.Add(this.lblType);
            this.pnlData.Controls.Add(this.txtEntity);
            this.pnlData.Controls.Add(this.lblEntity);
            this.pnlData.Controls.Add(this.txtWord);
            this.pnlData.Controls.Add(this.lblWord);
            this.pnlData.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlData.Location = new System.Drawing.Point(21, 75);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(251, 271);
            this.pnlData.TabIndex = 12;
            this.pnlData.TabStop = false;
            this.pnlData.Text = "Data";
            // 
            // cboAuthor
            // 
            this.cboAuthor.FormattingEnabled = true;
            this.cboAuthor.Location = new System.Drawing.Point(78, 32);
            this.cboAuthor.Name = "cboAuthor";
            this.cboAuthor.Size = new System.Drawing.Size(165, 23);
            this.cboAuthor.TabIndex = 12;
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblAuthor.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAuthor.Location = new System.Drawing.Point(11, 38);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(51, 17);
            this.lblAuthor.TabIndex = 11;
            this.lblAuthor.Text = "Author:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDate.Location = new System.Drawing.Point(78, 111);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dtpDate.Size = new System.Drawing.Size(165, 23);
            this.dtpDate.TabIndex = 7;
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(9, 111);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(40, 17);
            this.lblDate.TabIndex = 6;
            this.lblDate.Text = "Date:";
            // 
            // cboSentimentType
            // 
            this.cboSentimentType.Enabled = false;
            this.cboSentimentType.FormattingEnabled = true;
            this.cboSentimentType.Location = new System.Drawing.Point(78, 157);
            this.cboSentimentType.Name = "cboSentimentType";
            this.cboSentimentType.Size = new System.Drawing.Size(165, 23);
            this.cboSentimentType.TabIndex = 5;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(8, 157);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(38, 17);
            this.lblType.TabIndex = 4;
            this.lblType.Text = "Type:";
            // 
            // txtEntity
            // 
            this.txtEntity.Enabled = false;
            this.txtEntity.Location = new System.Drawing.Point(80, 229);
            this.txtEntity.Name = "txtEntity";
            this.txtEntity.Size = new System.Drawing.Size(165, 23);
            this.txtEntity.TabIndex = 3;
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblEntity.Location = new System.Drawing.Point(10, 232);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(45, 17);
            this.lblEntity.TabIndex = 2;
            this.lblEntity.Text = "Entity:";
            // 
            // txtWord
            // 
            this.txtWord.Location = new System.Drawing.Point(78, 76);
            this.txtWord.Name = "txtWord";
            this.txtWord.Size = new System.Drawing.Size(165, 23);
            this.txtWord.TabIndex = 1;
            // 
            // lblWord
            // 
            this.lblWord.AutoSize = true;
            this.lblWord.Location = new System.Drawing.Point(8, 76);
            this.lblWord.Name = "lblWord";
            this.lblWord.Size = new System.Drawing.Size(42, 17);
            this.lblWord.TabIndex = 0;
            this.lblWord.Text = "Word:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(309, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(139, 35);
            this.btnSave.TabIndex = 15;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(21, 385);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(139, 35);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(339, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(110, 40);
            this.lblTitle.TabIndex = 11;
            this.lblTitle.Text = "Phrase";
            // 
            // cboGrade
            // 
            this.cboGrade.Enabled = false;
            this.cboGrade.FormattingEnabled = true;
            this.cboGrade.Location = new System.Drawing.Point(78, 190);
            this.cboGrade.Name = "cboGrade";
            this.cboGrade.Size = new System.Drawing.Size(165, 23);
            this.cboGrade.TabIndex = 14;
            // 
            // lblGrade
            // 
            this.lblGrade.AutoSize = true;
            this.lblGrade.Location = new System.Drawing.Point(8, 190);
            this.lblGrade.Name = "lblGrade";
            this.lblGrade.Size = new System.Drawing.Size(47, 17);
            this.lblGrade.TabIndex = 13;
            this.lblGrade.Text = "Grade:";
            // 
            // PhraseControl
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
            this.Name = "PhraseControl";
            this.Size = new System.Drawing.Size(779, 430);
            this.pnlSentiments.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox pnlSentiments;
        private System.Windows.Forms.ListBox lstPhrases;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox pnlData;
        private System.Windows.Forms.TextBox txtWord;
        private System.Windows.Forms.Label lblWord;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtEntity;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.ComboBox cboSentimentType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.ComboBox cboAuthor;
        private System.Windows.Forms.Label lblAuthor;
        private System.Windows.Forms.ComboBox cboGrade;
        private System.Windows.Forms.Label lblGrade;
    }
}
