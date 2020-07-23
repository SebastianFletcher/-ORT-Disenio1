using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence;
using Persistence.Repositories;
using System;
using System.Windows.Forms;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class AuthorControl : UserControl
    {
        private const string SAVE_AUTHOR = "Author saved correctly.";
        private const string MODIFY_AUTHOR = "Author modify correctly.";
        private const string DELETE_AUTHOR = "Author deleted.";

        private IRepository<Author> repository;
        private Author SelectedAuthor;

        public AuthorControl()
        {
            repository = new AuthorRepository();

            InitializeComponent();

            try
            {
                LoadAuthors();
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

                Author author = new Author(txtUsername.Text, txtName.Text, txtLastName.Text, dtpBirthdate.Value);

                if (SelectedAuthor == null)
                {
                    repository.Add(author);
                    message = SAVE_AUTHOR;
                }
                else
                {
                    author.Id = SelectedAuthor.Id;
                    repository.Modify(SelectedAuthor.Id, author);
                    message = MODIFY_AUTHOR;
                }

                LoadAuthors();
                ShowSuccess(message);
                CleanFields();
            }
            catch (AuthorException ex)
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
                repository.Delete(SelectedAuthor != null ? SelectedAuthor.Id : (int?) null);
                LoadAuthors();
                CleanFields();
                ShowSuccess(DELETE_AUTHOR);
            }
            catch (AuthorException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void lstAuthors_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstAuthors.SelectedItem is null)
                return;

            SelectedAuthor = lstAuthors.SelectedItem as Author;
            LoadData(SelectedAuthor);
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
            SelectedAuthor = null;
            txtUsername.Text = string.Empty;
            txtName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            dtpBirthdate.Value = DateTime.Now;

            lstAuthors.ClearSelected();
        }

        private void LoadAuthors()
        {
            lstAuthors.DataSource = null;
            lstAuthors.DataSource = repository.GetAll();
        }

        private void LoadData(Author author)
        {
            CleanMessage();

            txtUsername.Text = author.Username;
            txtName.Text = author.Name;
            txtLastName.Text = author.LastName;
            dtpBirthdate.Value = author.Birthdate.Value;
        }

        #endregion
    }
}
