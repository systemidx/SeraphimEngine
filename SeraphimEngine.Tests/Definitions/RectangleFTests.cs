using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Tests.Definitions
{
    [TestClass]
    public class RectangleFTests
    {
        [TestMethod]
        public void RectangleFWithParameterlessConstructorDefaultsToCorrectValues()
        {
            RectangleF f = new RectangleF();

            Assert.AreEqual(0, f.X);
            Assert.AreEqual(0, f.Y);
            Assert.AreEqual(0, f.Width);
            Assert.AreEqual(0, f.Height);
        }

        [TestMethod]
        public void RectangleFWithParameterDefaultsToCorrectValues()
        {
            RectangleF f = new RectangleF(1.1f, 2.1f, 3.1f, 4.1f);

            Assert.AreEqual(1.1f, f.X);
            Assert.AreEqual(2.1f, f.Y);
            Assert.AreEqual(3.1f, f.Width);
            Assert.AreEqual(4.1f, f.Height); 
        }

        [TestMethod]
        public void RectangleFAndRectangleHaveSameTopExpressions()
        {
            Rectangle r = new Rectangle(0, 10, 10, 10);
            RectangleF f = new RectangleF(0, 10, 10.0f, 10.0f);

            Assert.AreEqual(r.Top, Convert.ToInt32(f.Top));
        }

        [TestMethod]
        public void RectangleFAndRectangleHaveSameBottomExpressions()
        {
            Rectangle r = new Rectangle(0, 10, 10, 10);
            RectangleF f = new RectangleF(0, 10, 10.0f, 10.0f);

            Assert.AreEqual(r.Bottom, Convert.ToInt32(f.Bottom));
        }

        [TestMethod]
        public void RectangleFAndRectangleHaveSameLeftExpressions()
        {
            Rectangle r = new Rectangle(9, 10, 10, 10);
            RectangleF f = new RectangleF(9.0f, 10, 10.0f, 10.0f);

            Assert.AreEqual(r.Left, Convert.ToInt32(f.Left));
        }

        [TestMethod]
        public void RectangleFAndRectangleHaveSameRightExpressions()
        {
            Rectangle r = new Rectangle(5, 10, 10, 10);
            RectangleF f = new RectangleF(5.0f, 10, 10.0f, 10.0f);

            Assert.AreEqual(r.Right, Convert.ToInt32(f.Right));
        }

        [TestMethod]
        public void RectangleFIntersectsRectangleWithSameOriginAndDimensions()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            Rectangle b = new Rectangle(0, 0, 2, 2);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFIntersectsRectangleWithSameOrigin()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            Rectangle b = new Rectangle(0, 0, 1, 5);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFIntersectsRectangle()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            Rectangle b = new Rectangle(1, 1, 2, 2);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFDoesNotIntersectRectangle()
        {
            RectangleF a = new RectangleF(2.1f, 2, 2, 2);
            Rectangle b = new Rectangle(0, 0, 2, 2);

            Assert.IsFalse(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFIntersectsWithSameOriginAndDimensions()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            RectangleF b = new RectangleF(0, 0, 2, 2);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFIntersectsWithSameOrigin()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            RectangleF b = new RectangleF(0, 0, 1, 5);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFIntersects()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            RectangleF b = new RectangleF(1, 1, 2, 2);

            Assert.IsTrue(a.Intersects(b));
        }

        [TestMethod]
        public void RectangleFDoesNotIntersect()
        {
            RectangleF a = new RectangleF(0, 0, 2, 2);
            RectangleF b = new RectangleF(2.1f, 2, 2, 2);

            Assert.IsFalse(a.Intersects(b));
        }
    }
}
