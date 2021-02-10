using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Text;

namespace Paint
{
    
    class Square
    {
        Point[] points = new Point[4];
        Pen pen;
        Graphics g;
    
      public  Square(Pen p,Point [] s,Graphics gr)
        {
            this.points = s;
            this.pen = p;
            this.g = gr;
         

        }
      public  void drow()
        {
          this.g.DrawPolygon(this.pen, this.points);
         
        }
    }
}
