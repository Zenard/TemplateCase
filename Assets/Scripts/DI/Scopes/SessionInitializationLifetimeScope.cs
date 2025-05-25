using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Scopes
{
    public class SessionInitializationLifetimeScope : LifetimeScope
    {
        protected override void Awake()
        {
            autoRun = false;
            Debug.Log("START SESSION");
            base.Awake();
            BuildContainer().Forget();
            
        }

        [SerializeField] private MonoBehaviour[] installers;
        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var installer in installers)
                (installer as IInstaller)?.Install(builder);
        }
        public async UniTaskVoid BuildContainer()
        {
            await UniTask.DelayFrame(3);
            Build();
        }
    }
}
