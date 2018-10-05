using SkiaSharp;
using SkiaSharp.Elements;
using System;

namespace Sample.SampleViews
{
    class Circle : Steerable
    {
        public SkiaSharp.Elements.Ellipse _ellipse = new Ellipse(new SKPoint(0, 0), Single.Epsilon);

        public Circle(int maxX, int maxY, int maxSize) :base(maxX, maxY, maxSize)
        {
            _ellipse = new SkiaSharp.Elements.Ellipse(Location, Size);
            _ellipse.FillColor = new SKColor((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
        }

        public override SKPoint Location
        {
            get { return _ellipse.Location; }
            set { _ellipse.Location = value; }
        }

        public override Element Shape
        {
            get { return _ellipse; }
        }


        public SKColor Color
        {
            get { return _ellipse.FillColor;}
            set
            {
                _ellipse.FillColor = value;
            }
        }
    }
}
