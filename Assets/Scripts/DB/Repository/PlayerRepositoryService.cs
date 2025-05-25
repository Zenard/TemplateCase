using System.Linq;
using Cysharp.Threading.Tasks;
using DB.DBModels;
using DB.Repository.Abstraction;
using UnityEngine;

namespace DB.Repository
{
    public class PlayerRepositoryService : IPlayerRepositoryService   
    {
        PlayerDBModel playerDBModel;
        IRepositoryService repositoryService;
        public PlayerRepositoryService()
        {
            repositoryService = new PersistentRepositoryService(Application.persistentDataPath);
        }
        public async UniTask Initialize(string playerId)
        {
            var playerDBModel = await repositoryService.GetAsync<PlayerDBModel>(x => x.user_id == playerId);
            
            
            if (playerDBModel == null)
            {
                playerDBModel = new PlayerDBModel
                {
                    user_id = playerId,
                    score = 0
                };
                await repositoryService.InsertAsync(playerDBModel);
            }
            else
            {
                this.playerDBModel = playerDBModel;
                if (this.playerDBModel == null)
                {
                    Debug.LogError("PlayerDBModel is null");
                }
            }
        }

        public UniTask AddScore(int score)
        {
            if (playerDBModel == null)
            {
                Debug.LogError("PlayerDBModel is null");
                return UniTask.CompletedTask;
            }
            playerDBModel.score += score;
            return repositoryService.UpdateAsync(playerDBModel);
        }

        public UniTask SetScore(int score)
        {
            if (playerDBModel == null)
            {
                Debug.LogError("PlayerDBModel is null");
                return UniTask.CompletedTask;
            }
            playerDBModel.score = score;
            return repositoryService.UpdateAsync(playerDBModel);
        }
    }
}
