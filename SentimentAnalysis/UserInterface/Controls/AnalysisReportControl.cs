using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Persistence;
using Persistence.Repositories;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class AnalysisReportControl : UserControl
    {
        public AnalysisReportControl()
        {
            InitializeComponent();
            
            try
            {
                LoadGrid();
            }
            catch (DatabaseException ex)
            {
                Alerts.CrashApp(ex.Message);
            }
        }

        private void LoadGrid()
        {
            IRepository<Phrase> phraseRepository = new PhraseRepository();

            var reports = phraseRepository.GetAll().ToList();

            List<Analysis> reportPhrases = new List<Analysis>();

            reports.ForEach(r =>
               reportPhrases.Add(new Analysis()
               {
                   Author = r.Author.ToString(),
                   Word = r.Word,
                   SentimentType = r.Type != null
                    ? r.Type.ToString()
                    : string.Empty,
                   PostedDate = r.PostedDate.ToString("dd/MM/yyyy HH:mm"),
                   Entity = r.Entity != null
                    ? r.Entity.ToString()
                    : string.Empty,
                   Grade = r.Type == SentimentType.NEUTRAL
                    ? string.Empty
                    : r.Grade.ToString()
               })
            ); ;

            dgvPhrases.DataSource = reportPhrases.ToList();
        }
    }
}
