using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework.Content;
using Moq;
using SeraphimEngine.Exceptions;
using SeraphimEngine.Managers.Asset;

namespace SeraphimEngine.Tests.Managers.Asset
{
    [TestClass]
    public class AssetManagerTests
    {
        [TestMethod]
        [ExpectedException(typeof(AssetManagerInitializationException))]
        public void UninitializedManagerThrowsException()
        {
            IAssetManager newInstance = new AssetManager();
            newInstance.GetAsset<string>("");
        }

        [TestMethod]
        public void UnloadAssetsRemvoesAssetFromCache()
        {
            const string OBJECT_KEY_FIRST = "some_key";

            Mock<ContentManager> mockContent = new Mock<ContentManager>(new Mock<IServiceProvider>().Object, "");
            mockContent.Setup(x => x.Load<string>(OBJECT_KEY_FIRST)).Returns("first");

            IAssetManager manager = new AssetManager();
            Assert.IsTrue(manager.ObjectCache.Count == 0);

            manager.Initialize(mockContent.Object, null);
            manager.GetAsset<string>(OBJECT_KEY_FIRST);
            Assert.IsTrue(manager.ObjectCache.Count == 1);

            manager.UnloadAsset(OBJECT_KEY_FIRST);
            Assert.IsTrue(manager.ObjectCache.Count == 0);
        }

        [TestMethod]
        public void GetAssetAddsAssetToCache()
        {
            const string OBJECT_KEY_FIRST = "some_key";

            Mock<ContentManager> mockContent = new Mock<ContentManager>(new Mock<IServiceProvider>().Object, "");
            mockContent.Setup(x => x.Load<string>(OBJECT_KEY_FIRST)).Returns("first");

            IAssetManager manager = new AssetManager();
            Assert.IsTrue(manager.ObjectCache.Count == 0);

            manager.Initialize(mockContent.Object, null);
            manager.GetAsset<string>(OBJECT_KEY_FIRST);

            Assert.IsTrue(manager.ObjectCache.Count == 1);
        }

        [TestMethod]
        public void GettingAssetTwiceLoadsFromCacheAfterInitialRetrieval()
        {
            const string OBJECT_KEY_FIRST = "some_key";

            Mock<ContentManager> mockContent = new Mock<ContentManager>(new Mock<IServiceProvider>().Object, "");
            mockContent.Setup(x => x.Load<string>(OBJECT_KEY_FIRST)).Returns("first");

            IAssetManager manager = new AssetManager();
            Assert.IsTrue(manager.ObjectCache.Count == 0);

            manager.Initialize(mockContent.Object, null);
            manager.GetAsset<string>(OBJECT_KEY_FIRST);
            manager.GetAsset<string>(OBJECT_KEY_FIRST);
        }

        [TestMethod]
        public void GetAssetReturnsMultipleTypes()
        {
            const string OBJECT_KEY_FIRST = "some_key";
            const string OBJECT_KEY_SECOND = "some_other_key";
            
            Mock<ContentManager> mockContent = new Mock<ContentManager>(new Mock<IServiceProvider>().Object, "");
            mockContent.Setup(x => x.Load<string>(OBJECT_KEY_FIRST)).Returns("first");
            mockContent.Setup(x => x.Load<bool>(OBJECT_KEY_SECOND)).Returns(true);

            IAssetManager manager = new AssetManager();
            Assert.IsTrue(manager.ObjectCache.Count == 0);

            manager.Initialize(mockContent.Object, null);
            Assert.AreEqual("first", manager.GetAsset<string>(OBJECT_KEY_FIRST));
            Assert.AreEqual(true, manager.GetAsset<bool>(OBJECT_KEY_SECOND));
        }
    }
}
