using System;
using Android.OS;
using SkiaSharp;
using System.Diagnostics;
using Debug = Android.OS.Debug;


namespace Sample.SampleViews
{
    class Steerable : Truc
    {
        public Vector MovementVector { get; set; } = new Vector(0, 0);
        public Vector OldMovementVector { get; set; } = new Vector(0, 0);
        public Vector Acceleration { get; set; } = new Vector(0, 0);
        public Vector UnitDirection { get { return MovementVector.unitVector(); }  }

        public Steerable(int maxX, int maxY)
        {
            SKPoint loc = new SKPoint(rnd.Next(maxX), rnd.Next(maxY));
            _rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(loc, new SKSize(20, 20)))
            {
                FillColor = SKColors.SteelBlue
            };
        }

        public Steerable(int maxX, int maxY, int maxSize)
        {
            System.Diagnostics.Debug.WriteLine("constructor max values: " + maxX + '/' + maxY + '/' + maxSize);
            try
            {
                size = rnd.Next(maxSize);

                SKPoint loc = new SKPoint(rnd.Next(maxX), rnd.Next(maxY));
                _rectangle = new SkiaSharp.Elements.Rectangle(SKRect.Create(loc, new SKSize(size, size)))
                {
                    FillColor = SKColors.SteelBlue
                };
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                System.Diagnostics.Debug.WriteLine(e.Message);
            }


        }
    }
}
