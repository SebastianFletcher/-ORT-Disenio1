using System;
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
    public partial class SentimentControl : UserControl
    {
        private const string SAVE_SENTIMENT = "Sentiment saved correctly.";
        private const string MODIFY_SENTIMENT = "Sentiment modify correctly.";
        private const string DELETE_SENTIMENT = "Sentiment deleted.";

        private IRepository<Sentiment> repository;
        private Sentiment SelectedSentiment;

        public SentimentControl()
        {
            this.repository = new SentimentRepository();

            InitializeComponent();

            try
            {
                LoadSentimentTypes();
                LoadSentiments();
                CleanFields();
            }
            catch (DatabaseException ex)
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

                Sentiment sentiment = new Sentiment((SentimentType)cboSentimentType.SelectedItem, txtWord.Text);

                if (SelectedSentiment == null)
                {
                    repository.Add(sentiment);
                    message = SAVE_SENTIMENT;
                }
                else
                {
                    sentiment.Id = SelectedSentiment.Id;
                    repository.Modify(SelectedSentiment.Id, sentiment);
                    message = MODIFY_SENTIMENT;
                }

                LoadSentiments();
                ShowSuccess(message);
                CleanFields();
            }
            catch (SentimentException ex)
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
                repository.Delete(SelectedSentiment != null ? SelectedSentiment.Id : (int?) null);
                LoadSentiments();
                CleanFields();
                ShowSuccess(DELETE_SENTIMENT);
            }
            catch (SentimentException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void lstSentiments_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstSentiments.SelectedItem is null)
                return;

            SelectedSentiment = lstSentiments.SelectedItem as Sentiment;
            LoadData(SelectedSentiment);
        }

        private void cboSentimentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CleanMessage();
            LoadSentiments();
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
        }

        #endregion

        #region Fields

        private void LoadSentimentTypes()
        {
            cboSentimentType.DataSource = Enum.GetValues(typeof(SentimentType));
            cboSentimentType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void CleanFields()
        {
            SelectedSentiment = null;

            txtWord.Text = string.Empty;
            lstSentiments.ClearSelected();
        }

        private void LoadSentiments()
        {
            var dataSource = repository.GetAll().Where(p => p.Type == (SentimentType)cboSentimentType.SelectedItem);

            lstSentiments.DataSource = null;
            lstSentiments.DataSource = dataSource.ToList();

            CleanFields();
        }

        private void LoadData(Sentiment sentiment)
        {
            CleanMessage();

            txtWord.Text = sentiment.Word;
            cboSentimentType.SelectedItem = sentiment.Type;
        }

        #endregion
    }
}
