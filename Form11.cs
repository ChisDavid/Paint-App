using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace Paint
{
   
    public partial class Form11 : Form
    {
        int database = 0;
        TextBox t = new TextBox();
        Graphics g;
        Color currentColor = Color.Black;
        int k=0, x = -1, y = -1, counter = 0, mouseX = 0, mouseY = 0, xMoved = 0, yMoved = 0;
        bool moving = false, ok = false;
        Pen pen;
        ComboBox comboBox;
        string comand = "DrowLine";
        string pensize = "5";
        Bitmap canvas, move;
        Image lastImage;
        ArrayList FormsRedo = new ArrayList();
        int XPressed = 0, YPressed = 0, XUnpressed = 0, YUnpressed = 0;
        ArrayList FormsList = new ArrayList(), ZoomList = new ArrayList();
        ArrayList SelectPoints = new ArrayList();
        RadioButton SelectButton;
        Pen pen1 = new Pen(Color.Black, 0);
        bool Zoomcheck=false;
        TrackBar trackBar;
        ArrayList FormsAdded = new ArrayList();
        ArrayList FormsAddedNames = new ArrayList();

        public Form11(string name,int database)
        {
            this.database = database;
             DBHelper.EstablishConnection();
            this.KeyPreview = true;
             this.KeyDown += new KeyEventHandler(KeyProcedure);
             InitializeComponent();
             canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
             MainPictureBox.Image = canvas;
             g = Graphics.FromImage(canvas);
             pen = new Pen(Color.Black, 5);
             pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
             this.Width = 1920;
             this.Height = 1080;
             MainPictureBox.Width = this.Width;
             MainPictureBox.Height = this.Height-115;
             pen = new Pen(Color.Black, 5);
             pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
             int counter = 0;
             #region FormsAdded

            string PathForms = Path.Combine(Directory.GetCurrentDirectory(), "Forms.ser");
            
            FileStream file = new FileStream(PathForms, FileMode.Open);
            BinaryFormatter b = new BinaryFormatter();
            if(file.Length!=0)
            {
                FormsAdded = (ArrayList)b.Deserialize(file);
            }
            
            
            file.Close();


            string PathFormsNames = Path.Combine(Directory.GetCurrentDirectory(), "FormsNames.ser");
            FileStream file1 = new FileStream(PathFormsNames, FileMode.Open);
            BinaryFormatter b1 = new BinaryFormatter();
            if (file1.Length != 0)
            {
                FormsAddedNames = (ArrayList)b.Deserialize(file1);
            }
            if (FormsAddedNames.Count != 0)
            {
                for (int i = 0; i < FormsAddedNames.Count; i++)
                {
                    this.FormsComboBox.Items.Add(((FormsAddedNames as ArrayList)[i]) as String);
                }
            }


            file1.Close();


            #endregion
             #region ThemeColor

            if(database==0)
            {
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "Theme.txt");
                StreamReader ftheme = new StreamReader(PathTheme);
                string l1 = "", l2 = "";

                l1 = ftheme.ReadLine();
                if (l1 != null)
                {
                    while (l1 != null)
                    {
                        l2 = l1;
                        l1 = ftheme.ReadLine();
                    }
                    string[] rgb1 = l2.Split(' ');
                    Color c1 = Color.FromArgb(Int32.Parse(rgb1[0]), Int32.Parse(rgb1[1]), Int32.Parse(rgb1[2]));
                  
                    this.BackColor = c1;
                    this.panel1.BackColor = c1;
                    this.MainPictureBox.BackColor = c1;

                }
                ftheme.Close();
            }
            else
            {
                
                string PathTheme = Path.Combine(Directory.GetCurrentDirectory(), "IndexColorTheme.txt");
                if (File.Exists(PathTheme))
                {
                    StreamReader ff = new StreamReader(PathTheme);
                    int index = Convert.ToInt32(ff.ReadLine());
                 //   MessageBox.Show(index + "");
                    ff.Close();
                    Theme t =  ( DaCommand.getUser("id", index + "", "Theme"))[0] as Theme;
                   // MessageBox.Show(t.R+" "+t.G+" "+t.B + "");
                    Color c = Color.FromArgb(t.R, t.G, t.B);
                    this.BackColor = c;
                    this.panel1.BackColor = c;
                    this.MainPictureBox.BackColor = c;

                }
            
            }
          
            #endregion
             #region AddColor
            if (database==0)
            {
                string PathColors = Path.Combine(Directory.GetCurrentDirectory(), "RGBColors.txt");
                MessageBox.Show(PathColors, PathColors, MessageBoxButtons.YesNoCancel);
             
                StreamReader f = new StreamReader(PathColors);
                string line = f.ReadLine();
                if (line != null)
                {
                    while (line != null)
                    {
                       
                        string[] rgb = line.Split(' ');
                        Color c = Color.FromArgb(Int32.Parse(rgb[0]), Int32.Parse(rgb[1]), Int32.Parse(rgb[2]));
                        PictureBox p = new PictureBox();
                        p.BackColor = c;
                        p.Location = new System.Drawing.Point(counter, 27);
                        p.Name = "redPanel";
                        p.Size = new System.Drawing.Size(28, 26);
                        p.TabIndex = 1;
                        p.TabStop = false;
                        p.Click += new System.EventHandler(this.ChangeColor);
                        this.ColorsPanel.Controls.Add(p);
                        line = f.ReadLine();
                        counter += 34;
                    

                    }
                }
                f.Close();
            }
            else
            {  

                ArrayList colors = DaCommand.select("Colors");
               
                if(colors!=null)
                {
                    for (int i= 0;i < colors.Count;i++)
                    {
                       // MessageBox.Show((colors[i] as Culoare).R + "");
                        Color c = Color.FromArgb((colors[i]as Culoare).Red1, (colors[i] as Culoare).Green1, (colors[i] as Culoare).Blue1);
                        PictureBox p = new PictureBox();
                        p.BackColor = c;
                        p.Location = new System.Drawing.Point(counter, 27);
                        p.Name = "New Color";
                        p.Size = new System.Drawing.Size(28, 26);
                        p.TabIndex = 1;
                        p.TabStop = false;
                        p.Click += new System.EventHandler(this.ChangeColor);
                        this.ColorsPanel.Controls.Add(p);
                        counter += 34;
                    }
                   
                }
                
            }
           
             #endregion
             #region ButtonAdmin
             if (name=="admin")
             {
                 this.EditAdmin.Visible = true;
             }
             else
             {  
                 this.EditAdmin.Visible = false; 
             }
             
             #endregion
        }


        #region Keyboard

        public void Close(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public void MouseZoom(object sender, MouseEventArgs e)
        {
            /*   Debug1.Text = e.Delta + "";
               if (e.Delta>0)
               {
                   canvas = new Bitmap(canvas,MainPictureBox.Width-e.Delta, MainPictureBox.Height-e.Delta);
                   ZoomList.Add(canvas.Clone());
                   g = Graphics.FromImage(canvas);
               }
               else
               {
                  *//* canvas = (Image)ZoomList[ZoomList.Count - 1] as Bitmap;
                   g = Graphics.FromImage(canvas);*//*
               }*/
        }
        void KeyProcedure(object sender, KeyEventArgs e)
        {
            FillButton.Checked = false;
            if (e.Control && e.KeyCode == Keys.V)
            {
                g.DrawImage(Clipboard.GetImage(), new Point(0, 0));

            }
            if (e.Control && e.KeyCode == Keys.Y && FormsRedo.Count != 0)
            {

                Image newImage = (Image)FormsRedo[FormsRedo.Count - 1];
                MainPictureBox.Image = newImage;
                canvas = (Image)FormsRedo[FormsRedo.Count - 1] as Bitmap;
                FormsList.Add(canvas.Clone());
                g = Graphics.FromImage(canvas);
                FormsRedo.RemoveAt(FormsRedo.Count - 1);
                comand = "DrowLine";
            }
            if (e.Control && e.KeyCode == Keys.Z && FormsList.Count != 0)
            {

                FormsRedo.Add(canvas.Clone());
                Image newImage = (Image)FormsList[FormsList.Count - 1];
                MainPictureBox.Image = newImage;
                canvas = (Image)FormsList[FormsList.Count - 1] as Bitmap;
                g = Graphics.FromImage(canvas);

                FormsList.RemoveAt(FormsList.Count - 1);
                comand = "DrowLine";

            }



            if (e.Control && e.KeyCode == Keys.S)
            {  /* Debug.Text = "Am apasat Ctrl+v";*/
                SaveFileDialog s = new SaveFileDialog();
                s.FileName = "untitled";
                s.DefaultExt = ".jpg";
                s.Filter = "Image (.jpg)|*.jpg";
                s.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                s.RestoreDirectory = true;
                if (s.ShowDialog() == DialogResult.OK)
                {
                    string filename = s.FileName;
                    if (System.IO.File.Exists(filename))
                        System.IO.File.Delete(filename);
                    (FormsList[FormsList.Count - 1] as Image).Save(filename);
                }
            }
        }

        #endregion
        #region Mouse
        public Bitmap CropImage(Bitmap source, Rectangle section)
        {

            Bitmap rez = new Bitmap(section.Width, section.Height);
          /*  Debug.Text = section.Height + " " + section.Width;
            Debug.Text = section.X + " " + section.Y;*/
            for (int i = section.X; i < section.X + section.Width; i++)
            {
                for (int j = section.Y; j < section.Y + section.Height; j++)
                {
                    rez.SetPixel(i - section.X, j - section.Y, source.GetPixel(i, j));

                }
            }
            return rez;
        }
        public Bitmap CropImage1(Bitmap source, Rectangle section)
        {

            Bitmap rez = new Bitmap(section.Width, section.Height);
           /* Debug.Text = section.Height + " " + section.Width;
            Debug.Text = section.X + " " + section.Y;*/
            for (int i = section.X; i < section.X + section.Width; i++)
            {
                for (int j = section.Y; j < section.Y + section.Height; j++)
                {
                    try
                    {
                        rez.SetPixel(i - section.X, j - section.Y, source.GetPixel(i, j));
                    }
                    catch (Exception e)
                    {
                       /* Debug1.Text = e.Message;*/
                    }
                }
            }
            return rez;
        }
        private void MainPanel_MouseDown(object sender, MouseEventArgs e)
        {
            FormsList.Add(canvas.Clone());
            moving = true;
            x = e.X;
            y = e.Y;
            XPressed = e.X;
            YPressed = e.Y;
     /*       Debug1.Text = k + "";*/

            t.Dispose();

          
            if (comand == "FillColor" && FillButton.Checked)
            {
                FloodFill(canvas, e.Location, canvas.GetPixel(e.X, e.Y), currentColor);
            }
            if (comand == "Move")
            {
                Point p1, p2, p3, p4;
                p1 = (Point)SelectPoints[0];
                p2 = (Point)SelectPoints[1];
                p3 = (Point)SelectPoints[2];
                p4 = (Point)SelectPoints[3];
                if (e.X > p1.X && e.X < p2.X && e.Y > p1.Y && e.Y < p3.Y)
                {
                    comand = "MoveAgain";
                    xMoved = e.X;
                    yMoved = e.Y;

                    move = CropImage(canvas, new Rectangle((int)(p1.X + pen1.Width + 1), (int)(p1.Y + pen1.Width + 1), (int)(p2.X - p1.X - pen1.Width - 1), (int)(p3.Y - p2.Y - pen1.Width - 1)));


                }


            }


        }
        void NewText(object sender, EventArgs e)
        {
            g.DrawString((sender as TextBox).Text, (sender as TextBox).Font, new SolidBrush(pen.Color), (sender as TextBox).Location);
            k = 1;
            MainPictureBox.Controls.Remove(sender as TextBox);

        }
        private void MainPanel_MouseUp(object sender, MouseEventArgs e)
        {



            moving = false;
            x = -1;
            y = -1;
            XUnpressed = e.X;
            YUnpressed = e.Y;

           
            if (this.IntroducereText.Checked)
            {
            /*    Debug.Text = "Aici";*/
                t = new TextBox();
                t.SetBounds(XPressed, YPressed, XUnpressed - XPressed, YUnpressed - YPressed);
                MainPictureBox.Controls.Add(t);
                t.Leave += new EventHandler(NewText);

                this.IntroducereText.Checked = false;
            }


            for (int i = 0; i < FormsAddedNames.Count; i++)
            {
                if (comand == "Drow" + (FormsAddedNames as ArrayList)[i])
                {
                    g.DrawImage(((FormsAdded as ArrayList)[i] as Bitmap), new Rectangle(XPressed, YPressed, XUnpressed - XPressed, YUnpressed - YPressed));
                    break;
                }
            }

            if (comand == "MoveAgain" && SelectPoints.Count != 0)
            {
                int x1 = e.X - xMoved;
                int y1 = e.Y - yMoved;
                Point a = (Point)SelectPoints[0];
                Point b = (Point)SelectPoints[1];
                Point c = (Point)SelectPoints[2];
                Point d = (Point)SelectPoints[3];

                for (float i = a.X - pen1.Width; i <= b.X + pen1.Width; i++)
                {
                    for (float j = a.Y - pen1.Width; j <= c.Y + pen1.Width; j++)
                    {
                        if (MainPictureBox.BackColor == Color.White)
                        {
                            canvas.SetPixel((int)i, (int)j, canvas.GetPixel(MainPictureBox.Width - 1, MainPictureBox.Height - 1));
                        }
                        else
                        {
                            canvas.SetPixel((int)i, (int)j, Color.White);
                        }

                    }
                }

                a.X = a.X + x1;
                b.X = b.X + x1 - 1;
                c.X = c.X + x1 - 1;
                d.X = d.X + x1;
                a.Y = a.Y + y1;
                b.Y = b.Y + y1;
                c.Y = c.Y + y1 - 5;
                d.Y = d.Y + y1 - 5;
                Point[] p = new Point[4];
                p[0] = a;
                p[1] = b;
                p[2] = c;
                p[3] = d;
                SelectPoints.Clear();
                FillButton.Checked = false;
                g.DrawImage(move, a);
               


            }
            if (comand == "DrowElipse")
            {

                FillButton.Checked = false;
                g.DrawEllipse(pen, new Rectangle(XPressed, YPressed, XUnpressed - XPressed, YUnpressed - YPressed));
            }
            if (comand == "DrowCircle")
            {
                Circle c = new Circle(XPressed, XUnpressed, YPressed, YUnpressed, pen, g);
                FillButton.Checked = false;
                c.drow();

               

            }

            if (comand == "DrowSquare")
            {
                x = XPressed;
                y = YPressed;
                Point[] p = new Point[4];
                p[0] = new Point(x, y);
                p[1] = new Point(XUnpressed, y);
                p[2] = new Point(XUnpressed, YUnpressed);
                p[3] = new Point(x, YUnpressed);
                /*    g.DrawPolygon(pen, p);
                    g1.DrawPolygon(pen, p);*/
                Square s = new Square(pen, p, g);
                FillButton.Checked = false;
                s.drow();



            }
            if (comand == "Select" && SelectButton.Checked)

            {
                x = XPressed;
                y = YPressed;
                Point[] p = new Point[4];
                p[0] = new Point(x, y);
                p[1] = new Point(XUnpressed, y);
                p[2] = new Point(XUnpressed, YUnpressed);
                p[3] = new Point(x, YUnpressed);
                Pen pen1 = new Pen(Color.Black, 0);

                g.DrawPolygon(pen1, p);
                // g1.DrawPolygon(pen1, p);
                foreach (Point i in p)
                {
                    SelectPoints.Add(i);
                }
                SelectButton.Checked = false;
                comand = "Move";


            }
            if (comand == "DrowTriangle")
            {

                Point[] p = new Point[3];
                p[0] = new Point(XPressed, YPressed);
                p[1] = new Point(XUnpressed, YUnpressed);
                p[2] = new Point(2 * XPressed - XUnpressed, YUnpressed);
                FillButton.Checked = false;
                //   g1.DrawPolygon(pen, p);
                g.DrawPolygon(pen, p);

            }
            if (comand == "Drow Straight Line")
            {
                FillButton.Checked = false;
                g.DrawLine(pen, new Point(XPressed, YPressed), new Point(XUnpressed, YUnpressed));
                //   g1.DrawLine(pen, new Point(XPressed, YPressed), new Point(XUnpressed, YUnpressed));


            }
            if (comand == "Clear All")
            {
               /* Debug1.Text = "am intrat";*/
                MainPictureBox.Refresh();
                comand = "DrowLine";
            }
            MainPictureBox.Image = canvas;


        }
        private void MainPanel_MouseMove(object sender, MouseEventArgs e)
        {


            if (moving && x != -1 && y != -1 && comand == "DrowLine" && this.IntroducereText.Checked == false)
            {
                Brush p;
                ok = true;
                g.DrawLine(pen, new Point(x, y), e.Location);
                //  g1.DrawLine(pen, new Point(x, y), e.Location);
                MainPictureBox.Image = canvas;

                x = e.X;
                y = e.Y;

            }
           

            if (comboBox != null && comboBox.SelectedItem != "Shapes" && moving && x != -1 && y != -1 && comand == "ComboBox")
            {

                switch ((string)comboBox.SelectedItem)
                {

                    case "Triangle":
                        comand = "DrowTriangle";
                        break;
                    case "Square":
                        comand = "DrowSquare";
                        break;
                    case "Circle":
                        comand = "DrowCircle";
                        break;
                    case "Straight Line":
                        comand = "Drow Straight Line";
                        break;
                    case "Line":
                        comand = "DrowLine";
                        break;
                    case "Elipse":
                        comand = "DrowElipse";
                        break;
                    default:
                        foreach (string i in FormsAddedNames)
                        {
                            if (comboBox.SelectedItem == i)
                            {
                                comand = "Drow" + i;
                            }
                         
                        }
                        break;

                }
            }




        }

        private void LogOutButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            App a = new App(database);
            a.ShowDialog();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            trackBar = sender as TrackBar;
            trackBar.Maximum = 100;
            Zoom.Text = trackBar.Value + " ";
            comand = "ZoomInOut";
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }


        private void IntroduceText_CheckedChanged(object sender, EventArgs e)
        {
            this.IntroducereText = sender as RadioButton;
        }

        private void EditAdmin_Click(object sender, EventArgs e)
        {
            EditAdmin admin = new EditAdmin(this,database);
            admin.ShowDialog();

        }

        private void comboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            comand = "ColorComboBox";
            ComboBox c = sender as ComboBox;


            string[] rgb = c.SelectedItem.ToString().Split();

            Color color = new Color();
            color = Color.FromArgb(Int32.Parse(rgb[0]), Int32.Parse(rgb[1]), Int32.Parse(rgb[2]));
            c.BackColor = color;
            ColorNow.BackColor = color;
            //  c.BackColor = new Color (c.SelectedItem)
        }





        #endregion

        #region Buttons
        private void ChangeSize_Click(object sender, EventArgs e)
        {
            pen = new Pen(currentColor, Int32.Parse(pensize));
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            FillButton.Checked = false;
            comand = "DrowLine";



        }


        private void ChangePenSize_TextChanged(object sender, EventArgs e)
        {
            TextBox t = (TextBox)sender;
            pensize = t.Text;
            comand = "DrowLine";
        }


        private void PenButton_Click(object sender, EventArgs e)
        {
            int penz;
            if (Int32.Parse(pensize) != 0)
            {
                penz = Int32.Parse(pensize);
            }
            else
            {
                penz = 5;
            }
            pen = new Pen(Color.Black, penz);
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            comand = "DrowLine";
            FillButton.Checked = false;
        }


        private void Select_CheckedChanged(object sender, EventArgs e)
        {
            SelectButton = (RadioButton)sender;
            FillButton.Checked = false;
            comand = "Select";

        }

        private void eraseButton_Click(object sender, EventArgs e)
        {

            int penz = 5;
            if (Int32.Parse(pensize) != 0)
            {
                penz = Int32.Parse(pensize);
            }

            Color old = pen.Color;
            pen.Color = Color.White;
            pen.Width = penz;
            pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            FillButton.Checked = false;

            comand = "DrowLine";


        }

        private void ChangeColor(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            currentColor = p.BackColor;
            ColorNow.BackColor = currentColor;
            pen.Color = p.BackColor;
        }



        private void ClearAll_Click(object sender, EventArgs e)
        {
            
            //  g1 = MainPictureBox.CreateGraphics();
            //  g1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            canvas = new Bitmap(MainPictureBox.Width, MainPictureBox.Height);
            MainPictureBox.Image = canvas;
            g = Graphics.FromImage(MainPictureBox.Image);
            MainPictureBox.BackColor = Color.White;
            FillButton.Checked = false;
            comand = "DrowLine";

        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox = (ComboBox)sender;
            comand = "ComboBox";
        }

        #endregion
        #region Fill
        private void FillButton_CheckedChanged(object sender, EventArgs e)
        {
            if (FillButton.Checked == true)
            {

                comand = "FillColor";
            }

        }

        private void FloodFill(Bitmap bmp, Point pt, Color targetColor, Color replacementColor)
        {
            Stack<Point> pixels = new Stack<Point>();
            targetColor = bmp.GetPixel(pt.X, pt.Y);
            int y1;
            bool spanLeft, spanRight;
            pixels.Push(pt);
            while (pixels.Count != 0)
            {
                Point temp = pixels.Pop();
                y1 = temp.Y;
                while (y1 > 0 && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    y1--;
                }
                if (y1 == 0)
                {

                    MainPictureBox.BackColor = replacementColor;

                }

                y1++;
                spanLeft = false;
                spanRight = false;
                while (y1 < bmp.Height && bmp.GetPixel(temp.X, y1) == targetColor)
                {
                    bmp.SetPixel(temp.X, y1, replacementColor);
                    if (!spanLeft && temp.X > 0 && bmp.GetPixel(temp.X - 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X - 1, y1));
                        spanLeft = true;
                    }
                    else if (spanLeft && temp.X - 1 == 0 && bmp.GetPixel(temp.X - 1, y1) != targetColor)
                    {
                        spanLeft = false;
                    }
                    if (!spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) == targetColor)
                    {
                        pixels.Push(new Point(temp.X + 1, y1));
                        spanRight = true;
                    }
                    else if (spanRight && temp.X < bmp.Width - 1 && bmp.GetPixel(temp.X + 1, y1) != targetColor)
                    {
                        spanRight = false;
                    }
                    y1++;
                }
            }
            MainPictureBox.Refresh();
            return;
        }


        #endregion
    }
}
