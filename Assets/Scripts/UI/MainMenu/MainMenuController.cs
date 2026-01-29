public class MainMenuController : UIController
{
    private MainMenuView view;

    protected override void Start()
    {
        base.Start();

        view = GetComponent<MainMenuView>();
        view.newGameButton.onClick.AddListener(OnNewGameButtonClicked);
        view.loadButton.onClick.AddListener(OnLoadButtonClicked);
        view.continueButton.onClick.AddListener(OnContinueButtonClicked);
        view.settingsButton.onClick.AddListener(OnSettingsButtonClicked);
        view.quitButton.onClick.AddListener(OnQuitButtonClicked);
    }

    #region 事件集
    private void OnNewGameButtonClicked()
    {
        SceneLoader.Instance.OnSceneStartChange += () => { UIManager.Instance.ClosePanel(this.name); };
        SceneLoader.Instance.LoadScene(SceneName.Scene1);
    }

    private void OnLoadButtonClicked()
    {

    }

    private void OnContinueButtonClicked()
    {

    }

    private void OnSettingsButtonClicked()
    {

    }

    private void OnQuitButtonClicked()
    {
        //UIManager.Instance.ClosePanel(this.name);
    }
    #endregion
}