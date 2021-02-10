using MySqlConnector;
using System;
using System.Data;
using System.Windows.Forms;


namespace Paint
{
    class DBHelper
    {
        public static MySqlConnection con;
        private static MySqlCommand cmd = null;
        private static DataTable table;
        private static MySqlDataAdapter sda;


        public static void EstablishConnection()
        {
            try
            {
               
                con = new MySqlConnection("datasource=127.0.0.1;port=3309;database=Paint;username=root;password=David27****");
               
            }
            catch (Exception eee)
            {
                
                MessageBox.Show(eee.Message, "Connection", MessageBoxButtons.OK);

            }
        }
        public static MySqlCommand runQuerry(string querry,string username)
        {
            try
            {
                if (con!=null)
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = querry;
                    cmd.Parameters.AddWithValue("@Nume", username);
                    cmd.ExecuteNonQuery();
                    con.Close();
                

                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Connection", MessageBoxButtons.OK);
            }
       
            return cmd;
        }
        public static MySqlCommand runQuerry0(string querry)
        {
            try
            {
                if (con != null)
                {
                    con.Open();
                    cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = querry;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Connection", MessageBoxButtons.OK);
            }

            return cmd;
        }


    }
}
