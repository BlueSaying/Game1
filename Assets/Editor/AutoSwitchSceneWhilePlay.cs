using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/// <summary>
/// 点击播放按钮，场景自动切换为Bootstrap
/// 当播放结束后，场景自动切换为原本的场景
/// </summary>
[InitializeOnLoad]
public class AutoSwitchToBootstrap
{
    static AutoSwitchToBootstrap()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {
            // 替换为你 BootstrapScene 的路径（Assets/... 下的相对路径）
            string bootstrapScenePath = "Assets/Scenes/Bootstrap.unity";

            if (EditorBuildSettings.scenes.Length == 0)
            {
                Debug.LogError("EditorBuildSettings 中没有设置任何场景！");
                return;
            }

            // 检查当前打开的场景是否已经是 BootstrapScene
            var currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene();
            if (currentScene.path != bootstrapScenePath)
            {
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                {
                    EditorSceneManager.OpenScene(bootstrapScenePath);
                }
                else
                {
                    // 用户取消保存，中断播放
                    EditorApplication.isPlaying = false;
                }
            }
        }
    }
}