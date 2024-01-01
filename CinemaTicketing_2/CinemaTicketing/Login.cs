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
    public partial class Login : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();
        public static string LoggedIn;
        public static string contact;
        public static string name;
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            Landing lnd = new Landing();
            lnd.Show();
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Text;
            string role = cmbRole.Text;
            try
            {
                cn = new SqlConnection(db.MyConnection());
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblUsers WHERE username = @username AND password = @password AND role = @role", cn);
                cm.Parameters.AddWithValue("@username", username);
                cm.Parameters.AddWithValue("@password", password);
                cm.Parameters.AddWithValue("@role", role);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    name = dr["name"].ToString();
                    LoggedIn = dr["username"].ToString();
                    contact = dr["contact"].ToString();
                    MessageBox.Show("Successfully Logged In!");
                    if (role =="Admin")
                    {
                        admin ad = new admin();
                        ad.Show();
                        this.Dispose();
                    }
                    else if (role == "Customer")
                    {
                        Movie_Section ms = new Movie_Section();
                        ms.Show();
                        this.Dispose();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Role!");
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!");
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
            Landing lnd = new Landing();
            lnd.Show();
            this.Dispose();
        }
    }
}
