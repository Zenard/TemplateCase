using System.Collections.Generic;
using UnityEngine;

namespace Utility.Pooling.Abstraction
{
    public abstract class PoolBase<T> : IPool<T>
    {
        Stack<T> _pool;
        public int InitialPoolSize { get; set; }
        public int MaxPoolSize { get; set; }

        public PoolBase(int initialPoolSize = 0, int maxPoolSize = int.MaxValue)
        {
            InitialPoolSize = initialPoolSize;
            MaxPoolSize = maxPoolSize;
            _pool = new Stack<T>(InitialPoolSize);
        }

        public T Get()
        {
            if (_pool.Count == 0)
            {
                if (InitialPoolSize < MaxPoolSize)
                {
                    var item = Create();
                    _pool.Push(item);
                    return item;
                }
                else
                {
                    throw new System.Exception("Pool is empty");
                }
            }
            else
            {
                var item = _pool.Pop();
                if (item is IPoolable poolableItem)
                    poolableItem.OnSpawned();
                return item;
            }
        }

        protected abstract T Create();

        public abstract void Dispose();


        public void Return(T item)
        {
            if (item is IPoolable poolableItem)
                poolableItem.OnDespawned();
            _pool.Push(item);
        }
    }
}