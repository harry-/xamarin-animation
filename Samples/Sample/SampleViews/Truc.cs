using SkiaSharp;
using SkiaSharp.Elements;
using System;

namespace Sample.SampleViews
{
    class Truc
    {
        public int Mass { get; set; } = 10;
        private SKPoint _location = new SKPoint(0, 0);

        public virtual Element Shape { set; get; }
        protected static Random rnd = new Random();

        public virtual SKPoint Location
        {
            set { _location = value; }
            get { return _location; }
        }

        public float X
        {
            get { return Shape.X; }
            set { Shape.X = value; }
        }

        public float Y
        {
            get { return Shape.Y; }
            set { Shape.Y = value; }
        }

        public int Size
        {
            get { return  (int)Shape.Size.Height; }
            set { Shape.Size = new SKSize(value, value); }
        }

        public virtual SKColor Color { get; set; }


    }
}
