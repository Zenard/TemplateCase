using DB.Repository;
using DB.Repository.Abstraction;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DB.Installer
{
    public class DBServiceInstaller : MonoBehaviour, IInstaller
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<PersistentRepositoryService>(Lifetime.Scoped).AsImplementedInterfaces()
                .WithParameter(Application.persistentDataPath);
            
            builder.Register<PlayerRepositoryService>(Lifetime.Scoped).AsImplementedInterfaces();
            

        }
    }
}
