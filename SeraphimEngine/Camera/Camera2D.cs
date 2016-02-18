using System;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;
using SeraphimEngine.Definitions;

namespace SeraphimEngine.Camera
{
    /// <summary>
    /// Class Camera2D.
    /// </summary>
    public class Camera2D
    {
        #region Variables 

        /// <summary>
        /// The resolution
        /// </summary>
        public readonly Vector2 Resolution;

        /// <summary>
        /// The virtual resolution
        /// </summary>
        public readonly Vector2 VirtualResolution;

        /// <summary>
        /// The viewport
        /// </summary>
        public RectangleF Viewport;

        /// <summary>
        /// The viewport position
        /// </summary>
        public RectangleF ViewportPosition;

        /// <summary>
        /// The origin
        /// </summary>
        public Vector2 Origin;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the scale.
        /// </summary>
        /// <value>The scale.</value>
        public float Scale => Resolution.X / VirtualResolution.X;

        /// <summary>
        /// Gets or sets the view matrix.
        /// </summary>
        /// <value>The view matrix.</value>
        public Matrix ViewMatrix => Matrix.CreateScale(Scale);

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Camera2D" /> class.
        /// </summary>
        /// <param name="viewportAdapter">The viewport adapter.</param>
        public Camera2D(ViewportAdapter viewportAdapter)
        {
            ViewportAdapter adapter = viewportAdapter;
            if (adapter == null)
                throw new ArgumentNullException();

            Resolution = new Vector2(adapter.ViewportWidth, adapter.ViewportHeight);
            VirtualResolution = new Vector2(adapter.VirtualWidth, adapter.VirtualHeight);

            Viewport = new RectangleF(0, 0, VirtualResolution.X, VirtualResolution.Y);
            ViewportPosition = new RectangleF(0, 0, VirtualResolution.X, VirtualResolution.Y);

            Origin = Vector2.Zero;
        }

        #region public Methods

        /// <summary>
        /// Moves the camera viewport position by a two dimensional magnitude.
        /// </summary>
        /// <param name="magnitude">The magnitude.</param>
        public void MoveBy(Vector2 magnitude)
        {
            float moveByX = MathHelper.Clamp(magnitude.X, 0, float.MaxValue);
            float moveByY = MathHelper.Clamp(magnitude.Y, 0, float.MaxValue);

            Origin = new Vector2(Origin.X + moveByX, Origin.Y + moveByY);
            ViewportPosition = new RectangleF(ViewportPosition.X + moveByX, ViewportPosition.Y + moveByY, Viewport.Width, Viewport.Height);
        }

        /// <summary>
        /// Moves the camera viewport position by a location specified by two dimensional coordinates.
        /// </summary>
        /// <param name="position">The position.</param>
        public void MoveTo(Vector2 position)
        {
            float moveToX = MathHelper.Clamp(ViewportPosition.X + position.X, 0, float.MaxValue);
            float moveToY = MathHelper.Clamp(ViewportPosition.Y + position.Y, 0, float.MaxValue);
            
            Origin = new Vector2(position.X, position.Y);
            ViewportPosition = new RectangleF(moveToX, moveToY, Viewport.Width, Viewport.Height);
        }

        /// <summary>
        /// Determines whether [is in camera view] [the specified source rectangle].
        /// </summary>
        /// <param name="sourceRectangle">The source rectangle.</param>
        /// <returns>System.Boolean.</returns>
        public bool IsInCameraView(RectangleF sourceRectangle)
        {
            return ViewportPosition.Intersects(sourceRectangle);
        }

        /// <summary>
        /// Gets the center coordinates.
        /// </summary>
        /// <returns>Microsoft.Xna.Framework.Vector2.</returns>
        public Vector2 GetCenterCoordinates()
        {
            float widthSlice = VirtualResolution.X / 3;
            float heightSlice = VirtualResolution.Y / 3;

            return new Vector2(widthSlice, heightSlice);
        }

        #endregion
    }
}
