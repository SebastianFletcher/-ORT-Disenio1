using System.Windows.Forms;

namespace UserInterface.DTO
{
    public class Alerts
    {
        public static void CrashApp(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            Application.Exit();
        }
    }
}
