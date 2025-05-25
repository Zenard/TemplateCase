using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DB.Repository.Abstraction
{
    public interface IPlayerRepositoryService  
    {
        
        UniTask Initialize(string userId);
        UniTask AddScore(int score);
        UniTask SetScore(int score);
    }
}
