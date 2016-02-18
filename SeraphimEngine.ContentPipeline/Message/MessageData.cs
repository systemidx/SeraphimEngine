namespace SeraphimEngine.ContentPipeline.Message
{
    /// <summary>
    /// Class MessageData.
    /// </summary>
    public class MessageData
    {
        #region Member Properties

        /// <summary>
        /// The x
        /// </summary>
        [VariableConvertible]
        public string X { get; set; }

        /// <summary>
        /// The y
        /// </summary>
        [VariableConvertible]
        public string Y { get; set; }

        /// <summary>
        /// The width
        /// </summary>
        [VariableConvertible]
        public string Width { get; set; }

        /// <summary>
        /// The height
        /// </summary>
        [VariableConvertible]
        public string Height { get; set; }

        /// <summary>
        /// The text
        /// </summary>
        [VariableConvertible]
        public string Text { get; set; }

        /// <summary>
        /// The font name
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// The font size
        /// </summary>
        public int FontSize { get; set; }

        #endregion
    }
}
