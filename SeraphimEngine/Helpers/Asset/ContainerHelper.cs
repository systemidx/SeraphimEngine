using Microsoft.Xna.Framework;

namespace SeraphimEngine.Helpers.Asset
{
    public static class ContainerHelper
    {
        /// <summary>
        /// Gets the spritesheet scope rect.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="containerDimensions">The container dimensions.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle GetSpritesheetScopeRect(int index, Vector2 containerDimensions)
        {
            int width = (int)containerDimensions.X / 3;
            int height = (int)containerDimensions.Y / 3;

            if (index < 3)
                return new Rectangle(index * width, 0, width, height);
            if (index < 6)
                return new Rectangle(index % 3 * width, height, width, height);
            return new Rectangle(index % 3 * width, height * 2, width, height);
        }

        /// <summary>
        /// Gets the container rect.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="startPoint">The start point.</param>
        /// <param name="containerDimensions">The container dimensions.</param>
        /// <param name="variableDimensions">The variable dimensions.</param>
        /// <returns>Rectangle.</returns>
        public static Rectangle GetContainerRect(int index, Vector2 startPoint, Vector2 containerDimensions, Vector2 variableDimensions)
        {
            int sliceWidth = (int)containerDimensions.X / 3;
            int sliceHeight = (int)containerDimensions.Y / 3;

            if (index < 3)
            {
                if (index == 0)
                    return new Rectangle((int)startPoint.X, (int)startPoint.Y, sliceWidth, sliceHeight);

                if (index == 1)
                    return new Rectangle((int)startPoint.X + sliceWidth, (int) startPoint.Y, (int)variableDimensions.X, sliceHeight);

                return new Rectangle((int)startPoint.X + (int)variableDimensions.X + sliceWidth, (int) startPoint.Y, sliceWidth, sliceHeight);
            }

            if (index < 6)
            {
                if (index == 3)
                    return new Rectangle((int)startPoint.X, (int)startPoint.Y + sliceHeight, sliceWidth, (int)variableDimensions.Y);

                //Middle
                if (index == 4)
                    return new Rectangle((int)startPoint.X + sliceWidth, (int)startPoint.Y + sliceHeight, (int)variableDimensions.X, (int)variableDimensions.Y);

                return new Rectangle((int)startPoint.X + (int)variableDimensions.X + sliceWidth, (int)startPoint.Y + sliceHeight, sliceWidth, (int)variableDimensions.Y);
            }

            if (index == 6)
                return new Rectangle((int)startPoint.X, (int)startPoint.Y + sliceHeight + (int)variableDimensions.Y, sliceWidth, sliceHeight);

            if (index == 7)
                return new Rectangle((int)startPoint.X + sliceWidth, (int)startPoint.Y + sliceHeight + (int)variableDimensions.Y, (int)variableDimensions.X, sliceHeight);

            return new Rectangle((int)startPoint.X + (int)variableDimensions.X + sliceWidth, (int)startPoint.Y + sliceHeight + (int)variableDimensions.Y, sliceWidth, sliceHeight);
        }
    }
}
