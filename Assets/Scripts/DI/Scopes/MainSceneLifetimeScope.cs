using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Scopes
{
    public class MainSceneLifetimeScope : LifetimeScope
    {
        [SerializeField] private MonoBehaviour[] monoInstallers;
        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var installer in monoInstallers)
                (installer as IInstaller)?.Install(builder);
            
        }
    }
}
