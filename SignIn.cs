using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Paint;
using MySqlConnector;

namespace Paint
{
    
    public partial class SignIn : Form
    {
        TextBox Name,Email, SurName,UserName;
        int database = 0;
     
        public SignIn(int database)
        {
            InitializeComponent();
            this.database = database;
        }

        private void SurNameTextBox_TextChanged(object sender, EventArgs e)
        {
            SurName = sender as TextBox;
        }
        public void CloseSignIn(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        public string CreatePassword(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length --)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
     
        private void SignInButton_Click(object sender, EventArgs e)
        {
            Message.Text = "";
            try
            {

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("pointapp27@gmail.com");
                    mail.To.Add(Email.Text);
                    mail.Subject = "Create cont on Graphic Editor 2D";
                    Random r = new Random();

                    string pass = CreatePassword(10);
                    mail.Body = "<h> Your password is  </h>" +
                        "<h>"+ pass+ " And your UserName is "+ UserName.Text+" </h>" +
                        "";
                    mail.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential("pointapp27@gmail.com", "qwertyuiop27");
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                        Message.Text = "Mail was send";
                        
                        User user = new User(Name.Text, SurName.Text, pass + "",UserName.Text, Email.Text);
                        if (database==0)
                        {
                            string PathUseri = Path.Combine(Directory.GetCurrentDirectory(), "Useri.txt");
                            StreamWriter f = File.AppendText(PathUseri);
                            f.Write(Name.Text + " " + SurName.Text + " " + pass + " " + UserName.Text + " " + Email.Text);
                            f.Close();
                        }
                        else
                        {


                            int ok = 1;
                            if (ok==0)
                            {
                                try
                                {
                                    DBHelper.con.Open();
                                    StreamWriter f1 = File.AppendText("comand.txt");
                                    
                                    string sql = "insert into Useri(Nume,Prenume,Email,Username,Pass) values ('" + Name.Text.Trim() + "','" +
                                   SurName.Text.Trim() + "','" + Email.Text.Trim() + "','" + UserName.Text.Trim() + "','" + pass.Trim() + "')";
                                    

                                    f1.Write(sql);
                                    f1.Close();
                                    MySqlCommand commmand = new MySqlCommand(sql, DBHelper.con);
                                    if (commmand.ExecuteNonQuery() == 1)
                                    {
                                        MessageBox.Show("Data Inserted");
                                    }
                                    else
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
                            else
                            {
                                ArrayList values = new ArrayList();
                                values.Add(Name.Text);
                                values.Add(SurName.Text);
                                values.Add(Email.Text);
                                values.Add(UserName.Text);
                                values.Add(pass);
                                ArrayList columns = new ArrayList();
                                columns.Add("Nume");
                                columns.Add("Prenume");
                                columns.Add("Email");
                                columns.Add("Username");
                                columns.Add("Pass");
                                DaCommand.insert("Useri", columns, values,"string");

                            }
                            
                        }
                       

                        

                    }
                }

                App a = new App(database);
                this.Hide();
                a.ShowDialog();

            }
            catch (Exception ex)
            {
                Message.Text = ex.Message;
            }
           
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            UserName = sender as TextBox;
        }

        private void EmailTextBox_TextChanged(object sender, EventArgs e)
        {
            Email = sender as TextBox;
        }

        private void NameTextBox_TextChanged(object sender, EventArgs e)
        {
            Name = sender as TextBox;
        }
    }
}
