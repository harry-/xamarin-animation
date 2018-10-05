using Android.OS;
using SkiaSharp;
using System.Diagnostics;
using Debug = System.Diagnostics.Debug;

namespace Sample.SampleViews
{
    class Steerable : Truc
    {
        public Vector MovementVector { get; set; } = new Vector(0, 0);
        public Vector OldMovementVector { get; set; } = new Vector(0, 0);
        public Vector Acceleration { get; set; } = new Vector(0, 0);
        public Vector UnitDirection { get { return MovementVector.unitVector(); }  }

        public Steerable(int maxX, int maxY, int maxSize)
        {
            Size = rnd.Next(maxSize)+20;
            Mass = Size;
            Location = new SKPoint(rnd.Next(maxX), rnd.Next(maxY));
        }

        public void AddAttractionVector(Steerable theOther)
        {
            Vector attractionVector = new Vector(Location, theOther.Location);
            double gforce = Gravitation.Force(this, theOther);
            attractionVector.x *= gforce/Mass;
            attractionVector.y *= gforce/Mass;
            //System.Diagnostics.Debug.WriteLine(attractionVector.stringify());
            //Debug.WriteLine("force: {0}", gforce);
            MovementVector.addVector(attractionVector);
        }
    }
}
