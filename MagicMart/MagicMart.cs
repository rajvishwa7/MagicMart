using System;
using System.Windows.Forms;

namespace MagicMart
{
    public partial class MagicMart : Form
    {
        Registration rg = new Registration();
        public MagicMart()
        {
            InitializeComponent();
        }

        private void logOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

        private void registerBtn_Click(object sender, EventArgs e)
        {
            rg.Show();
        }
    }
}
