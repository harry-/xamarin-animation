using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.SampleViews
{
    class Gravitation
    {
        private static double G = 0.01;
        //private static double G = 6.67408E-11;

        public static double Force(Steerable obj1, Steerable obj2)
        {
            double force = G*((obj1.Mass*obj2.Mass)/Math.Pow(new Vector(obj1.Location, obj2.Location).length(), 2));
            return force;
        }
    }
}
