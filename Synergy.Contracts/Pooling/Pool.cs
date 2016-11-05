using System;
using System.Collections.Generic;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace Synergy.Pooling
{
    /// <summary>
    /// Object pool for reusing objects to prevent memory fragmentation.
    /// </summary>
    /// <typeparam name="TPooled">Type of the object to be pooled</typeparam>
#if INTERNAL_POOL
    internal
#else
    public
#endif
    class Pool<TPooled>
    {
        [NotNull]
        internal readonly Func<TPooled> Constructor;

        [CanBeNull]
        internal readonly Action<TPooled> Destructor;

        [NotNull] 
        private readonly Stack<Pooled<TPooled>> items;

        [NotNull] 
        private readonly object syncRoot = new object();

        public Pool([NotNull] Func<TPooled> constructor, int initialSize = 1, [CanBeNull] Action<TPooled> destructor = null)
        {
            this.Constructor = constructor;
            this.Destructor = destructor;
            this.items = new Stack<Pooled<TPooled>>(initialSize);
            for (var i = 0; i < initialSize; i++)
            {
                var pooled = new Pooled<TPooled>(this);
                this.items.Push(pooled);
            }
        }

        /// <summary>
        /// Gets the object from the pool.
        /// </summary>
        public Pooled<TPooled> Get()
        {
            lock (this.syncRoot)
            {
                if (this.items.Count == 0)
                    return new Pooled<TPooled>(this);

                return this.items.Pop();
            }
        }

        /// <summary>
        /// Returns the object to the pool.
        /// </summary>
        public void Free(Pooled<TPooled> pooled)
        {
            lock (this.syncRoot)
            {
                this.items.Push(pooled);
            }
        }
    }

#if INTERNAL_POOL
    internal
#else
    public
#endif
    class Pooled<TPooled> : IDisposable
    {
        public TPooled Value { get; }
        private readonly Pool<TPooled> pool;

        public Pooled([NotNull] Pool<TPooled> pool)
        {
            this.Value = pool.Constructor();
            this.pool = pool;
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.pool.Destructor?.Invoke(this.Value);
            this.pool.Free(this);
        }
    }
}