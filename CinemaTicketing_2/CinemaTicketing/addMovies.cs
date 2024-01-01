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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CinemaTicketing
{
    public partial class addMovies : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;

        dbCon db = new dbCon();
        admin admin;
        public addMovies(admin adm)
        {
            InitializeComponent();
            cn = new SqlConnection(db.MyConnection());
            admin = adm;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMovie.Text) && !string.IsNullOrEmpty(txtPresyo.Text) && !string.IsNullOrEmpty(txtDesc.Text))
                {
                    if (MessageBox.Show("Add this Movie?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT INTO tblMovies (title, description, price, image) values (@title, @description, @price, @image)", cn);
                        cm.Parameters.AddWithValue("@title", txtMovie.Text);
                        cm.Parameters.AddWithValue("@description", txtDesc.Text);
                        cm.Parameters.AddWithValue("@price", txtPresyo.Text);
                        cm.Parameters.AddWithValue("@image", getPhoto());
                        cm.ExecuteNonQuery();
                        CreateSeatsForMovie(selectMovies(txtMovie.Text));
                        cn.Close();
                        dr.Close();
                        MessageBox.Show("Movie Added!");
                        txtMovie.Clear();
                        txtDesc.Clear();
                        txtPresyo.Clear();
                        admin.showMovies();
                        this.Dispose();
                    }
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

        public void CreateSeatsForMovie(int movieID)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            string tableName = "seats_for_movie_" + movieID;
            string sql = $@"CREATE TABLE {tableName} (
                                seat_id INT PRIMARY KEY IDENTITY NOT NULL,
                                RowNumber INT NOT NULL,
                                SeatNumber INT NOT NULL,
                                isBooked VARCHAR(50) DEFAULT 'Available');";
            cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        public int selectMovies(string title)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '" + title + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            int movieID = Convert.ToInt32(dr["mID"]);
            dr.Close();
            cn.Close();
            return movieID;
        }

        public byte[] getPhoto()
        {
            MemoryStream ms = new MemoryStream();
            pictureBox1.Image.Save(ms, pictureBox1.Image.RawFormat);
            return ms.GetBuffer();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMovie.Text) && !string.IsNullOrEmpty(txtPresyo.Text) && !string.IsNullOrEmpty(txtDesc.Text))
                {
                    if (MessageBox.Show("Are you sure you want to update this product?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblMovies SET title = @title, description = @description, price = @price, image = @image WHERE title like '" + lblD.Text + "'", cn);
                        cm.Parameters.AddWithValue("@title", txtMovie.Text);
                        cm.Parameters.AddWithValue("@description", txtDesc.Text);
                        cm.Parameters.AddWithValue("@price", txtPresyo.Text);
                        cm.Parameters.AddWithValue("@image", getPhoto());
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Movie Updated!");
                        txtMovie.Clear();
                        txtDesc.Clear();
                        txtPresyo.Clear();
                        admin.showMovies();
                        this.Dispose();
                    }
                }
                else
                {
                    MessageBox.Show("Please fill up all fields!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Choose Image(*.jpg; *.png; *.gif)|*.jpg; *.png; *.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = new Bitmap(ofd.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Hide();
        }

    }
}
