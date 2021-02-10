using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Paint
{
    class Circle
    {
        int  x1, x2, x3, x4;
        float radius;
        Pen p;
        Graphics g;

        public Circle (int a,int b, int c,int d,Pen pen ,Graphics g)
        {
            this.x1 = a;
            this.x2 = b;
            this.x3 = c;
            this.x4 = d;
            this. radius = (float)Math.Sqrt(((a - b) * (a - b)) + ((c - d) * (c - d)));
            this.p = pen;
            this.g = g;
        }
        public void drow()
        {
           this. g.DrawEllipse(this.p, this.x1 - radius, this.x3 - radius, 2 * radius, 2 * radius);
        }
    }
}
