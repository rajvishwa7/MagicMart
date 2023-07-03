using Microsoft.Office.Interop.Excel;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace MagicMart
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
            display();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connStr"].ConnectionString);
        System.Data.DataTable dt;

        private void btnSave_Click(object sender, System.EventArgs e)
        {
            if (txtName.Text == "" || txtEmail.Text == "" || txtMobile.Text == "" || txtAge.Text == "" || txtUserId.Text == "" || txtCity.Text == "")
            {
                MessageBox.Show("Please Fill All Required Details");
            }
            else
            {
                try
                {
                    string gender;
                    if (rbtnMale.Checked)
                    {
                        gender = "Male";
                    }
                    else { gender = "Female"; }

                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    SqlCommand cmd = new SqlCommand("sp_insert", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@uName", txtName.Text);
                    cmd.Parameters.AddWithValue("@uEmail", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@uMobile", txtMobile.Text);
                    cmd.Parameters.AddWithValue("@uAge", txtAge.Text);
                    cmd.Parameters.AddWithValue("@uId", txtUserId.Text);
                    cmd.Parameters.AddWithValue("@uGender", gender);
                    cmd.Parameters.AddWithValue("@uAddress", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@uCity", txtCity.Text);
                    cmd.Parameters.AddWithValue("@uState", txtState.Text);

                    int i = cmd.ExecuteNonQuery();

                    if (i > 0)
                    {
                        MessageBox.Show("User Data Inserted Perfectly");
                        clear();
                        display();
                    }
                    else
                    {
                        MessageBox.Show("User Data Not Inserted");
                    }

                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
                finally { conn.Close(); }
            }
        }

        public void clear()
        {
            txtName.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtMobile.Text = string.Empty;
            txtAge.Text = string.Empty;
            txtUserId.Text = string.Empty;
            txtAddress.Text = string.Empty;
            txtCity.Text = string.Empty;
            txtState.Text = string.Empty;
        }

        public void display()
        {
            try
            {
                dt = new System.Data.DataTable();
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_fetch", conn);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        int ID;
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtName.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtEmail.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtMobile.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtAge.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtUserId.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtAddress.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            txtCity.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
            txtState.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();

            rbtnMale.Checked = true;
            rbtnFemale.Checked = false;


            if (dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() == "Female")
            {
                rbtnMale.Checked = false;
                rbtnFemale.Checked = true;
            }
            else
            {
                rbtnMale.Checked = true;
                rbtnFemale.Checked = false;
            }

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void btnUpdate_Click(object sender, System.EventArgs e)
        {
            try
            {
                string gender;
                if (rbtnMale.Checked)
                {
                    gender = "Male";
                }
                else { gender = "Female"; }

                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_update", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@uName", txtName.Text);
                cmd.Parameters.AddWithValue("@uEmail", txtEmail.Text);
                cmd.Parameters.AddWithValue("@uMobile", txtMobile.Text);
                cmd.Parameters.AddWithValue("@uAge", txtAge.Text);
                cmd.Parameters.AddWithValue("@uId", txtUserId.Text);
                cmd.Parameters.AddWithValue("@uGender", gender);
                cmd.Parameters.AddWithValue("@uAddress", txtAddress.Text);
                cmd.Parameters.AddWithValue("@uCity", txtCity.Text);
                cmd.Parameters.AddWithValue("@uState", txtState.Text);
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("User Data Updated Perfectly");
                display();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnDelete_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                SqlCommand cmd = new SqlCommand("sp_delete", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.ExecuteNonQuery();

                MessageBox.Show("User Data Deleted Perfectly");
                display();
                clear();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void txtSearch_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                dt = new System.Data.DataTable();
                SqlCommand cmd = new SqlCommand("sp_search", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@searchData", txtSearch.Text);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
        }

        private void btnExport_Click(object sender, System.EventArgs e)
        {
            try
            {
                Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application(); // create object of Excel
                Workbook workbook = excel.Workbooks.Add(XlSheetType.xlWorksheet); // create workbook object
                Worksheet worksheet = (Worksheet)excel.ActiveSheet; // create worksheet object
                excel.Visible = true;

                for (int j = 2; j <= dataGridView1.Rows.Count; j++) // need to count gridview Rows using nested for loop
                {
                    for (int i = 1; i <= 1; i++)
                    {
                        worksheet.Cells[j, i] = dataGridView1.Rows[j - 2].Cells[i - 1].Value;
                    }
                }

                for (int i = 1; i <= dataGridView1.Columns.Count; i++) // need to count the column which is available on the gridview
                {
                    worksheet.Cells[1, i] = dataGridView1.Columns[i - 1].HeaderText;
                }

                for (int i = 0; i < dataGridView1.Rows.Count; i++) // here you will get all the data from gridview
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        worksheet.Cells[i + 2, j + 1] = dataGridView1.Rows[i].Cells[j].Value?.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
