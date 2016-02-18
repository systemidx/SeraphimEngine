using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Definitions;
using SeraphimEngine.Helpers.Asset;

namespace SeraphimEngine.Tests.Helpers.Asset
{
    [TestClass]
    public class Texture2DHelperTests
    {
        [TestMethod]
        public void TextureBreaksDownIntoCorrectMatrixWithEvenlyDivisibleDimensions()
        {
            Texture2D texture = new Texture2D(new GraphicsDevice(GraphicsAdapter.DefaultAdapter, new GraphicsProfile(), new PresentationParameters()), 60, 60);

            Matrix3<RectangleF> expected = new Matrix3<RectangleF>
            {
                [0, 0] = new RectangleF(0, 0, 20, 20),
                [0, 1] = new RectangleF(20, 0, 20, 20),
                [0, 2] = new RectangleF(40, 0, 20, 20),
                [1, 0] = new RectangleF(0, 20, 20, 20),
                [1, 1] = new RectangleF(20, 20, 20, 20),
                [1, 2] = new RectangleF(40, 20, 20, 20),
                [2, 0] = new RectangleF(0, 40, 20, 20),
                [2, 1] = new RectangleF(20, 40, 20, 20),
                [2, 2] = new RectangleF(40, 40, 20, 20)
            };
            Matrix3<RectangleF> actual = texture.GetMatrix();

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
        public void TextureBreaksDownIntoCorrectMatrixWithNotEvenlyDivisibleDimensions()
        {
            const int WIDTH = 55;
            const int HEIGHT = 63;

            float x = (float)WIDTH / 3;
            float y = (float)HEIGHT / 3;

            Texture2D texture = new Texture2D(new GraphicsDevice(GraphicsAdapter.DefaultAdapter, new GraphicsProfile(), new PresentationParameters()), WIDTH, HEIGHT);

            Matrix3<RectangleF> expected = new Matrix3<RectangleF>
            {
                [0, 0] = new RectangleF(0, 0, x, y),
                [0, 1] = new RectangleF(x, 0, x, y),
                [0, 2] = new RectangleF(x * 2, 0, x, y),
                [1, 0] = new RectangleF(0, y, x, y),
                [1, 1] = new RectangleF(x, y, x, y),
                [1, 2] = new RectangleF(x * 2, y, x, y),
                [2, 0] = new RectangleF(0, y * 2, x, y),
                [2, 1] = new RectangleF(x, y * 2, x, y),
                [2, 2] = new RectangleF(x * 2, y * 2, x, y)
            };
            Matrix3<RectangleF> actual = texture.GetMatrix();

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
