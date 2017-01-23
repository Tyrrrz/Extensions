namespace Tyrrrz.Extensions.Types
{
    /// <summary>
    /// Pair of two objects of same type
    /// </summary>
    public class Pair<T>
    {
        /// <summary>
        /// Left-side object
        /// </summary>
        public T Left { get; }

        /// <summary>
        /// Right-side object
        /// </summary>
        public T Right { get; }

        /// <summary>
        /// Create a pair of two objects
        /// </summary>
        public Pair(T left, T right)
        {
            Left = left;
            Right = right;
        }
    }
}