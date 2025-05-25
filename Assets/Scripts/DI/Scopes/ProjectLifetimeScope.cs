using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    
    [SerializeField] private MonoBehaviour[] monoInstallers;

    protected override void Configure(IContainerBuilder builder)
    {
        foreach (var installer in monoInstallers)
            (installer as IInstaller)?.Install(builder);
    }
}
