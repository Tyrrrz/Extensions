namespace Tyrrrz.Extensions.Types
{
    /// <summary>
    /// Specifies how to ensure max count in a list
    /// </summary>
    public enum EnsureMaxCountMode
    {
        /// <summary>
        /// Deletes the first element in the list
        /// </summary>
        DeleteFirst,

        /// <summary>
        /// Deletes the last element in the list
        /// </summary>
        DeleteLast,

        /// <summary>
        /// Deletes a random element in the list
        /// </summary>
        DeleteRandom,

        /// <summary>
        /// Deletes all elements in the list
        /// </summary>
        DeleteAll
    }
}