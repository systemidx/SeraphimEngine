using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Helpers.Asset;
using SeraphimEngine.Managers.Asset;
using SeraphimEngine.Managers.Input;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.TestGame.Scenes
{
    public class Splash : Scene.Scene
    {
        private Song _splashBg;

        private readonly TextureFader _poweredByFader = new TextureFader();
        private bool _poweredByScreenFadedOut = false;
        private bool _poweredByScreenPlaying = false;
        private Texture2D _poweredByTexture;

        private readonly TextureFader _studioFader = new TextureFader();
        private bool _studioScreenFadedOut = false;
        private bool _studioScreenPlaying = false;
        private Texture2D _studioTexture;

        public Splash(GraphicsDevice graphics, ViewportAdapter viewport) : base(graphics, viewport) {}

        public override void Load()
        {
            _poweredByTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/splash_engine");
            _studioTexture = AssetManager.Instance.GetAsset<Texture2D>("textures/splash_waffletech");
            _splashBg = AssetManager.Instance.GetAsset<Song>("music/splash");
            _studioScreenPlaying = true;
        }

        public override void Unload()
        {
        }

        public override void Update(GameTime gameTime)
        {
            if (InputManager.Instance.IsKeyDown(Keys.F12))
            {
                Unload();
                SceneManager.Instance.SwitchScene(typeof (SecondCustomScene));
            }

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (MediaPlayer.PlayPosition.TotalSeconds == 0.0)
                MediaPlayer.Play(_splashBg);

            if (_studioScreenPlaying)
            {
                _studioFader.Update(gameTime);

                if (_studioFader.DoneFading) {
                    if (!_studioScreenFadedOut) { 
                        _studioFader.ChangeDirection();
                        _studioScreenFadedOut = true;
                        return;
                    }

                    _studioScreenPlaying = false;
                    _poweredByScreenPlaying = true;
                }
            }

            if (_poweredByScreenPlaying)
            {
                _poweredByFader.Update(gameTime);
                if (!_poweredByFader.DoneFading)
                    return;

                if (!_poweredByScreenFadedOut)
                {
                    _poweredByFader.ChangeDirection();
                    _poweredByScreenFadedOut = true;
                    return;
                }

                SceneManager.Instance.SwitchScene(typeof(SecondCustomScene));
            }
        }

        public override void Draw(GameTime gameTime)
        {
            Graphics.Clear(Color.Black);

            SpriteBatch.Begin(transformMatrix: SceneCamera.GetViewMatrix());

            if (_studioScreenPlaying)
                SpriteBatch.Draw(_studioTexture, Vector2.Zero, Color.White* _studioFader.FadeAlpha);

            if (_poweredByScreenPlaying)
                SpriteBatch.Draw(_poweredByTexture, Vector2.Zero, Color.White * _poweredByFader.FadeAlpha);

            SpriteBatch.End();
        }
    }
}