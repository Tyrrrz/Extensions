using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Tyrrrz.Extensions.Types
{
    /// <summary>
    /// <see cref="IEqualityComparer{T}"/> implementation that uses a delegate to compare objects
    /// </summary>
    public class DelegateEqualityComparer<T> : IEqualityComparer<T>
    {
        private readonly Func<T, T, bool> _comparer;
        private readonly Func<T, int> _hashGenerator;

        /// <summary>
        /// Initializes with the given comparer and hash generator
        /// </summary>
        public DelegateEqualityComparer([NotNull] Func<T, T, bool> comparer, [NotNull] Func<T, int> hashGenerator)
        {
            if (comparer == null)
                throw new ArgumentNullException(nameof(comparer));
            if (hashGenerator == null)
                throw new ArgumentNullException(nameof(hashGenerator));

            _comparer = comparer;
            _hashGenerator = hashGenerator;
        }

        /// <summary>
        /// Initializes with the given comparer and default hash generator
        /// </summary>
        public DelegateEqualityComparer([NotNull] Func<T, T, bool> comparer)
            : this(comparer, o => o.GetHashCode())
        {
        }

        /// <summary>
        /// Initializes with the default comparer and default hash generator
        /// </summary>
        public DelegateEqualityComparer()
            : this((x, y) => object.Equals(x, y), o => o.GetHashCode())
        {
        }

        /// <inheritdoc />
        public bool Equals(T x, T y)
        {
            return _comparer(x, y);
        }

        /// <inheritdoc />
        public int GetHashCode(T obj)
        {
            return _hashGenerator(obj);
        }
    }
}