using System;
using System.Windows.Forms;
using Persistence.Repositories;
using BusinessLogic.DTO;
using BusinessLogic.Enums;
using BusinessLogic.Exceptions;
using Persistence;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class AlarmControl : UserControl
    {
        private const string SAVE_ALARM = "Alarm saved correctly.";
        private const string MODIFY_ALARM = "Alarm modify correctly.";
        private const string DELETE_ALARM = "Alarm deleted.";

        private IRepository<EntityAlarm> entityAlarmRepository;
        private IRepository<AuthorAlarm> authorAlarmRepository;
        private IRepository<Entity> entityRepository;

        private EntityAlarm SelectedEntityAlarm;
        private AuthorAlarm SelectedAuthorAlarm;

        public AlarmControl()
        {
            entityAlarmRepository = new EntityAlarmRepository();
            authorAlarmRepository = new AuthorAlarmRepository();
            entityRepository = new EntityRepository();

            InitializeComponent();

            try
            {
                LoadSentimentTypes();
                LoadEntities();
                LoadTimeMeasures();
                rbtnEntity_CheckedChanged(null, null);
                EnableFields(false);
                HideFields(true);
                CleanFields();
            }
            catch(DatabaseException ex)
            {
                Alerts.CrashApp(ex.Message);
            }
        }

        #region Events

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            try
            {
                string message;

                if (rbtnEntity.Checked)
                {
                    #region Entity Alarm

                    EntityAlarm alarm = new EntityAlarm(
                            txtPostQuantity.Text,
                            txtCantDays.Text,
                            (SentimentType)cboSentimentType.SelectedItem,
                            (Entity)cboEntity.SelectedItem);

                    if (SelectedEntityAlarm == null)
                    {
                        entityAlarmRepository.Add(alarm);
                        message = SAVE_ALARM;
                    }
                    else
                    {
                        alarm.Id = SelectedEntityAlarm.Id;
                        alarm.CreationDate = SelectedEntityAlarm.CreationDate;
                        entityAlarmRepository.Modify(SelectedEntityAlarm.Id, alarm);
                        message = MODIFY_ALARM;
                    }

                    #endregion
                }
                else
                {
                    #region Author Alarm

                    AuthorAlarm alarm = new AuthorAlarm(
                            txtPostQuantity.Text,
                            txtCantDays.Text,
                            (SentimentType)cboSentimentType.SelectedItem,
                            (TimeMeasure)cboTimeMeasure.SelectedItem);

                    if (SelectedAuthorAlarm == null)
                    {
                        authorAlarmRepository.Add(alarm);
                        message = SAVE_ALARM;
                    }
                    else
                    {
                        alarm.Id = SelectedAuthorAlarm.Id;
                        alarm.CreationDate = SelectedAuthorAlarm.CreationDate;
                        authorAlarmRepository.Modify(SelectedAuthorAlarm.Id, alarm);
                        message = MODIFY_ALARM;
                    }

                    #endregion
                }

                LoadAlarms();
                ShowSuccess(message);
                CleanFields();
            }
            catch (AlarmException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            CleanFields();
            CleanMessage();
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (rbtnEntity.Checked)
                    entityAlarmRepository.Delete(SelectedEntityAlarm != null ? SelectedEntityAlarm.Id : (int?)null);
                else
                    authorAlarmRepository.Delete(SelectedAuthorAlarm != null ? SelectedAuthorAlarm.Id : (int?)null);

                LoadAlarms();
                CleanFields();
                ShowSuccess(DELETE_ALARM);
            }
            catch (AlarmException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void lstAlarms_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstAlarms.SelectedItem is null)
                return;

            if (rbtnEntity.Checked)
            {
                SelectedEntityAlarm = lstAlarms.SelectedItem as EntityAlarm;
                LoadEntityAlarmData(SelectedEntityAlarm);
            }
            else
            {
                SelectedAuthorAlarm = lstAlarms.SelectedItem as AuthorAlarm;
                LoadAuthorAlarmData(SelectedAuthorAlarm);
            }
        }

        private void rbtnEntity_CheckedChanged(object sender, EventArgs e)
        {
            CleanMessage();

            lblCantDays.Text = "Cant Days:";
            lblEntity.Text = "Entity:";
            cboEntity.Visible = true;
            cboTimeMeasure.Visible = false;

            LoadAlarms();
        }

        private void rbntAuthor_CheckedChanged(object sender, EventArgs e)
        {
            CleanMessage();

            lblCantDays.Text = "Time:";
            lblEntity.Text = "Time Measure:";
            cboTimeMeasure.Visible = true;
            cboEntity.Visible = false;

            HideFields(true);

            LoadAlarms();
        }

        #endregion

        #region Messages

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Red;
        }

        private void ShowSuccess(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
            lblMessage.ForeColor = System.Drawing.Color.Green;
        }

        private void CleanMessage()
        {
            lblMessage.Text = string.Empty;
            lblMessage.Visible = false;

            lstAlarms.ClearSelected();
        }

        #endregion

        #region Fields

        private void CleanFields()
        {
            HideFields(true);

            SelectedEntityAlarm = null;
            SelectedAuthorAlarm = null;

            txtPostQuantity.Text = string.Empty;
            txtCantDays.Text = string.Empty;
            cboSentimentType.SelectedIndex = -1;
            cboTimeMeasure.SelectedIndex = -1;
            cboEntity.SelectedIndex = -1;
            txtPostCount.Text = string.Empty;
            chkActivity.Checked = false;

            lstAlarms.ClearSelected();
        }

        private void LoadAlarms()
        {
            if (rbtnEntity.Checked)
            {
                lstAlarms.DataSource = entityAlarmRepository.GetAll();
            }
            else
            {
                lstAlarms.DataSource = authorAlarmRepository.GetAll();
            }

            CleanFields();
        }

        private void LoadEntityAlarmData(EntityAlarm alarm)
        {
            CleanMessage();
            HideFields(false);

            txtPostQuantity.Text = alarm.PostQuantity;
            txtCantDays.Text = alarm.Time;
            cboSentimentType.SelectedItem = alarm.Type;
            cboEntity.SelectedItem = alarm.Entity;
            txtPostCount.Text = alarm.PostCount.ToString();
            chkActivity.Checked = alarm.IsEnabled();
        }

        private void LoadAuthorAlarmData(AuthorAlarm alarm)
        {
            CleanMessage();
            HideFields(false);

            txtPostQuantity.Text = alarm.PostQuantity;
            txtCantDays.Text = alarm.Time;
            cboSentimentType.SelectedItem = alarm.Type;
            cboTimeMeasure.SelectedItem = alarm.TimeMeasure;
            txtPostCount.Text = alarm.PostCount.ToString();
            chkActivity.Checked = alarm.IsEnabled();
        }

        private void LoadSentimentTypes()
        {
            cboSentimentType.DataSource = Enum.GetValues(typeof(SentimentType));
            cboSentimentType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadTimeMeasures()
        {
            cboTimeMeasure.DataSource = Enum.GetValues(typeof(TimeMeasure));
            cboTimeMeasure.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadEntities()
        {
            cboEntity.Items.Clear();
            cboEntity.DataSource = entityRepository.GetAll();
            cboEntity.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void EnableFields(bool enabled)
        {
            txtPostCount.Enabled = enabled;
            chkActivity.Enabled = enabled;
        }

        private void HideFields(bool visible)
        {
            lblPostCount.Visible = !visible;
            txtPostCount.Visible = !visible;
            chkActivity.Visible = !visible;
        }

        #endregion
    }
}
