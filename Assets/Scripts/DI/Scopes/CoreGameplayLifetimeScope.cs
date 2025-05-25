using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DI.Scopes
{
    public class CoreGameplayLifetimeScope : LifetimeScope
    {
        MonoBehaviour[] monoInstallers;
        protected override void Configure(IContainerBuilder builder)
        {
            foreach (var installer in monoInstallers)
                (installer as IInstaller)?.Install(builder);
        }
    }
}
