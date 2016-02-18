using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SeraphimEngine.Definitions;
using SeraphimEngine.Gui;

namespace SeraphimEngine.Tests.Gui
{
    [TestClass]
    public class MessageHelperTests
    {
        [TestMethod]
        public void MatrixOfRectanglesReturnsCorrectlyWithZeroBasedStart()
        {
            Vector2 textureDimensions = new Vector2(20, 20);
            Vector3 textDimensions = new Vector3(100, 35, 1);

            Matrix3<RectangleF> expected = new Matrix3<RectangleF>
            {
                [0, 0] = new RectangleF(0, 0, 20, 20),
                [0, 1] = new RectangleF(20, 0, 100, 20),
                [0, 2] = new RectangleF(120, 0, 20, 20),
                [1, 0] = new RectangleF(0, 20, 20, 35),
                [1, 1] = new RectangleF(20, 20, 100, 35),
                [1, 2] = new RectangleF(120, 20, 20, 35),
                [2, 0] = new RectangleF(0, 55, 20, 20),
                [2, 1] = new RectangleF(20, 55, 100, 20),
                [2, 2] = new RectangleF(120, 55, 20, 20),
            };
            Matrix3<RectangleF> actual = MessageHelper.CreateContainerDestinationMatrix(Vector2.Zero, textureDimensions, textDimensions);

            Assert.AreEqual(expected[0, 0], actual[0, 0]);
            Assert.AreEqual(expected[0, 1], actual[0, 1]);
            Assert.AreEqual(expected[0, 2], actual[0, 2]);

            Assert.AreEqual(expected[1, 0], actual[1, 0]);
            Assert.AreEqual(expected[1, 1], actual[1, 1]);
            Assert.AreEqual(expected[1, 2], actual[1, 2]);

            Assert.AreEqual(expected[2, 0], actual[2, 0]);
            Assert.AreEqual(expected[2, 1], actual[2, 1]);
            Assert.AreEqual(expected[2, 2], actual[2, 2]);
        }

        [TestMethod]
        public void MatrixOfRectanglesReturnsCorrectlyWithNonZeroBasedStart()
        {
            Vector2 textureDimensions = new Vector2(20, 20);
            Vector3 textDimensions = new Vector3(100, 35, 1);

            Matrix3<RectangleF> expected = new Matrix3<RectangleF>
            {
                [0, 0] = new RectangleF(1, 1, 20, 20),
                [0, 1] = new RectangleF(21, 1, 100, 20),
                [0, 2] = new RectangleF(121, 1, 20, 20),
                [1, 0] = new RectangleF(1, 21, 20, 35),
                [1, 1] = new RectangleF(21, 21, 100, 35),
                [1, 2] = new RectangleF(121, 21, 20, 35),
                [2, 0] = new RectangleF(1, 56, 20, 20),
                [2, 1] = new RectangleF(21, 56, 100, 20),
                [2, 2] = new RectangleF(121, 56, 20, 20),
            };
            Matrix3<RectangleF> actual = MessageHelper.CreateContainerDestinationMatrix(Vector2.One, textureDimensions, textDimensions);

            Assert.AreEqual(expected[0, 0], actual[0, 0]);
            Assert.AreEqual(expected[0, 1], actual[0, 1]);
            Assert.AreEqual(expected[0, 2], actual[0, 2]);

            Assert.AreEqual(expected[1, 0], actual[1, 0]);
            Assert.AreEqual(expected[1, 1], actual[1, 1]);
            Assert.AreEqual(expected[1, 2], actual[1, 2]);

            Assert.AreEqual(expected[2, 0], actual[2, 0]);
            Assert.AreEqual(expected[2, 1], actual[2, 1]);
            Assert.AreEqual(expected[2, 2], actual[2, 2]);
        }
    }
}
