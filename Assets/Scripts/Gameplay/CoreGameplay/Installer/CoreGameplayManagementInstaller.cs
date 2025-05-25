using Gameplay.CoreGameplay.Management;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay.CoreGameplay.Installer
{
    public class CoreGameplayManagementInstaller : MonoBehaviour,IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<GameManager>(Lifetime.Singleton).AsImplementedInterfaces();
            
        }
    }
}
