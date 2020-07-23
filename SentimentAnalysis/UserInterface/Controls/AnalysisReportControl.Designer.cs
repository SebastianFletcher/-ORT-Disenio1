namespace UserInterface.Controls
{
    partial class AnalysisReportControl
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
            this.dgvPhrases = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhrases)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(251, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 38);
            this.lblTitle.TabIndex = 18;
            this.lblTitle.Text = "Analysis Report";
            // 
            // dgvPhrases
            // 
            this.dgvPhrases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPhrases.Location = new System.Drawing.Point(9, 69);
            this.dgvPhrases.Name = "dgvPhrases";
            this.dgvPhrases.RowHeadersWidth = 51;
            this.dgvPhrases.RowTemplate.Height = 24;
            this.dgvPhrases.Size = new System.Drawing.Size(740, 338);
            this.dgvPhrases.TabIndex = 19;
            // 
            // AlarmsReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dgvPhrases);
            this.Controls.Add(this.lblTitle);
            this.Name = "AlarmsReportControl";
            this.Size = new System.Drawing.Size(760, 430);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPhrases)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvPhrases;
    }
}
