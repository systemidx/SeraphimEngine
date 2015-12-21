using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using Moq;
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
        public void UpdateWhileDoneFadingDoesNotChangeAlphaWithFadeIn()
        {
            TextureFader fader = new TextureFader();
            DateTime startTime = DateTime.Now;
            
            float previousAlpha = 0.0f;
            while (!fader.DoneFading)
            {
                GameTime gameTime = new GameTime(TimeSpan.MaxValue, DateTime.Now - startTime);
                
                fader.Update(gameTime);
                previousAlpha = fader.FadeAlpha;
            }

            Assert.AreEqual(previousAlpha, fader.FadeAlpha);
            Assert.AreEqual(previousAlpha, 1.0f);
        }

        [TestMethod]
        public void UpdateWhileDoneFadingDoesNotChangeAlphaWithFadeOut()
        {
            TextureFader fader = new TextureFader(fadeDirection: TextureFadeDirection.Out);
            DateTime startTime = DateTime.Now;

            float previousAlpha = 0.0f;
            while (!fader.DoneFading)
            {
                GameTime gameTime = new GameTime(TimeSpan.MaxValue, DateTime.Now - startTime);

                fader.Update(gameTime);
                previousAlpha = fader.FadeAlpha;
            }

            Assert.AreEqual(previousAlpha, fader.FadeAlpha);
            Assert.AreEqual(previousAlpha, 0.0f);
        }
    }
}
