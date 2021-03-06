﻿using System.Collections.Generic;
using System.Diagnostics;
using System;

namespace Sample.SampleViews
{
    class Simulation
    {
        private List<Steerable> _objects = new List<Steerable>();
        internal double MinX { get; set; } = 0;
        internal double MaxX { get; set; } = 500;
        internal double MinY { get; set; } = 0;
        internal double MaxY { get; set; } = 500;
        internal int MinSize { get; set; } = 1;
        internal int MaxSize { get; set; } = 10;

        internal List<Steerable> Objects = new List<Steerable>();

        private static Random _random = new Random();

        public Simulation()
        {
        }
            
        internal void AddRandomObject()
        {
            System.Diagnostics.Debug.WriteLine("calling constructor with these values: {0}, {1}, {2}", MaxX, MaxY, MaxSize);
            int rnd = _random.Next(2);
            if (rnd >= 0)
            {
                Circle newObject = new Circle((int) MaxX, (int) MaxY, MaxSize);
                Objects.Add(newObject);
            }
            else
            {
                Square newObject = new Square((int) MaxX, (int) MaxY, MaxSize);
                Objects.Add(newObject);
            }

        }
    }
}
