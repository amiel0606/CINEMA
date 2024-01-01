using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CinemaTicketing
{
    public partial class Receipt : Form
    {

        public Receipt(string title, int tickets, int amount)
        {
            InitializeComponent();
            txt_title.Text = title;
            txt_tickets.Text = tickets.ToString();
            txt_amount.Text = amount.ToString();
            txtName.Text = Login.name;
            txtContact.Text = Login.contact;
            txtDate.Text = BookingInfo.dateWatch;
            txtTime.Text = BookingInfo.Watchtime;
        }

        private void Receipt_FormClosing(object sender, FormClosingEventArgs e)
        {
            var sagot = MessageBox.Show("Thank you for booking with us", "EXIT", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (sagot.ToString() == "Okay")
            {
                e.Cancel = true;
            }
        }
    }
}
