using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace CinemaTicketing
{
    public partial class BookingInfo : Form
    {
        public static string title;
        public static string desc;
        public static int price;
        public static string dateWatch;
        public static string Watchtime;

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();

        public BookingInfo(string MovieName)
        {
            InitializeComponent();
            title = lblTitle.Text;
            title = MovieName;
            lblName.Text = Login.name;
            lblContact.Text = Login.contact;
            desc = txtDesc.Text;
            lblTitle.Text = title;
            lblTitle.Text = MovieName;
            watchDate.MinDate = DateTime.Now;
            watchDate.MaxDate = DateTime.Now.AddDays(7);
        }
        public BookingInfo()
        {
            MessageBox.Show("No Movie found!");
        }

        private void BookingInfo_Load(object sender, EventArgs e)
        {
            desc = Movie_Section.passingText;
            title = Movie_Section.passingTitle;
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + title + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                title = dr["title"].ToString();
                txtDesc.Text = dr["description"].ToString();
                pictureBox1.Image = GetImage((byte[])dr["image"]);
            }
            else
            {
                MessageBox.Show("No Movie found!");
            }
        }

        private Image GetImage(byte[] img)
        {
            MemoryStream ms = new MemoryStream(img);
            return Image.FromStream(ms);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + title + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                string nprice = dr["price"].ToString();
                price = Convert.ToInt32(nprice);
                cn = new SqlConnection(db.MyConnection());
                cn.Open();
                cm = new SqlCommand($"INSERT INTO tblBookings(name, date, time, tickets, total, contact) VALUES(@name, @date, @time, @tickets, @total, @contact)", cn);
                cm.Parameters.AddWithValue("@name", lblName.Text);
                cm.Parameters.AddWithValue("@date", watchDate.Value);
                DateTime time = Convert.ToDateTime(cmbTime.Text);
                cm.Parameters.AddWithValue("@time", time.ToString("HH:mm"));
                cm.Parameters.AddWithValue("@tickets", txtTicket.Text);
                cm.Parameters.AddWithValue("@total", price * Convert.ToInt32(txtTicket.Text));
                cm.Parameters.AddWithValue("@contact", lblContact.Text);
                Watchtime = cmbTime.Text;
                dateWatch = watchDate.Value.ToString("yyyy-MM-dd");
                cm.ExecuteNonQuery();
                cn.Close();

                string ticket = txtTicket.Text;
                Seats seat = new Seats(title, ticket, price);
                seat.ShowDialog();
                this.Hide();
            }
            else
            {
                MessageBox.Show("No Movie found!");
            }
            cn.Close();
            dr.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Movie_Section bck = new Movie_Section();
            bck.Show();
        }
    }
    }

