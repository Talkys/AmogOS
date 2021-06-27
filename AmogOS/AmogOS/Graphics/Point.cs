using System;
using System.Collections.Generic;
using System.Text;

namespace AmogOS.Graphics
{
    class Point
    {
        /*Atributos*/
        public int x, y;

        /*Construtores*/
        public Point()            { this.x = 0;this.y = 0; }
        public Point(int val)     { this.x = val;this.y = val; }
        public Point(int x,int y) { this.x = x;this.y = y; }

        /*Constantes*/
        public static Point zero { get { return new Point(0); } }
        public static Point one  { get { return new Point(1); } }
    }
}
