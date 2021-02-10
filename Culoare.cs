using System;
using System.Collections.Generic;
using System.Text;

namespace Paint
{
    class Culoare
    {
         int Red, Green, Blue;
        public Culoare(int red, int blue, int green)
        {
            this.Red = red;
            this.Green = green;
            this.Blue = blue;
        }

        public int Red1 { get => Red; set => Red = value; }
        public int Green1 { get => Green; set => Green = value; }
        public int Blue1 { get => Blue; set => Blue = value; }
    }
}
