using SkiaSharp;
using SkiaSharp.Elements;

namespace Sample.SampleViews
{
    class Square : Steerable
    {
        public SkiaSharp.Elements.Rectangle _rectangle = new Rectangle(new SKRect(0, 0, 0, 0));

        public Square(int maxX, int maxY, int maxSize) :base(maxX, maxY, maxSize)
        {
            _rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(Location,  new SKSize(Size, Size)));
            _rectangle.FillColor = new SKColor((byte)rnd.Next(256), (byte)rnd.Next(256), (byte)rnd.Next(256));
        }

        public override SKPoint Location
        {
            get { return _rectangle.Location; }
            set { _rectangle.Location = value; }
        }

        public override Element Shape
        {
            get { return _rectangle; }
        }
    }
}
