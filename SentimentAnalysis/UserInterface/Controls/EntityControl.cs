using System.Windows.Forms;
using BusinessLogic.DTO;
using BusinessLogic.Exceptions;
using Persistence;
using Persistence.Repositories;
using UserInterface.DTO;

namespace UserInterface.Controls
{
    public partial class EntityControl : UserControl
    {
        private const string SAVE_ENTITY = "Entity saved correctly.";
        private const string MODIFY_ENTITY = "Entity modify correctly.";
        private const string DELETE_ENTITY = "Entity deleted.";


        private IRepository<Entity> repository;
        private Entity SelectedEntity;


        public EntityControl()
        {
            repository = new EntityRepository();

            InitializeComponent();

            try
            {
                LoadEntities();
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

                Entity entity = new Entity(txtName.Text);

                if (SelectedEntity == null)
                {
                    repository.Add(entity);
                    message = SAVE_ENTITY;
                }
                else
                {
                    entity.Id = SelectedEntity.Id;
                    repository.Modify(SelectedEntity.Id, entity);
                    message = MODIFY_ENTITY;
                }

                LoadEntities();
                ShowSuccess(message);
                CleanFields();
            }
            catch (EntityException ex)
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
                repository.Delete(SelectedEntity != null ? SelectedEntity.Id : (int?) null);
                LoadEntities();
                CleanFields();
                ShowSuccess(DELETE_ENTITY);
            }
            catch (EntityException ex)
            {
                ShowError(ex.Message);
            }
        }

        private void lstEntities_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (lstEntities.SelectedItem is null)
                return;

            SelectedEntity = lstEntities.SelectedItem as Entity;
            LoadData(SelectedEntity);
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
            SelectedEntity = null;
            txtName.Text = string.Empty;

            lstEntities.ClearSelected();
        }

        private void LoadEntities()
        {
            lstEntities.DataSource = null;
            lstEntities.DataSource = repository.GetAll();
        }

        private void LoadData(Entity entity)
        {
            CleanMessage();

            txtName.Text = entity.Name;
        }

        #endregion
    }
}
