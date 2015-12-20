using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using SeraphimEngine.Helpers.Asset;

namespace SeraphimEngine.Tests.Helpers.Asset
{
    [TestClass]
    public class TextureFadingTests
    {
        [TestMethod]
        public void ChangeDirectionChangesFadeDirectionAndResetsValuesForFadeOut()
        {
            TextureFader fader = new TextureFader(fadeDirection: TextureFadeDirection.In);
            fader.ChangeDirection();

            Assert.IsTrue(fader.FadeDirection == TextureFadeDirection.Out);
            Assert.IsTrue(fader.FadeAlpha == 1.0f);
            Assert.IsFalse(fader.DoneFading);
        }

        [TestMethod]
        public void ChangeDirectionChangesFadeDirectionAndResetsValuesForFadeIn()
        {
            TextureFader fader = new TextureFader(fadeDirection: TextureFadeDirection.Out);
            fader.ChangeDirection();

            Assert.IsTrue(fader.FadeDirection == TextureFadeDirection.In);
            Assert.IsTrue(fader.FadeAlpha == 0.0f);
            Assert.IsFalse(fader.DoneFading);
        }

        [TestMethod]
        public void UpdateWhileDoneFadingDoesNotChangeAlpha()
        {
            TextureFader fader = new TextureFader();
            DateTime startTime = DateTime.Now;

            GameTime gameTime = new GameTime(TimeSpan.MaxValue, DateTime.Now - startTime);
            
            while ((DateTime.Now - startTime).TotalSeconds < 2)
            {
                float previousAlpha = fader.FadeAlpha;
                fader.Update(gameTime);
                if (fader.DoneFading)
                {
                    Assert.AreEqual(previousAlpha, fader.FadeAlpha);
                    return;
                }

                gameTime = new GameTime(TimeSpan.MaxValue, DateTime.Now - startTime);
            }
        }
    }
}
