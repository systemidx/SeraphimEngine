namespace SeraphimEngine.Content
{
    /// <summary>
    /// Interface IVariableConvertible
    /// </summary>
    /// <typeparam name="TModel">The type of the t model.</typeparam>
    public interface IVariableConvertible<TModel> where TModel : class
    {
        /// <summary>
        /// Converts the variables.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>TModel.</returns>
        TModel ConvertVariables(TModel model);
    }
}