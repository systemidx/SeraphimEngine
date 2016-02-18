namespace SeraphimEngine.Content
{
    /// <summary>
    /// Interface IContentConvertible
    /// </summary>
    /// <typeparam name="TContentModel">The type of the t content model.</typeparam>
    /// <typeparam name="TSeraphimModel">The type of the t seraphim model.</typeparam>
    public interface IContentConvertible<in TContentModel, out TSeraphimModel>
    {
        /// <summary>
        /// Converts the specified content model.
        /// </summary>
        /// <param name="contentModel">The content model.</param>
        /// <returns>TSeraphimModel.</returns>
        TSeraphimModel Convert(TContentModel contentModel);
    }
}