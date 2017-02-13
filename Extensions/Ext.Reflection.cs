using System.Reflection;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions
{
    public static partial class Ext
    {
        /// <summary>
        /// Creates a shallow copy of an object using <see cref="object.MemberwiseClone"/>
        /// </summary>
        [Pure]
        public static T ShallowCopy<T>(this T obj)
        {
            var method = typeof (object).GetMethod(nameof(MemberwiseClone),
                BindingFlags.NonPublic | BindingFlags.Instance);
            var result = method.Invoke(obj, null);
            return (T) result;
        }
    }
}