using UnityEditor;
using UnityEditor.SceneManagement;

public class QuickSwitchScene
{
    // 快捷键 Alt + 1
    [MenuItem("OpenScenes/Bootstrap &1")]
    public static void OpenBootstrap()
    {
        SwitchToScene("Assets/Scenes/Bootstrap.unity");
    }

    // 快捷键 Alt + 2
    [MenuItem("OpenScenes/MainMenu &2")]
    public static void OpenMainMenu()
    {
        SwitchToScene("Assets/Scenes/MainMenu.unity");
    }

    // 快捷键 Alt + 3
    [MenuItem("OpenScenes/Scene1 &3")]
    public static void OpenScene1()
    {
        SwitchToScene("Assets/Scenes/Scene1.unity");
    }

    // 快捷键 Alt + 4
    [MenuItem("OpenScenes/Forest &4")]
    public static void OpenForest()
    {
        SwitchToScene("Assets/Scenes/Forest.unity");
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