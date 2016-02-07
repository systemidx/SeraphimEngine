using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SeraphimEngine.Map
{
    /// <summary>
    /// Class SeraphimTile.
    /// </summary>
    public class SeraphimTile : ISeraphimTile
    {
        #region Properties

        /// <summary>
        /// Gets the animation interval.
        /// </summary>
        /// <value>The animation interval.</value>
        public int AnimationInterval { get; set; }

        /// <summary>
        /// Gets the index of the animation frame.
        /// </summary>
        /// <value>The index of the animation frame.</value>
        public int AnimationFrameIndex { get; private set; }

        /// <summary>
        /// Gets the size of the tile.
        /// </summary>
        /// <value>The size of the tile.</value>
        public Point TileSize { get; set; }

        /// <summary>
        /// Gets the collision position.
        /// </summary>
        /// <value>The collision position.</value>
        public Vector2 CollisionOrigin { get; set; }

        /// <summary>
        /// Gets the collision dimensions.
        /// </summary>
        /// <value>The collision dimensions.</value>
        public Vector2 CollisionDimensions { get; set; }

        /// <summary>
        /// Gets the frames.
        /// </summary>
        /// <value>The frames.</value>
        public List<ISeraphimTileFrame> Frames { get; set; }

        #endregion

        #region Variables

        /// <summary>
        /// The time accumulation in milliseconds
        /// </summary>
        private int _timeAccumulationInMs = 0;

        /// <summary>
        /// The collision rectangle
        /// </summary>
        private Rectangle? _collisionRectangle;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="SeraphimTile"/> class.
        /// </summary>
        /// <param name="animationInterval">The animation interval.</param>
        /// <param name="frames">The frames.</param>
        public SeraphimTile(int animationInterval, List<ISeraphimTileFrame> frames)
        {
            AnimationInterval = animationInterval;
            Frames = frames;
        }

        #endregion

        #region Life Cycle Events

        /// <summary>
        /// Draws the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Draw(GameTime gameTime)
        {
            if (Frames.Count == 0)
                return;

            Frames[AnimationFrameIndex].Draw(gameTime);
        }

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            if (_timeAccumulationInMs < AnimationInterval)
            {
                _timeAccumulationInMs += gameTime.ElapsedGameTime.Milliseconds;
                return;
            }

            ++AnimationFrameIndex;
            if (AnimationFrameIndex > Frames.Count - 1)
                AnimationFrameIndex = 0;
            _timeAccumulationInMs = 0;
        }

        #endregion

        #region Public Variables

        /// <summary>
        /// Determines whether the specified source is colliding.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns><c>true</c> if the specified source is colliding; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public bool IsColliding(Rectangle source)
        {
            if (_collisionRectangle.HasValue)
                return _collisionRectangle.Value.Intersects(source);

            _collisionRectangle = new Rectangle(
                (int) CollisionOrigin.X, 
                (int) CollisionOrigin.Y,
                MathHelper.Clamp((int)(CollisionDimensions.X), 0, TileSize.X),
                MathHelper.Clamp((int)(CollisionDimensions.Y), 0, TileSize.Y));


            return IsColliding(source);
        }

        #endregion
    }
}
