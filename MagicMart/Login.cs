using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace MagicMart
{
    public partial class Login : Form
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLoginUName.Text == "" && txtLoginUPwd.Text == "")
                {
                    MessageBox.Show("Please Enter Login User Name & Password");
                }
                else
                {
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_login", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Lname", txtLoginUName.Text);
                    cmd.Parameters.AddWithValue("@Lpwd", txtLoginUPwd.Text);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);


                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        MessageBox.Show("Awesome.. Welcome to Magic Mart");
                        MagicMart mm = new MagicMart();
                        mm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Please Check Login User Name & Password");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtLoginUName.Text = string.Empty;
            txtLoginUPwd.Text = string.Empty;
        }
    }
}
