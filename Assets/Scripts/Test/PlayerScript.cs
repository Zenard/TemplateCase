using System;
using Cysharp.Threading.Tasks;
using DB.DBModels;
using DB.Repository;
using DB.Repository.Abstraction;
using UnityEngine;

namespace Test
{
    public class PlayerScript : MonoBehaviour
    {
        private IPlayerRepositoryService playerRepositoryService;
        private string playerId = "2e2fwfasdhf2wqf23";
        private int scoreCurrent = 0;
        private void Awake()
        {
            playerRepositoryService = new PlayerRepositoryService();
            Initalize().Forget();
        }

        private void Update()
        {
            //Concurrency test
            if (Input.GetKey(KeyCode.A))
            {
                playerRepositoryService.SetScore(scoreCurrent);
                scoreCurrent++;
                Debug.Log($"Score: {scoreCurrent}");

            }
            
        }

        private async UniTask Initalize()
        {
            await playerRepositoryService.Initialize(playerId);
        }
    }
}
