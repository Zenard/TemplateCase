using System;
using UnityEngine;

namespace Utility.Pooling.Abstraction
{
    public interface IPool<T> : IDisposable
    {
        T Get();
        void Return(T item);
    }
    public interface IPoolable
    {
        void OnSpawned();
        void OnDespawned();
    }

    public interface IPoolable<T>
    {
        void OnSpawned(T arg);
        void OnDespawned();
    }
}
