using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketing
{
    public partial class Seats : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();
        int selectedSeats = 0;
        int total;
        public Seats(string MovieName, string tickets, int price)
        {
            InitializeComponent();
            lblTicket.Text = tickets.ToString();
            lblTitle.Text = MovieName;
            int tickets1 = int.Parse(tickets);
            total = tickets1 * price;
            lblTotal.Text = total.ToString();
            int numberOfSeats = 16;
            Button[] button = { Row1Seat1, Row1Seat2, Row1Seat3, Row1Seat4, Row2Seat1, Row2Seat2, Row2Seat3, Row2Seat4, Row3Seat1, Row3Seat2, Row3Seat3, Row3Seat4, Row4Seat1, Row4Seat2, Row4Seat3, Row4Seat4 };
            for (int i = 0; i<numberOfSeats; i++)
            {
                button[i].Click += Button_Click;
            }
            this.Load += new EventHandler(Seats_Load);
            FetchSeatsFromDatabase();

        }
        private void Button_Click(object sender, EventArgs e)
        {
            Row1Seat1.Name = "Row1Seat1";
            Row1Seat2.Name = "Row1Seat2";
            Row1Seat3.Name = "Row1Seat3";
            Row1Seat4.Name = "Row1Seat4";
            Row2Seat1.Name = "Row2Seat1";
            Row2Seat2.Name = "Row2Seat2";
            Row2Seat3.Name = "Row2Seat3";
            Row2Seat4.Name = "Row2Seat4";
            Row3Seat1.Name = "Row3Seat1";
            Row3Seat2.Name = "Row3Seat2";
            Row3Seat3.Name = "Row3Seat3";
            Row3Seat4.Name = "Row3Seat4";
            Row4Seat1.Name = "Row4Seat1";
            Row4Seat2.Name = "Row4Seat2";
            Row4Seat3.Name = "Row4Seat3";
            Row4Seat4.Name = "Row4Seat4";
            Button button = sender as Button;
            int numSeats = Int32.Parse(lblTicket.Text);
            

            if (button.BackColor == Color.Green)
            {
                if (selectedSeats < numSeats)
                {
                    button.BackColor = Color.Red;
                    selectedSeats++;
                    Console.WriteLine(selectedSeats);
                    string seatIdentifier = button.Name;
                    string[] parts = seatIdentifier.Split(new string[] { "Seat" }, StringSplitOptions.None);
                    int rowNumber = int.Parse(parts[0].Substring(3));
                    int seatNumber = int.Parse(parts[1]);
                    cn = new SqlConnection(db.MyConnection());
                    cn.Open();
                    int movieID = selectMovies(lblTitle.Text);
                    cm = new SqlCommand($"INSERT INTO seats_for_movie_{movieID} (RowNumber, SeatNumber, isBooked) values (@rowNumber, @seatNumber, 'taken')", cn);
                    cm.Parameters.AddWithValue("@rowNumber", rowNumber);
                    cm.Parameters.AddWithValue("@seatNumber", seatNumber);
                    cm.ExecuteNonQuery();
                }
                else
                {
                    MessageBox.Show("You have selected the maximum number of seats");
                }
            } 
            else
            {
                selectedSeats--;
                string seatIdentifier = button.Name;
                string[] parts = seatIdentifier.Split(new string[] { "Seat" }, StringSplitOptions.None);
                int rowNumber = int.Parse(parts[0].Substring(3));
                int seatNumber = int.Parse(parts[1]);
                cn = new SqlConnection(db.MyConnection());
                cn.Open();
                int movieID = selectMovies(lblTitle.Text);
                cm = new SqlCommand($"DELETE FROM seats_for_movie_{movieID} WHERE RowNumber = {rowNumber} AND SeatNumber = {seatNumber}", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                button.BackColor = Color.Green;
            }
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
            return movieID;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            int tickets = int.Parse(lblTicket.Text);
            Receipt frm = new Receipt(lblTitle.Text , tickets, total);
            frm.Show();
            this.Dispose();
        }


        private void Seats_Load(object sender, EventArgs e)
        {
            FetchSeatsFromDatabase();
        }
        private void FetchSeatsFromDatabase()
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            string sqlMovieID = $"SELECT * FROM tblMovies WHERE title = '{lblTitle.Text}'";
            cm = new SqlCommand(sqlMovieID, cn);
            dr = cm.ExecuteReader();
            dr.Read();
            int movieID = Convert.ToInt32(dr["mID"]);
            string sql = $"SELECT * FROM seats_for_movie_{movieID}";
            dr.Close();
            cm = new SqlCommand(sql, cn);
            dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    string rowNumber = dr["RowNumber"].ToString();
                    string seatNumber = dr["SeatNumber"].ToString();
                    string isBooked = dr["IsBooked"].ToString();
                    string seatIdentifier = "Row" + rowNumber + "Seat" + seatNumber;
                    Button seatButton = this.Controls.Find(seatIdentifier, true).FirstOrDefault() as Button;
                        if (seatButton != null)
                        {
                            if (isBooked == "taken")
                            {
                                seatButton.BackColor = Color.Red;
                                seatButton.Enabled = false;
                            }
                            else
                            {
                                seatButton.BackColor = Color.Green;
                                seatButton.Enabled = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Seat not found");
                        }
                }

        }

    }
}
    
