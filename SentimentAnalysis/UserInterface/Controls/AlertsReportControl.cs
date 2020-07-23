using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence;
using Persistence.Repositories;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class AlertsReportControl : UserControl
    {
        public AlertsReportControl()
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
            IRepository<EntityAlarm> entityAlarmRepository = new EntityAlarmRepository();
            IRepository<AuthorAlarm> authorAlarmRepository = new AuthorAlarmRepository();
            IRepository<Phrase> phrasesRepository = new PhraseRepository();

            List<Alert> alerts = new List<Alert>();

            #region Entity Alarms

            var activeEntityAlarms = entityAlarmRepository.GetAll().Where(a => a.IsEnabled()).ToList();

            activeEntityAlarms.ForEach(a =>
               alerts.Add(new Alert()
               {
                   NumberDays = a.Time.ToString(),
                   CantPost = a.PostQuantity.ToString(),
                   Entity = a.Entity.Name.ToString(),
                   InitDate = a.CreationDate.ToString("dd/MM/yyyy HH:mm"),
                   SentimentType = a.Type.ToString(),
                   Authors = string.Empty
               })
            );

            #endregion

            #region Author Alarms

            var activeAuthorAlarms = authorAlarmRepository.GetAll().Where(a => a.IsEnabled()).ToList();

            var phrases = phrasesRepository.GetAll().ToList();


            foreach (AuthorAlarm alarm in activeAuthorAlarms)
            {
                var phrasesInRange = phrases
                    .Where(p => p.Type == alarm.Type && alarm.InRangeOfTime(p.PostedDate))
                    .GroupBy(p => p.Author.Username);

                string authorsName = "";

                phrasesInRange.Select(p => p.Key).ToList().ForEach(a => authorsName += a + ",");

                alerts.Add(new Alert()
                {
                    NumberDays = alarm.Time.ToString(),
                    CantPost = alarm.PostQuantity.ToString(),
                    Entity = string.Empty,
                    InitDate = alarm.CreationDate.ToString("dd/MM/yyyy HH:mm"),
                    SentimentType = alarm.Type.ToString(),
                    Authors = authorsName.Substring(0, authorsName.Length - 1)
                });
            }
            #endregion

            dgvAlerts.DataSource = alerts.ToList();
        }

    }
}
