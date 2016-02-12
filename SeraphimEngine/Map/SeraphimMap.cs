using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SeraphimEngine.Managers.Scene;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Class SeraphimMap.
    /// </summary>
    public class SeraphimMap : ISeraphimMap
    {
        #region Properties

        /// <summary>
        /// Gets the size.
        /// </summary>
        /// <value>The size.</value>
        public Vector2 Size { get; }

        /// <summary>
        /// Gets the layers.
        /// </summary>
        /// <value>The layers.</value>
        public List<ISeraphimLayer> Layers { get; }
        
        /// <summary>
        /// Moves the map by a magnitude.
        /// </summary>
        /// <param name="magnitude">The magnitude.</param>
        public void MoveBy(Vector2 magnitude)
        {
            if (SceneManager.Instance.Camera.ViewportPosition.Left + magnitude.X < 0)
                return;

            if (SceneManager.Instance.Camera.ViewportPosition.Top + magnitude.Y < 0)
                return;

            if (SceneManager.Instance.Camera.ViewportPosition.Right + magnitude.X > Size.X)
                return;

            if (SceneManager.Instance.Camera.ViewportPosition.Bottom + magnitude.Y > Size.Y)
                return;

            SceneManager.Instance.Camera.MoveBy(magnitude);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeraphimMap" /> class.
        /// </summary>
        /// <param name="layers">The layers.</param>
        /// <param name="size">The size.</param>
        public SeraphimMap(List<ISeraphimLayer> layers, Vector2 size)
        {
            Layers = layers;
            Size = size;
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(GameTime gameTime)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < Layers.Count; ++i)
                Layers[i].Update(gameTime);
        }

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Draw(GameTime gameTime)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (int i = 0; i < Layers.Count; ++i)
                Layers[i].Draw(gameTime);
        }

        #endregion
    }
}