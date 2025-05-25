using System.Threading;
using UnityEngine;
using Utility.Pooling.Abstraction;

namespace Utility.Pooling
{
    public class ComponentPool : PoolBase<Component>
    {
        CancellationTokenSource _lifeTimeCts;
        private readonly Component _prefab;
        private readonly Transform _poolParent;
        protected override Component Create()
        {
            return UnityEngine.
                Object.Instantiate(_prefab, _poolParent);
        }

        public override void Dispose()
        {
            _lifeTimeCts?.Cancel();
        }
    }
}
