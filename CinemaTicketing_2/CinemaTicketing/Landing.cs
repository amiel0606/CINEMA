using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketing
{
    public partial class Landing : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();
        public Landing()
        {
            InitializeComponent();
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            string username = txtUserName.Text;
            string password = txtPass.Text;
            string contact = txtContact.Text;
            string role = cmbRole.Text;
            try
            {
                if (name.Length != 0 && username.Length != 0 && password.Length != 0 && contact.Length != 0)
                {
                    cn = new SqlConnection(db.MyConnection());
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblUsers (name, username, password, contact, role) values (@name, @username, @password, @contact, @role)", cn);
                    cm.Parameters.AddWithValue("@name", name);
                    cm.Parameters.AddWithValue("@username", username);
                    cm.Parameters.AddWithValue("@password", password);
                    cm.Parameters.AddWithValue("@contact", contact);
                    cm.Parameters.AddWithValue("@role", role);
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Successfully Registered!");
                    txtName.Clear();
                    txtUserName.Clear();
                    txtPass.Clear();
                    txtContact.Clear();
                    cn.Close();
                }
                else
                {
                    MessageBox.Show("Please fill up all fields!");
                }

            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            this.Hide();
        }
    }
}
