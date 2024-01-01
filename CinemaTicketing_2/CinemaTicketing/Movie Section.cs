using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketing
{
    public partial class Movie_Section : Form
    {
        public static string passingTitle;
        public static string passingText;

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();
        public Movie_Section()
        {
            InitializeComponent();
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies", cn);
            dr = cm.ExecuteReader();

            PictureBox[] pictureBoxes = { pictureBox1, pictureBox2, pictureBox3, pictureBox4, pictureBox5, pictureBox6, pictureBox7, pictureBox8, pictureBox9, pictureBox10, pictureBox11, pictureBox12, pictureBox13, pictureBox14 };
            Label[] labels = { lblT1, lblT2, lblT3, lblT4, lblT5, lblT6, lblT7, lblT8, lblT9, lblT10, lblT11, lblT12, lblT13, lblT14 };
            Button[] buttons = { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14 }; 

            int i = 0;
            while (i < pictureBoxes.Length)
            {
                if (dr.Read())
                {
                    if (!(dr["image"] is DBNull))
                    {
                        pictureBoxes[i].Image = GetImage((byte[])dr["image"]);
                        labels[i].Text = dr["title"].ToString();
                    }
                }
                else
                {
                    pictureBoxes[i].Visible = false;
                    labels[i].Visible = false;
                    buttons[i].Visible = false;
                }
                i++;
            }
        }
        private Image GetImage(byte[] img)
        {
            MemoryStream ms = new MemoryStream(img);
            return Image.FromStream(ms);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT1.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        public void getSeats(int movieID)
        {
            cn.Open();
            string tableName = "seats_for_movie_" + movieID;
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE mID = '" + movieID + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT2.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT3.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnAfter_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT4.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnBarbie_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT5.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnEqua_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT6.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnExp_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT7.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnFnaf_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT8.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnGT_Click(object sender, EventArgs e)
        {
            
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT9.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnMI_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT10.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingTitle = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnOppen_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT11.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingText = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnFrozen_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT12.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingText = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btn3000_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT13.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingText = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }

        private void btnSawX_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + lblT14.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                BookingInfo ss = new BookingInfo(dr["title"].ToString());
                passingText = dr["title"].ToString();
                passingText = dr["description"].ToString();
                dr.Close();
                cn.Close();
                ss.Show();
                this.Dispose();
            }
        }
    }
}
