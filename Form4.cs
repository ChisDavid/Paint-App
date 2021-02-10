using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;

namespace Paint
{
    public partial class EditAdmin : Form
    {
        int database = 0;
        Form11 LastForm;
        Graphics g,g1;
        Bitmap canvas;
        Pen pen;
        int  x = -1, y = -1;
        bool moving;
        string command = "DrowPoint";
        ArrayList Forms = new ArrayList();
        ArrayList FormsNames = new ArrayList();
        public EditAdmin(Form11 a,int database)
        {
            this.database = database;
            InitializeComponent();
            this.KeyPreview = true;
            canvas = new Bitmap(DrowPictureBox.Width, DrowPictureBox.Height);
            DrowPictureBox.Image = canvas;
            g = DrowPictureBox.CreateGraphics();
            g1 = Graphics.FromImage(canvas);
            this.LastForm = a;
            pen = new Pen(Color.Black, 5);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

        }
      
        private void Save_Click(object sender, EventArgs e)
        {

            this.Hide();
            LastForm.Hide();
          
           

            /*    Debug.Text = "Am apasat Ctrl+v";
                SaveFileDialog s1 = new SaveFileDialog();
                s1.FileName = "untitled";
                s1.DefaultExt = ".jpg";
                s1.Filter = "Image (.jpg)|*.jpg";
                s1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                s1.RestoreDirectory = true;
                if (s1.ShowDialog() == DialogResult.OK)
                {
                    string filename = s1.FileName;
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);
                    canvas.Save(filename);
                }
    */
          /*   a.Add(new Label());
            b = a;
            Debug.Text = b.Count + " ";
*/

            Form11 f = new Form11("admin",database);
            f.ShowDialog();
           
     
           
            
            
            
            
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            ColorPictureBox.BackColor = Color.FromArgb(RedTrackBar.Value, GreenTrackbar.Value, BlueTrackbar.Value);
            RedTextBox.Text = RedTrackBar.Value + "";
            GreenTextBox.Text = GreenTrackbar.Value + "";
            BlueTextBox.Text = BlueTrackbar.Value + "";
            
        }

