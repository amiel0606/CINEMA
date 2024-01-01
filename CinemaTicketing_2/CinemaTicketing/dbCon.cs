using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CinemaTicketing
{
    internal class dbCon
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        public string MyConnection()
        {
            string con = @"Data Source=HEART;Initial Catalog=dbCinema;Integrated Security=True";
            return con;
        }
    }
}