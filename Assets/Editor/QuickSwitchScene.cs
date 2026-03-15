using UnityEditor;
using UnityEditor.SceneManagement;

public class QuickSwitchScene
{
    private const string BootstrapPath = "Assets/Scenes/Bootstrap.unity";
    private const string MainMenuPath = "Assets/Scenes/MainMenu.unity";
    private const string Scene1Path = "Assets/Scenes/Scene1.unity";

    // 快捷键 Alt + 1
    [MenuItem("OpenScenes/Bootstrap &1")]
    public static void OpenBootstrap()
    {
        SwitchToScene(BootstrapPath);
    }

    // 快捷键 Alt + 2
    [MenuItem("OpenScenes/MainMenu &2")]
    public static void OpenMainMenu()
    {
        SwitchToScene(MainMenuPath);
    }

    // 快捷键 Alt + 3
    [MenuItem("OpenScenes/Scene1 &3")]
    public static void OpenScene1()
    {
        SwitchToScene(Scene1Path);
    }

    private static void SwitchToScene(string scenePath)
    {
        // 弹出保存提示（如果当前场景有未保存的修改）
        if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            return;

        // 打开目标场景
        EditorSceneManager.OpenScene(scenePath);
    }
}