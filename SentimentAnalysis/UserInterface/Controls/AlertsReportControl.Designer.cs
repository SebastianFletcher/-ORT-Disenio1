namespace UserInterface.Controls
{
    partial class AlertsReportControl
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
            this.dgvAlerts = new System.Windows.Forms.DataGridView();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAlerts
            // 
            this.dgvAlerts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAlerts.Location = new System.Drawing.Point(23, 71);
            this.dgvAlerts.Name = "dgvAlerts";
            this.dgvAlerts.RowHeadersWidth = 51;
            this.dgvAlerts.RowTemplate.Height = 24;
            this.dgvAlerts.Size = new System.Drawing.Size(741, 338);
            this.dgvAlerts.TabIndex = 21;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(250, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(221, 38);
            this.lblTitle.TabIndex = 20;
            this.lblTitle.Text = "Alerts Report";
            // 
            // AlertsReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvAlerts);
            this.Controls.Add(this.lblTitle);
            this.Name = "AlertsReportControl";
            this.Size = new System.Drawing.Size(780, 430);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAlerts)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAlerts;
        private System.Windows.Forms.Label lblTitle;
    }
}
