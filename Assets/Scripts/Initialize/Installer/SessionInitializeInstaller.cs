using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Initialize.Installer
{
    public class SessionInitializeInstaller : MonoBehaviour,IInstaller
    {

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<SessionInitializerBase>(Lifetime.Singleton)
                .AsSelf();
        }
    }
}
