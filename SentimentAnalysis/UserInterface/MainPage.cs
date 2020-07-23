using BusinessLogic.DTO;
using Persistence;
using Persistence.Repositories;
using System;
using System.Linq;
using System.Windows.Forms;
using UserInterface.Controls;

namespace UserInterface
{
    public partial class MainPage : Form
    {
        public MainPage()
        {
            InitializeComponent();
        }

        #region Buttons

        private void btnEntity_Click(object sender, EventArgs e)
        {
            EntityControl control = new EntityControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnAuthor_Click(object sender, EventArgs e)
        {
            AuthorControl control = new AuthorControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnSentiment_Click(object sender, EventArgs e)
        {
            SentimentControl control = new SentimentControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnPhrase_Click(object sender, EventArgs e)
        {
            PhraseControl control = new PhraseControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnAlarm_Click(object sender, EventArgs e)
        {
            AlarmControl control = new AlarmControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnReportAnalysis_Click(object sender, EventArgs e)
        {
            AnalysisReportControl control = new AnalysisReportControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnReportAlarm_Click(object sender, EventArgs e)
        {
            AlertsReportControl control = new AlertsReportControl();

            CleanControls();
            pnlControls.Controls.Add(control);
        }

        private void btnReportAuthors_Click(object sender, EventArgs e)
        {
            AuthorReportControl autorReportControl = new AuthorReportControl();
            
            CleanControls();
            pnlControls.Controls.Add(autorReportControl);
        }

        #endregion

        private void CleanControls()
        {
            pnlControls.Controls.Clear();
        }

    }
}
