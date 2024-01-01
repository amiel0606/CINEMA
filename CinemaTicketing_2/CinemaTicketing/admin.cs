using Microsoft.Win32;
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
    public partial class admin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        dbCon db = new dbCon();
        public admin()
        {
            InitializeComponent();
            cn = new SqlConnection(db.MyConnection());
            showMovies();
            SystemEvents.DisplaySettingsChanged += new EventHandler(SystemEvents_DisplaySettingsChanged);
            this.Resize += new EventHandler(Form1_Resize);
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            this.MinimumSize = new Size(800, 600);
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            btnAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            label1.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left;

        }
            
        private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
        {
            dataGridView1.Left = (this.ClientSize.Width - dataGridView1.Width) / 2;
            dataGridView1.Top = (this.ClientSize.Height - dataGridView1.Height) / 2;
            btnAdd.Left = (this.ClientSize.Width - btnAdd.Width) / 2;
            btnAdd.Top = (this.ClientSize.Height - btnAdd.Height) / 2;
            label1.Left = (this.ClientSize.Width - label1.Width) / 2;
            label1.Top = (this.ClientSize.Height - label1.Height) / 2;
            label2.Left = (this.ClientSize.Width - label2.Width) / 2;
            label2.Top = (this.ClientSize.Height - label2.Height) / 2;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            dataGridView1.Left = Math.Max((this.ClientSize.Width - dataGridView1.Width) / 2, 0);
            dataGridView1.Top = Math.Max((this.ClientSize.Height - dataGridView1.Height) / 2, 0);
            btnAdd.Left = Math.Max((this.ClientSize.Width - btnAdd.Width) / 2, 0);
            btnAdd.Top = Math.Max((this.ClientSize.Height - btnAdd.Height) / 2, 0);
            label1.Left = Math.Max((this.ClientSize.Width - label1.Width) / 2, 0);
            label1.Top = Math.Max((this.ClientSize.Height - label1.Height) / 2, 0);
            label2.Left = Math.Max((this.ClientSize.Width - label2.Width) / 2, 0);
            label2.Top = Math.Max((this.ClientSize.Height - label2.Height) / 2, 0);
        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SystemEvents.DisplaySettingsChanged -= SystemEvents_DisplaySettingsChanged;
        }
        public void showMovies()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblMovies", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(dr["mID"].ToString(),i, dr["title"].ToString(), dr["price"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            addMovies frm = new addMovies(this);
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "edit")
            {
                addMovies frm = new addMovies(this);
                frm.lblD.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtMovie.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.txtPresyo.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblMovies WHERE title = '"+frm.lblD.Text+"'", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    frm.txtDesc.Text = dr["description"].ToString();
                }
                dr.Close();
                cn.Close();
                frm.btnAdd.Enabled = false;
                frm.btnUpdate.Enabled = true;
                frm.ShowDialog();
            }
            else if (colName == "delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this movie?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblMovies WHERE title like '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    string movieID = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    dropSeatTable(Convert.ToInt32( movieID));
                    cn.Close();
                    MessageBox.Show("Movie Deleted!");
                    showMovies();
                }
            }
        }

        public void dropSeatTable(int movieID)
        {
            cn = new SqlConnection(db.MyConnection());
            cn.Open();
            string sql = $"DROP TABLE seats_for_movie_{movieID}";
            cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
    }
}
