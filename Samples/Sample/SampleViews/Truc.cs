using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.SampleViews
{
    class Truc
    {
        public int x { get; set; }
        public int y { get; set; }
        public int oldX { get; set; }
        public int oldY { get; set; }
        public int dirX { get; set; }
        public int dirY { get; set; }
        public float speed { get; set; }

        public SkiaSharp.Elements.Rectangle _rectangle;

        protected static Random rnd = new Random();

        public Truc(int maxX, int maxY)
        {
            x = rnd.Next(maxX);
            y = rnd.Next(maxY);
        }

        public Truc() { }

        public void move()
        {
            x += dirX;
            y += dirY;
        }
        
    }
}
