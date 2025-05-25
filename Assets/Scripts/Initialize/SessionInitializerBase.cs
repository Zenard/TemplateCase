using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DB.Repository.Abstraction;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Initialize
{
    public class SessionInitializerBase : IStartable, IDisposable
    {
        protected  IPlayerRepositoryService PlayerRepositoryService;
        CancellationTokenSource _cancellationTokenSource;
        private ProjectLifetimeScope _projectLifetimeScope;
        public SessionInitializerBase(IPlayerRepositoryService playerRepositoryService,ProjectLifetimeScope projectLifetimeScope)
        {
            PlayerRepositoryService = playerRepositoryService;
            _projectLifetimeScope = projectLifetimeScope;
            _cancellationTokenSource = new CancellationTokenSource();
        }
        public async UniTaskVoid Initialize()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
            
            string randomID = Guid.NewGuid().ToString();
            await PlayerRepositoryService.Initialize(randomID);
            
            using (LifetimeScope.EnqueueParent(_projectLifetimeScope)) //  
            {
                await SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
                var mainScene = SceneManager.GetSceneByName("MainScene");
                SceneManager.SetActiveScene(mainScene);
                Application.targetFrameRate = 60;
            }
            
            SceneManager.LoadScene(2);

        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
            _cancellationTokenSource = null;
        }

        public void Start()
        {
            Initialize().Forget();
        }
    }
}
