using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuView : UIView
{
    public Button newGameButton;
    public Button loadButton;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;

    

    protected override void DOClosePanel()
    {
        base.DOClosePanel();

        DestroyPanel();
    }
}