using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SeraphimEngine.Helpers.Asset;

namespace SeraphimEngine.Tests.Helpers.Asset
{
    [TestClass]
    public class ContainerHelperTests
    {

        [TestMethod]
        public void TestScopeRectangleRetrieval()
        {
            Vector2 dimensions = new Vector2(18, 18);
            IList<Rectangle> rects = new List<Rectangle>();

            for (int i = 0; i < 9; ++i)
                rects.Add(ContainerHelper.GetSpritesheetScopeRect(i, dimensions));

            Assert.AreEqual(new Rectangle(0, 0, 6, 6), rects[0]);
            Assert.AreEqual(new Rectangle(6, 0, 6, 6), rects[1]);
            Assert.AreEqual(new Rectangle(12, 0, 6, 6), rects[2]);

            Assert.AreEqual(new Rectangle(0, 6, 6, 6), rects[3]);
            Assert.AreEqual(new Rectangle(6, 6, 6, 6), rects[4]);
            Assert.AreEqual(new Rectangle(12, 6, 6, 6), rects[5]);

            Assert.AreEqual(new Rectangle(0, 12, 6, 6), rects[6]);
            Assert.AreEqual(new Rectangle(6, 12, 6, 6), rects[7]);
            Assert.AreEqual(new Rectangle(12, 12, 6, 6), rects[8]);
        }

        [TestMethod]
        public void TestContainerRectangleRetrieval()
        {
            //We are starting at point (0, 0)
            Vector2 startPoint = new Vector2(0, 0);

            //We have text that takes up 100x100 pixels
            Vector2 variableDimensions = new Vector2(100, 100);

            //Each container slice is 6x6 pixels.
            Vector2 containerDimensions = new Vector2(18, 18);

            IList<Rectangle> rects = new List<Rectangle>();

            for (int i = 0; i < 9; ++i)
                rects.Add(ContainerHelper.GetContainerRect(i, startPoint, containerDimensions, variableDimensions));

            Assert.AreEqual(new Rectangle(0, 0, 6, 6), rects[0]);
            Assert.AreEqual(new Rectangle(6, 0, 100, 6), rects[1]);
            Assert.AreEqual(new Rectangle(106, 0, 6, 6), rects[2]);

            Assert.AreEqual(new Rectangle(0, 6, 6, 100), rects[3]);
            Assert.AreEqual(new Rectangle(6, 6, 100, 100), rects[4]);
            Assert.AreEqual(new Rectangle(106, 6, 6, 100), rects[5]);

            Assert.AreEqual(new Rectangle(0, 106, 6, 6), rects[6]);
            Assert.AreEqual(new Rectangle(6, 106, 100, 6), rects[7]);
            Assert.AreEqual(new Rectangle(106, 106, 6, 6), rects[8]);
        }
    }
}