        private void Whitetheme_CheckedChanged(object sender, EventArgs e)
        {
            if (database==0)
            {
                this.Whitetheme = sender as RadioButton;
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "Theme.txt");
                StreamWriter s = File.AppendText(PathTheme);
                s.WriteLine(Color.White.R + " " + Color.White.G + " " + Color.White.B);
                s.Close();
               
            }
            else
            {
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "IndexColorTheme.txt");
                StreamWriter s = new StreamWriter(PathTheme);
                s.WriteLine(1+"");
                s.Close();
            }
          

        }

        private void DarkTheme_CheckedChanged(object sender, EventArgs e)
        {
            if (database==0)
            {
                this.DarkTheme = sender as RadioButton;
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "Theme.txt");
                StreamWriter s = File.AppendText(PathTheme);
                s.WriteLine(Color.DarkGreen.R + " " + Color.DarkGreen.G + " " + Color.DarkGreen.B);
                s.Close();
            }
            else
            {
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "IndexColorTheme.txt");
                StreamWriter s = new StreamWriter(PathTheme);
                s.WriteLine(2 + "");
                s.Close();
            }
            
        }

        private void userTextBox_TextChanged(object sender, EventArgs e)
        {
            this.userTextBox = sender as TextBox;
        }

        private void DrowPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            moving = true;
            x = e.X;
            y = e.Y;
        }

        private void DrowPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            moving = false;
            x = -1;
            y = -1;
           
        }

        private void addFormButton_Click(object sender, EventArgs e)
        {
            try
            {


                string PathForms = Path.Combine(Directory.GetCurrentDirectory(), "Forms.ser");
                FileStream file = new FileStream(PathForms, FileMode.Open);
                BinaryFormatter b_Deserialize = new BinaryFormatter();
                if (file.Length!=0)
                {
                    Forms = (ArrayList)b_Deserialize.Deserialize(file);
                }

                file.Close();

                Forms.Add(canvas);
                string PathForms1 = Path.Combine(Directory.GetCurrentDirectory(), "Forms.ser");
                FileStream f = new FileStream(PathForms1, FileMode.Open);
                BinaryFormatter b_Serialize = new BinaryFormatter();
                b_Serialize.Serialize(f, Forms);
                f.Close();
                f.Dispose();




                string PathFormsNames = Path.Combine(Directory.GetCurrentDirectory(), "FormsNames.ser");
                FileStream FileNames = new FileStream(PathFormsNames, FileMode.Open);
                BinaryFormatter bin = new BinaryFormatter();
                if (FileNames.Length!=0)
                {
                    FormsNames = (ArrayList)bin.Deserialize(FileNames);
                }
                FileNames.Close();

                FormsNames.Add(this.NameFigurePictureBox.Text);

                string PathFormsNames1 = Path.Combine(Directory.GetCurrentDirectory(), "FormsNames.ser");
                FileStream fileNames = new FileStream(PathFormsNames1, FileMode.Open);
                BinaryFormatter bin1 = new BinaryFormatter();
                bin1.Serialize(fileNames, FormsNames);
                fileNames.Close();
                fileNames.Dispose();

            }
            catch (Exception e1)
            {
            /*    Debug1.Text = e1.Message;*/
            }
           
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (database == 0)
            {
               
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "Theme.txt");
                StreamWriter s = File.AppendText(PathTheme);
                s.WriteLine(Color.Green.R + " " + Color.Green.G + " " + Color.Green.B);
                s.Close();
            }
            else
            {
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "IndexColorTheme.txt");
                StreamWriter s = new StreamWriter(PathTheme);
                s.WriteLine(3 + "");
                s.Close();
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (database == 0)
            {
                this.Whitetheme = sender as RadioButton;
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "Theme.txt");
                StreamWriter s = File.AppendText(PathTheme);
                s.WriteLine(Color.BlueViolet.R + " " + Color.BlueViolet.G + " " + Color.BlueViolet.B);
                s.Close();
            }
            else
            {
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "IndexColorTheme.txt");
                StreamWriter s = new StreamWriter(PathTheme);
                s.WriteLine(4 + "");
                s.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void AddColoor_Click(object sender, EventArgs e)
        {
            if (this.database == 0)
            {
                string PathColors = Path.Combine(Directory.GetCurrentDirectory(), "RGBColors.txt");
                StreamWriter s = File.AppendText(PathColors);
                s.WriteLine(ColorPictureBox.BackColor.R + " " + ColorPictureBox.BackColor.G + " " + ColorPictureBox.BackColor.B);
                s.Close();
            }
            else
            {
               // MessageBox.Show("am intrat aici");
                ArrayList values = new ArrayList();
                values.Add(Convert.ToInt32(ColorPictureBox.BackColor.R));
                values.Add(Convert.ToInt32(ColorPictureBox.BackColor.G));
                values.Add(Convert.ToInt32(ColorPictureBox.BackColor.B));
                //MessageBox.Show(values.Count + "");
                ArrayList columns = new ArrayList();
                columns.Add("r");
                columns.Add("g");
                columns.Add("b");
                DaCommand.insert("Colors", columns, values, "int");
             //   MessageBox.Show("Culori Count+" + values.Count + "");

            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            ////DELETE - from database
            if (database == 0)
            {

                string PathUseri = Path.Combine(Directory.GetCurrentDirectory(), "Useri.txt");
                string[] line = File.ReadAllLines(PathUseri);
                string scris = "";
                foreach (string i in line)
                {
                    if (i != "")
                    {
                        string[] str = i.Split(' ');
                        if (str[3] != this.userTextBox.Text)
                        {
                            Debug.Text = i;
                            scris += i + "\n";
                        }
                    }

                }
                try
                {

                    StreamWriter file = new StreamWriter(PathUseri);
                    file.WriteLine(scris);
                    file.Close();
                }
                catch (Exception e111)
                {
                    Debug.Text = e111.Message;
                }

            }
            else
            {
                if (this.userTextBox.Text != "")
                {
                   // MessageBox.Show("Urmeaza sa stergem");
                    DaCommand.delete("Useri", "Username", this.userTextBox.Text);
                }

            }
        }

        private void NameFigurePictureBox_TextChanged(object sender, EventArgs e)
        {
            this.NameFigurePictureBox = sender as TextBox;
        }

        private void DrowPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if(moving && x!=-1 && y!=-1 && command == "DrowPoint")
            {
               /* Debug1.Text = "Here";*/
                g.DrawLine(pen, new Point(x, y), e.Location);
                g1.DrawLine(pen, new Point(x, y), e.Location);
                x = e.X;
                y = e.Y;
            }
        }
    }
}
