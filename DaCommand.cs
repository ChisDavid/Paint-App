
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Numerics;
using System.Text;
using System.Windows.Forms;

using MySqlConnector;

namespace Paint
{
    class DaCommand
    {
      
        private static MySqlCommand cmd = null;
        private static DataTable table;
        private static MySqlDataAdapter sda;
        public static ArrayList getUser(string column,string username,string Tablename)
        {
            ArrayList objects = new ArrayList();
            string query = "select * from "+Tablename+" where "+column+" = (@Nume)";
            cmd = DBHelper.runQuerry(query, username);
            User user = null;
           
            if (column=="Username")
            {
                if (cmd != null)
                {
                    table = new DataTable();
                    sda = new MySqlDataAdapter(cmd);
                    sda.Fill(table);
                    foreach (DataRow i in table.Rows)
                    {
                        string id = i["id"].ToString();
                        string Name = i["Nume"].ToString();
                        string Prenume = i["Prenume"].ToString();
                        string Email = i["Email"].ToString();
                        string Username = i["Username"].ToString();
                        string Pass = i["Pass"].ToString();
                        user = new User(Name, Prenume, Pass, Username, Email);
                        objects.Add(user);

                    }
                }
            }
            if (column == "id")
            {
                if (cmd != null)
                {
                    table = new DataTable();
                    sda = new MySqlDataAdapter(cmd);
                    sda.Fill(table);
                    foreach (DataRow i in table.Rows)
                    {
                        int id = Convert.ToInt32(i["id"].ToString());
                        string Name = i["Nume"].ToString();
                        int R = Convert.ToInt32(i["R"].ToString());
                        int G = Convert.ToInt32(i["G"].ToString());
                        int B = Convert.ToInt32(i["B"].ToString());
                        objects.Add(new Theme(id, Name, R, G, B));

                    }
                }
            }
            return objects;

        }
        public static ArrayList select (string TableName)
        {
            ArrayList objects = new ArrayList();
            string querry = "select * from " + TableName;
            cmd = DBHelper.runQuerry0(querry);
            if (TableName == "Colors")
            {
                Culoare c = null;      
                if (cmd != null)
                {
                    table = new DataTable();
                    sda = new MySqlDataAdapter(cmd);
                    sda.Fill(table);
                    foreach (DataRow i in table.Rows)
                    {
                        int r = Convert.ToInt32(i["r"]);
                        int g = Convert.ToInt32(i["g"]);
                        int b = Convert.ToInt32(i["b"]);

                        c = new Culoare(r, g, b);
                        objects.Add(c);

                    }

                }

            }
            return objects;
            
        }
        public static void insert (string TableName , ArrayList columns,ArrayList values,string type)
        {
            try
            {
                DBHelper.con.Open();
                string sql = "insert into " + TableName +"( ";
                int ok = 1;
                for (int i = 0; i < columns.Count - 1; i++)
                {
                    sql += (string)(columns[i]) + " ,";
                }

                sql += (string)(columns[columns.Count - 1]);

                sql += " )  values ( \' ";
                switch (type)
                {
                    case "string":
                       

                        for (int i = 0; i < values.Count - 1; i++)
                        {
                            sql += (string)(values[i]) + " \' , \' ";
                        }
                        sql += (string)(values[values.Count - 1]) + " \' )";

                        break;
                    case "int":

                        for (int i = 0; i < values.Count - 1; i++)
                        {
                            sql += (Int32)(values[i]) + " \' , \' ";
                        }
                        sql += (int)(values[values.Count - 1]) + " \' )";
                        break;



                }
                
                MySqlCommand commmand = new MySqlCommand(sql, DBHelper.con);
               
                if (commmand.ExecuteNonQuery() == 0)
                {
                    MessageBox.Show("Error Data not inserted");
                }
                DBHelper.con.Close();
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "Error", MessageBoxButtons.OKCancel); 
            }
        }
        public static void delete (string TableName,string column,string value)
        {
            
            DBHelper.con.Open();
            string sql = " delete from " + TableName + " where " + column +" = " + "\""+value+"\"";
           
            cmd = new MySqlCommand(sql, DBHelper.con);
            try
            {
                cmd.ExecuteScalar();
                if (cmd.ExecuteReader()!=null)
                {
                    MessageBox.Show("Deleted");
                }
            }
            catch (MySqlException ee)
            {
                MessageBox.Show(ee.Message);
            }
            
            DBHelper.con.Close();
        }

    }
}
