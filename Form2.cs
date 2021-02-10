using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Paint
{
    public partial class App : Form
    {
        TextBox UserName, Password;
        ArrayList users = new ArrayList();
        int database = 0;
        public App(int database)
        {
            DBHelper.EstablishConnection();
            this.database = database;
            this.KeyPreview = true;
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(KeyProcedure);
            
        }

        private void UserTextBox_TextChanged(object sender, EventArgs e)
        {
            UserName = (TextBox)sender;

        }
        public void CloseApp(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        private void LogInButton_Click(object sender, EventArgs e)
        {

           if (database==0)
            {
                StreamReader f;
                string PathUseri = Path.Combine(Directory.GetCurrentDirectory(), "Useri.txt");
                f = new StreamReader(PathUseri);
                string line = f.ReadLine();
                Debug.Text = line;
                bool pass = false;
                bool user = false;
                UserName.Text = UserName.Text.Trim();
                while (line != null)
                {
                    if (line != "")
                    {
                        string[] s = line.Split(' ');

                        if (Password.Text == s[2])
                        {
                            pass = true;
                        }
                        if (UserName.Text == s[3])
                        {
                            user = true;
                            if (Password.Text == s[2])
                            {
                                pass = true;
                                Form11 f1 = new Form11(UserName.Text,database);
                                f1.ShowDialog();
                                this.Hide();
                               
                                break;
                            }
                        }

                    }
                    line = f.ReadLine();
                }
                if (user == false)
                {
                    Debug.Text = "Wrong USername";
                }
                if (pass == false)
                {
                    Debug.Text = "Wrong Password";
                }

                f.Close();



            }
           else
            {
                ArrayList a = DaCommand.getUser("Username".Trim(), UserName.Text.Trim(), "Useri".Trim());
                User u = new User("a", "b", "c", "d", "e");
                if (a != null)
                {
                    u = (a[0] as User);
                    if (u != null)
                    {
                        Form11 f1 = new Form11(UserName.Text, database);
                        this.Hide();
                        f1.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("User not found", "Error", MessageBoxButtons.YesNo);
                    }
                }
            }



        }
        void KeyProcedure(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                StreamReader f;


                string PathUseri = Path.Combine(Directory.GetCurrentDirectory(), "Useri.txt");
                f = new StreamReader(PathUseri);
                string line = f.ReadLine();
                Debug.Text = line;
                bool pass = false;
                bool user = false;
                while (line != null)
                {
                    if (line != "")
                    {
                        string[] s = line.Split(' ');
                        if (UserName.Text == s[3])
                        {
                            user = true;
                            if (Password.Text == s[2])
                            {
                                pass = true;
                                Form11 f1 = new Form11(UserName.Text,database);
                                f1.ShowDialog();
                                
                                this.Hide();
                              
                                break;
                            }
                        }

                    }
                    line = f.ReadLine();
                }
                if (user == false)
                {
                    Debug.Text = "Wrong USername";
                }

                f.Close();


            }
        }

        private void MakeAccountButton_Click(object sender, EventArgs e)
        { 
            SignIn m = new SignIn(database);
            this.Hide();
            m.ShowDialog();
        }

        private void App_Load(object sender, EventArgs e)
        {

        }

        private void PasswordTextField_TextChanged(object sender, EventArgs e)
        {
            Password = sender as TextBox;
        }
    }
}
