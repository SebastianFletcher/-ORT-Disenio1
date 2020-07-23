namespace UserInterface.Controls
{
    partial class AlarmControl
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
            this.pnlAlarms = new System.Windows.Forms.GroupBox();
            this.lstAlarms = new System.Windows.Forms.ListBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.pnlData = new System.Windows.Forms.GroupBox();
            this.cboTimeMeasure = new System.Windows.Forms.ComboBox();
            this.txtCantDays = new System.Windows.Forms.TextBox();
            this.txtPostCount = new System.Windows.Forms.TextBox();
            this.lblPostCount = new System.Windows.Forms.Label();
            this.chkActivity = new System.Windows.Forms.CheckBox();
            this.cboEntity = new System.Windows.Forms.ComboBox();
            this.cboSentimentType = new System.Windows.Forms.ComboBox();
            this.lblType = new System.Windows.Forms.Label();
            this.lblEntity = new System.Windows.Forms.Label();
            this.lblCantDays = new System.Windows.Forms.Label();
            this.txtPostQuantity = new System.Windows.Forms.TextBox();
            this.lblPostQuantity = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.rbtnEntity = new System.Windows.Forms.RadioButton();
            this.rbntAuthor = new System.Windows.Forms.RadioButton();
            this.pnlAlarms.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(324, 350);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(58, 17);
            this.lblMessage.TabIndex = 24;
            this.lblMessage.Text = "Message";
            this.lblMessage.Visible = false;
            // 
            // pnlAlarms
            // 
            this.pnlAlarms.Controls.Add(this.lstAlarms);
            this.pnlAlarms.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlAlarms.Location = new System.Drawing.Point(350, 90);
            this.pnlAlarms.Name = "pnlAlarms";
            this.pnlAlarms.Size = new System.Drawing.Size(406, 241);
            this.pnlAlarms.TabIndex = 20;
            this.pnlAlarms.TabStop = false;
            this.pnlAlarms.Text = "Alarms";
            // 
            // lstAlarms
            // 
            this.lstAlarms.FormattingEnabled = true;
            this.lstAlarms.ItemHeight = 15;
            this.lstAlarms.Location = new System.Drawing.Point(7, 22);
            this.lstAlarms.Name = "lstAlarms";
            this.lstAlarms.Size = new System.Drawing.Size(393, 184);
            this.lstAlarms.TabIndex = 0;
            this.lstAlarms.SelectedIndexChanged += new System.EventHandler(this.lstAlarms_SelectedIndexChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(592, 385);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(140, 35);
            this.btnDelete.TabIndex = 23;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.cboTimeMeasure);
            this.pnlData.Controls.Add(this.txtCantDays);
            this.pnlData.Controls.Add(this.txtPostCount);
            this.pnlData.Controls.Add(this.lblPostCount);
            this.pnlData.Controls.Add(this.chkActivity);
            this.pnlData.Controls.Add(this.cboEntity);
            this.pnlData.Controls.Add(this.cboSentimentType);
            this.pnlData.Controls.Add(this.lblType);
            this.pnlData.Controls.Add(this.lblEntity);
            this.pnlData.Controls.Add(this.lblCantDays);
            this.pnlData.Controls.Add(this.txtPostQuantity);
            this.pnlData.Controls.Add(this.lblPostQuantity);
            this.pnlData.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnlData.Location = new System.Drawing.Point(20, 90);
            this.pnlData.Name = "pnlData";
            this.pnlData.Size = new System.Drawing.Size(305, 241);
            this.pnlData.TabIndex = 19;
            this.pnlData.TabStop = false;
            this.pnlData.Text = "Data";
            // 
            // cboTimeMeasure
            // 
            this.cboTimeMeasure.FormattingEnabled = true;
            this.cboTimeMeasure.Location = new System.Drawing.Point(119, 143);
            this.cboTimeMeasure.Name = "cboTimeMeasure";
            this.cboTimeMeasure.Size = new System.Drawing.Size(180, 23);
            this.cboTimeMeasure.TabIndex = 17;
            // 
            // txtCantDays
            // 
            this.txtCantDays.Location = new System.Drawing.Point(119, 101);
            this.txtCantDays.Name = "txtCantDays";
            this.txtCantDays.Size = new System.Drawing.Size(180, 23);
            this.txtCantDays.TabIndex = 15;
            // 
            // txtPostCount
            // 
            this.txtPostCount.Location = new System.Drawing.Point(119, 186);
            this.txtPostCount.Name = "txtPostCount";
            this.txtPostCount.Size = new System.Drawing.Size(180, 23);
            this.txtPostCount.TabIndex = 14;
            // 
            // lblPostCount
            // 
            this.lblPostCount.AutoSize = true;
            this.lblPostCount.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblPostCount.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostCount.Location = new System.Drawing.Point(9, 189);
            this.lblPostCount.Name = "lblPostCount";
            this.lblPostCount.Size = new System.Drawing.Size(72, 17);
            this.lblPostCount.TabIndex = 13;
            this.lblPostCount.Text = "Post Count:";
            // 
            // chkActivity
            // 
            this.chkActivity.AutoSize = true;
            this.chkActivity.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkActivity.Location = new System.Drawing.Point(162, 215);
            this.chkActivity.Name = "chkActivity";
            this.chkActivity.Size = new System.Drawing.Size(93, 21);
            this.chkActivity.TabIndex = 12;
            this.chkActivity.Text = "Is Enabled?";
            this.chkActivity.UseVisualStyleBackColor = true;
            // 
            // cboEntity
            // 
            this.cboEntity.FormattingEnabled = true;
            this.cboEntity.Location = new System.Drawing.Point(119, 143);
            this.cboEntity.Name = "cboEntity";
            this.cboEntity.Size = new System.Drawing.Size(180, 23);
            this.cboEntity.TabIndex = 10;
            // 
            // cboSentimentType
            // 
            this.cboSentimentType.FormattingEnabled = true;
            this.cboSentimentType.Location = new System.Drawing.Point(119, 60);
            this.cboSentimentType.Name = "cboSentimentType";
            this.cboSentimentType.Size = new System.Drawing.Size(180, 23);
            this.cboSentimentType.TabIndex = 9;
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblType.Location = new System.Drawing.Point(9, 60);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(38, 17);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Type:";
            // 
            // lblEntity
            // 
            this.lblEntity.AutoSize = true;
            this.lblEntity.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.lblEntity.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEntity.Location = new System.Drawing.Point(9, 149);
            this.lblEntity.Name = "lblEntity";
            this.lblEntity.Size = new System.Drawing.Size(45, 17);
            this.lblEntity.TabIndex = 6;
            this.lblEntity.Text = "Entity:";
            // 
            // lblCantDays
            // 
            this.lblCantDays.AutoSize = true;
            this.lblCantDays.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCantDays.Location = new System.Drawing.Point(9, 101);
            this.lblCantDays.Name = "lblCantDays";
            this.lblCantDays.Size = new System.Drawing.Size(68, 17);
            this.lblCantDays.TabIndex = 2;
            this.lblCantDays.Text = "Cant Days:";
            // 
            // txtPostQuantity
            // 
            this.txtPostQuantity.Location = new System.Drawing.Point(119, 22);
            this.txtPostQuantity.Name = "txtPostQuantity";
            this.txtPostQuantity.Size = new System.Drawing.Size(180, 23);
            this.txtPostQuantity.TabIndex = 1;
            // 
            // lblPostQuantity
            // 
            this.lblPostQuantity.AutoSize = true;
            this.lblPostQuantity.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPostQuantity.Location = new System.Drawing.Point(9, 25);
            this.lblPostQuantity.Name = "lblPostQuantity";
            this.lblPostQuantity.Size = new System.Drawing.Size(88, 17);
            this.lblPostQuantity.TabIndex = 0;
            this.lblPostQuantity.Text = "Post Quantity:";
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Calibri", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(317, 385);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(140, 35);
            this.btnSave.TabIndex = 22;
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
            this.btnCancel.TabIndex = 21;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Calibri", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(320, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(100, 40);
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "Alarm";
            // 
            // rbtnEntity
            // 
            this.rbtnEntity.AllowDrop = true;
            this.rbtnEntity.AutoSize = true;
            this.rbtnEntity.Checked = true;
            this.rbtnEntity.Location = new System.Drawing.Point(20, 63);
            this.rbtnEntity.Name = "rbtnEntity";
            this.rbtnEntity.Size = new System.Drawing.Size(84, 21);
            this.rbtnEntity.TabIndex = 25;
            this.rbtnEntity.TabStop = true;
            this.rbtnEntity.Text = "By Entity";
            this.rbtnEntity.UseVisualStyleBackColor = true;
            this.rbtnEntity.CheckedChanged += new System.EventHandler(this.rbtnEntity_CheckedChanged);
            // 
            // rbntAuthor
            // 
            this.rbntAuthor.AllowDrop = true;
            this.rbntAuthor.AutoSize = true;
            this.rbntAuthor.Location = new System.Drawing.Point(110, 63);
            this.rbntAuthor.Name = "rbntAuthor";
            this.rbntAuthor.Size = new System.Drawing.Size(91, 21);
            this.rbntAuthor.TabIndex = 26;
            this.rbntAuthor.Text = "By Author";
            this.rbntAuthor.UseVisualStyleBackColor = true;
            this.rbntAuthor.CheckedChanged += new System.EventHandler(this.rbntAuthor_CheckedChanged);
            // 
            // AlarmControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.rbntAuthor);
            this.Controls.Add(this.rbtnEntity);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.pnlAlarms);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblTitle);
            this.Name = "AlarmControl";
            this.Size = new System.Drawing.Size(780, 430);
            this.pnlAlarms.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.pnlData.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.GroupBox pnlAlarms;
        private System.Windows.Forms.ListBox lstAlarms;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.GroupBox pnlData;
        private System.Windows.Forms.TextBox txtPostQuantity;
        private System.Windows.Forms.Label lblPostQuantity;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCantDays;
        private System.Windows.Forms.ComboBox cboSentimentType;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblEntity;
        private System.Windows.Forms.ComboBox cboEntity;
        private System.Windows.Forms.CheckBox chkActivity;
        private System.Windows.Forms.TextBox txtPostCount;
        private System.Windows.Forms.Label lblPostCount;
        private System.Windows.Forms.TextBox txtCantDays;
        private System.Windows.Forms.RadioButton rbtnEntity;
        private System.Windows.Forms.RadioButton rbntAuthor;
        private System.Windows.Forms.ComboBox cboTimeMeasure;
    }
}
