using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.Extended.ViewportAdapters;

namespace SeraphimEngine.Camera
{
    public class Camera2D
    {
        public readonly Vector2 Resolution;
        public readonly Vector2 VirtualResolution;

        public Rectangle Viewport;
        public Vector2 Origin;

        public float Scale => Resolution.X/VirtualResolution.X;
        public Matrix ViewMatrix => Matrix.CreateScale(Scale);

        public Camera2D(ViewportAdapter viewportAdapter)
        {
            ViewportAdapter adapter = viewportAdapter;

            Resolution = new Vector2(adapter.ViewportWidth, adapter.ViewportHeight);
            VirtualResolution = new Vector2(adapter.VirtualWidth, adapter.VirtualHeight);

            Viewport = new Rectangle(0, 0, (int)VirtualResolution.X, (int)VirtualResolution.Y);
            Origin = Vector2.Zero;
        }

        public void MoveBy(Vector2 magnitude)
        {
            int moveByX = (int) MathHelper.Clamp(magnitude.X, 0, float.MaxValue);
            int moveByY = (int)MathHelper.Clamp(magnitude.Y, 0, float.MaxValue);

            Origin = new Vector2(Origin.X + moveByX, Origin.Y + moveByY);
            Viewport = new Rectangle(Viewport.X + moveByX, Viewport.Y + moveByY, Viewport.Width, Viewport.Height);
        }

        public void MoveTo(Vector2 position)
        {
            
        }

        public bool IsInCameraView(Rectangle sourceRectangle)
        {
            return Viewport.Intersects(sourceRectangle);
        }
    }
}
