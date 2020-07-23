using System;
using System.Windows.Forms;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using BusinessLogic.Enums;
using Persistence;
using Persistence.Repositories;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class PhraseControl : UserControl
    {
        private const string SAVE_PHRASE = "Phrase saved correctly.";
        private const string MODIFY_PHRASE = "Prhase modify correctly.";
        private const string DELETE_PHRASE = "Phrase deleted.";

        private IRepository<Phrase> repository;
        private IRepository<Author> authorRepository;
        private Phrase SelectedPhrase;

        public PhraseControl()
        {
            repository = new PhraseRepository();
            authorRepository = new AuthorRepository();

            InitializeComponent();
            
            try
            {
                LoadPhrases();
                LoadSentimentTypes();
                LoadGrade();
                LoadAuthors();
                EnableFields(false);
                HideFields(true);
                CleanFields();

                dtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
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

                Phrase phrase = new Phrase(txtWord.Text, dtpDate.Value, (Author)cboAuthor.SelectedItem);

                if (SelectedPhrase == null)
                {
                    repository.Add(phrase);
                    message = SAVE_PHRASE;
                }
                else
                {
                    phrase.Id = SelectedPhrase.Id;
                    repository.Modify(SelectedPhrase.Id, phrase);
                    message = MODIFY_PHRASE;
                }

                LoadAuthors();
                LoadPhrases();
                ShowSuccess(message);
                CleanFields();
            }
            catch (PhraseException ex)
            {
                ShowError(ex.Message);
            }
            catch (AnalysisException aex)
            {
                ShowError(aex.Message);
                LoadPhrases();
                CleanFields();
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
                repository.Delete(SelectedPhrase != null ? SelectedPhrase.Id : (int?) null);
                LoadPhrases();
                CleanFields();
                ShowSuccess(DELETE_PHRASE);
            }
            catch (PhraseException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void lstPhrases_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstPhrases.SelectedItem is null)
                return;

            SelectedPhrase = lstPhrases.SelectedItem as Phrase;
            LoadData(SelectedPhrase);
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

        private void CleanFields()
        {
            HideFields(true);

            SelectedPhrase = null;
            txtWord.Text = string.Empty;
            dtpDate.Value = DateTime.Now;
            cboSentimentType.SelectedIndex = -1;
            cboGrade.SelectedIndex = -1;
            cboAuthor.SelectedIndex = -1;

            lstPhrases.ClearSelected();
        }

        private void LoadPhrases()
        {
            lstPhrases.DataSource = null;
            lstPhrases.DataSource = repository.GetAll();
        }

        private void LoadAuthors()
        {
            cboAuthor.DataSource = authorRepository.GetAll();
            cboAuthor.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadData(Phrase phrase)
        {
            CleanMessage();
            HideFields(false);

            txtWord.Text = phrase.Word;
            cboSentimentType.SelectedItem = phrase.Type;
            dtpDate.Value = phrase.PostedDate;
            txtEntity.Text = phrase.Entity != null ? phrase.Entity.Name : string.Empty;
            cboAuthor.SelectedItem = phrase.Author;
            cboGrade.SelectedItem = phrase.Grade;
        }

        private void LoadSentimentTypes()
        {
            cboSentimentType.DataSource = Enum.GetValues(typeof(SentimentType));
            cboSentimentType.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadGrade()
        {
            cboGrade.DataSource = Enum.GetValues(typeof(SentimentGrade));
            cboGrade.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void EnableFields(bool enabled)
        {
            cboSentimentType.Enabled = enabled;
            txtEntity.Enabled = enabled;
        }

        private void HideFields(bool visible)
        {
            lblType.Visible = !visible;
            cboSentimentType.Visible = !visible;
            lblGrade.Visible = !visible;
            cboGrade.Visible = !visible;
            lblEntity.Visible = !visible;
            txtEntity.Visible = !visible;
        }

        #endregion
    }
}
