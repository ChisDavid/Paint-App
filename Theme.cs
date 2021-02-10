using System;
using System.Collections.Generic;
using System.Text;

namespace Paint
{
    

    class Theme
    {
        int id;
        string Nume;
        int r, g, b;

        public Theme (int id,string nume,int a, int b, int c)
        {
            this.Id = id;
            this.Nume1 = nume;
            this.R = a;
            this.G = b;
            this.B = c;
        }

        public int R { get => r; set => r = value; }
        public int G { get => g; set => g = value; }
        public int B { get => b; set => b = value; }
        public string Nume1 { get => Nume; set => Nume = value; }
        public int Id { get => id; set => id = value; }
    }
}
