using UnityEngine;

public class MainMenuSceneManager:MonoBehaviour
{
    void Awake()
    {
        UIManager.Instance.OpenPanel(PanelName.MainMenu);
    }
}