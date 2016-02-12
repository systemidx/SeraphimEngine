using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.ViewportAdapters;
using Moq;
using SeraphimEngine.Camera;

namespace SeraphimEngine.Tests.Camera
{
    [TestClass]
    public class Camera2DTests
    {
        [TestMethod]
        public void Camera2DSupportsResolutionFromViewportAdapter()
        {
            Mock<ViewportAdapter> adapter = new Mock<ViewportAdapter>();
            adapter.Setup(x => x.ViewportWidth).Returns(800);
            adapter.Setup(x => x.ViewportHeight).Returns(600);
            adapter.Setup(x => x.VirtualWidth).Returns(1920);
            adapter.Setup(x => x.VirtualHeight).Returns(1080);

            Camera2D camera = new Camera2D(adapter.Object);

            Assert.AreEqual(1920, camera.Viewport.Width);
            Assert.AreEqual(1080, camera.Viewport.Height);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CannotPassNullViewportAdapterToCamera()
        {
            Camera2D camera = new Camera2D(null);
        }
    }
}
