using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.SampleViews
{
    /// <summary>
    /// two dimensional vectors
    /// </summary>
    public class Vector
    {
        public double x { get; set; }
        public double y { get; set; }

        public Vector(double newX, double newY)
        {
            x = newX;
            y = newY;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector"/> class, given the coordinates of the start (x1, y1) and end point (x2, y2) of the vector
        /// </summary>
        /// <param name="x1">x coordinate of the start point</param>
        /// <param name="y1">y coordinate of the start point</param>
        /// <param name="x2">x coordinate of the end point</param>
        /// <param name="y2">y coordinate of the end point</param>
        public Vector(double x1, double y1, double x2, double y2)
        {
            x = x2 - x1;
            y = y2 - y1;
        }

        public Vector()
        {
        }

        public double length()
        {
            return Math.Sqrt(x * x + y * y);
        }

        public Vector unitVector()
        {
            Vector UnitVector = new Vector();

            UnitVector.x = x / length();
            UnitVector.y = y / length();

            return UnitVector;
        }

        /// <summary>
        /// Adds the vector passed as an argument to the calling vector
        /// </summary>
        /// <param name="vector">The vector that is to be added to the caller</param>
        public void addVector(Vector vector)
        {
            x += vector.x;
            y += vector.y;
        }

        /// <summary>
        /// Multiplies the vector with the float passed as an argument.
        /// </summary>
        /// <param name="scalar">The scalar.</param>
        public void scalarMultiplication(float scalar)
        {
            x *= scalar;
            y *= scalar;
        }

        public string stringify()
        {
            return x + "/" + y;
        }
    }

}

