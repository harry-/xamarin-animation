using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sample.SampleViews;


namespace UnitTests
{
    [TestClass]
    public class VectorTests
    {
        [TestMethod]
        public void VectorLength()
        {
            double x = 10;
            double y = 0;
            double expected = 10;

            Vector vector = new Vector(3, 4);

            double length = vector.length();

            Assert.AreEqual(expected, length, 5);
        }

        [TestMethod]
        public void UnitVector()
        {
            Vector vector = new Vector(3, 4);

            Vector unitVector = vector.unitVector();

            Assert.AreEqual(unitVector.x, 0.6);
        }
    }
}
