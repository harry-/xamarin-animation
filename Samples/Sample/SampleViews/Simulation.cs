using System.Collections.Generic;
using Java.Util;

namespace Sample.SampleViews
{
    class Simulation
    {
        private List<Steerable> _objects = new List<Steerable>();
        internal double MinX { get; set; }
        internal double MaxX { get; set; }
        internal double MinY { get; set; }
        internal double MaxY { get; set; }
        internal int MinSize { get; set; }
        internal int MaxSize { get; set; } = 10;

        private Random random = new Random();

        internal List<Steerable> Objects { get => _objects; set => _objects = value; }

        internal void AddRandomObject()
        {
            Steerable newObject = new Steerable((int)MaxX, (int)MaxY, MaxSize);
            Objects.Add(newObject );
        }
    }
}
