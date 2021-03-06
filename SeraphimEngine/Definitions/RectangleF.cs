﻿using Microsoft.Xna.Framework;

namespace SeraphimEngine.Definitions
{
    /// <summary>
    /// Struct RectangleF
    /// </summary>
    public struct RectangleF
    {
        #region Variables

        /// <summary>
        /// The x coordinate
        /// </summary>
        public readonly float X;

        /// <summary>
        /// The y coordinate
        /// </summary>
        public readonly float Y;

        /// <summary>
        /// The width
        /// </summary>
        public readonly float Width;

        /// <summary>
        /// The height
        /// </summary>
        public readonly float Height;

        #endregion

        #region Properties

        public float Top => Y;
        public float Bottom => Y + Height;
        public float Left => X;
        public float Right => X + Width;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RectangleF(float x, float y, float width, float height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF" /> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="dimensions">The dimensions.</param>
        public RectangleF(Vector2 position, Vector2 dimensions)
        {
            X = position.X;
            Y = position.Y;
            Width = dimensions.X;
            Height = dimensions.Y;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF" /> struct.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public RectangleF(Vector2 position, float width, float height)
        {
            X = position.X;
            Y = position.Y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleF" /> struct.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="dimensions">The dimensions.</param>
        public RectangleF(float x, float y, Vector2 dimensions)
        {
            X = x;
            Y = y;
            Width = dimensions.X;
            Height = dimensions.Y;
        }
        #endregion

        #region Methods 

        /// <summary>
        /// Intersects the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.Boolean.</returns>
        public bool Intersects(RectangleF source)
        {
            return (source.Bottom >= Top && source.Top <= Bottom) && (source.Right >= Left && source.Left <= Right);
        }

        /// <summary>
        /// Intersects the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.Boolean.</returns>
        public bool Intersects(Rectangle source)
        {
            return (source.Bottom >= Top && source.Top <= Bottom) && (source.Right >= Left && source.Left <= Right);
        }

        #endregion
    }
}
