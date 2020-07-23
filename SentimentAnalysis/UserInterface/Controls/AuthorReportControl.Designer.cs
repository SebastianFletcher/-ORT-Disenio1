namespace UserInterface.Controls
{
    partial class AuthorReportControl
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lstAuthor = new System.Windows.Forms.DataGridView();
            this.chartAuthors = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.cboFilter = new System.Windows.Forms.ComboBox();
            this.lblFilter = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.lstAuthor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAuthors)).BeginInit();
            this.SuspendLayout();
            // 
            // lstAuthor
            // 
            this.lstAuthor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.lstAuthor.Location = new System.Drawing.Point(90, 108);
            this.lstAuthor.Name = "lstAuthor";
            this.lstAuthor.RowHeadersWidth = 51;
            this.lstAuthor.RowTemplate.Height = 24;
            this.lstAuthor.Size = new System.Drawing.Size(605, 110);
            this.lstAuthor.TabIndex = 30;
            // 
            // chartAuthors
            // 
            chartArea2.Name = "ChartArea1";
            this.chartAuthors.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartAuthors.Legends.Add(legend2);
            this.chartAuthors.Location = new System.Drawing.Point(90, 225);
            this.chartAuthors.Name = "chartAuthors";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Authors";
            this.chartAuthors.Series.Add(series2);
            this.chartAuthors.Size = new System.Drawing.Size(605, 191);
            this.chartAuthors.TabIndex = 29;
            this.chartAuthors.Text = "chart1";
            // 
            // cboFilter
            // 
            this.cboFilter.FormattingEnabled = true;
            this.cboFilter.Location = new System.Drawing.Point(155, 75);
            this.cboFilter.Name = "cboFilter";
            this.cboFilter.Size = new System.Drawing.Size(324, 24);
            this.cboFilter.TabIndex = 28;
            this.cboFilter.SelectedIndexChanged += new System.EventHandler(this.cboFilter_SelectedIndexChanged);
            // 
            // lblFilter
            // 
            this.lblFilter.AutoSize = true;
            this.lblFilter.Location = new System.Drawing.Point(87, 78);
            this.lblFilter.Name = "lblFilter";
            this.lblFilter.Size = new System.Drawing.Size(62, 17);
            this.lblFilter.TabIndex = 27;
            this.lblFilter.Text = "Filter by:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(261, 14);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(251, 38);
            this.lblTitle.TabIndex = 26;
            this.lblTitle.Text = "Authors Report";
            // 
            // AuthorReportControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lstAuthor);
            this.Controls.Add(this.chartAuthors);
            this.Controls.Add(this.cboFilter);
            this.Controls.Add(this.lblFilter);
            this.Controls.Add(this.lblTitle);
            this.Name = "AuthorReportControl";
            this.Size = new System.Drawing.Size(780, 430);
            ((System.ComponentModel.ISupportInitialize)(this.lstAuthor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartAuthors)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView lstAuthor;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartAuthors;
        private System.Windows.Forms.ComboBox cboFilter;
        private System.Windows.Forms.Label lblFilter;
        private System.Windows.Forms.Label lblTitle;
    }
}
