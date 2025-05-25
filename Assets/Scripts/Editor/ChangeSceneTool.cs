using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public static class ChangeSceneTool
    {
        private const string MENU_NAME = "Tools/Change Scene/";
        [MenuItem(MENU_NAME + "Session Initialization")]
        private static void OpenInitializationScene()
        {
            OpenScene("SessionInitializer");
        }

        [MenuItem(MENU_NAME + "Initial Scene")]
        private static void OpenInitialScene()
        {
            OpenScene("InitialScene");
        }

        [MenuItem(MENU_NAME + "Main Scene")]
        private static void OpenMainScene()
        {
            OpenScene("MainScene");
        }
        private static void OpenScene(string sceneName)
        {
            if (EditorApplication.isPlaying) return;
            EditorSceneManager.OpenScene("Assets/Scenes/" + sceneName + ".unity");
        }
        
    }
}
