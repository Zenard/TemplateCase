using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utility
{
    public class InitialSceneLoader : MonoBehaviour
    {
        [SerializeField] private int waitFrameCount = 3;
        
        private void Awake()
        {
            StartCoroutine(LoadNextScene());
        }

        private IEnumerator LoadNextScene()
        {
            for (int i = 0; i < waitFrameCount; i++)
            {
                yield return new WaitForEndOfFrame();
            }

            SceneManager.LoadScene(1);
        }
    }
}