using System;
using UnityEngine;
using UnityEngine.UI;

namespace MainScene.Management
{
    public class MainSceneManager : MonoBehaviour
    {
        
        [SerializeField] CanvasGroup mainSceneCanvasGroup;
        [SerializeField] GameObject coreGameplayLevelParent;
        [SerializeField] Button playButton;
        [SerializeField] Button returnBackToMainSceneButton;

        
        private void Awake()
        {
            playButton.onClick.AddListener(OnPlayButtonClicked);
            returnBackToMainSceneButton.onClick.AddListener(OnReturnBackToMainSceneButtonClicked);
            
        }

        private void OnReturnBackToMainSceneButtonClicked()
        {
            ToggleCoreGameplayParent(false);
            mainSceneCanvasGroup.alpha = 1;
        }

        private void OnPlayButtonClicked()
        {
            mainSceneCanvasGroup.alpha = 0;
            ToggleCoreGameplayParent(true);
        }

        public void ToggleCoreGameplayParent(bool b)
        {
            coreGameplayLevelParent?.SetActive(b);
        }
    }
}   
