using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Synergy.Contracts.Pooling
{
    /// <summary>
    /// Object pool for reusing objects to prevent memory fragmentation.
    /// </summary>
    /// <typeparam name="TPooled">Type of the object to be pooled</typeparam>
    internal class Pool<TPooled>
    {
        [NotNull] 
        public readonly Func<TPooled> Constructor;

        [CanBeNull] 
        public readonly Action<TPooled> Destructor;
        private readonly Stack<Pooled<TPooled>> items = new Stack<Pooled<TPooled>>();
        private readonly object syncRoot = new object();

        public Pool([NotNull] Func<TPooled> constructor, int initialSize = 1, [CanBeNull] Action<TPooled> destructor = null)
        {
            this.Constructor = constructor;
            this.Destructor = destructor;
            for (var i = 0; i < initialSize; i++)
            {
                var pooled = new Pooled<TPooled>(this);
                this.items.Push(pooled);
            }
        }

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

    internal class Pooled<TPooled> : IDisposable
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