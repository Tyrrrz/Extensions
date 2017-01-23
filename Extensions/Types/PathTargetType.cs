namespace Tyrrrz.Extensions.Types
{
    /// <summary>
    /// Type of the target of a path
    /// </summary>
    public enum PathTargetType
    {
        /// <summary>
        /// Could not determine
        /// </summary>
        Unknown,

        /// <summary>
        /// Target is a file
        /// </summary>
        File,

        /// <summary>
        /// Target is a directory
        /// </summary>
        Directory
    }
}