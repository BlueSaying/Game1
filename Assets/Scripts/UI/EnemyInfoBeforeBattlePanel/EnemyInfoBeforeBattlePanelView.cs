using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyInfoBeforeBattlePanelView : UIView
{
    public TMP_Text enemyNameText;
    public TMP_Text enemyDetailText;

    public Button startBattleButton;
    public Button quitBattleButton;

    public void UpdateText(string enemyName, string enemyDetail)
    {
        enemyNameText.text = enemyName;
        enemyDetailText.text = enemyDetail;
    }

    public void AddListenerToStartBattleButton(UnityAction action)
    {
        startBattleButton.onClick.AddListener(action);
    }

    public void AddListenerToQuitBattleButton(UnityAction action)
    {
        quitBattleButton.onClick.AddListener(action);
    }

    protected override void DOClosePanel()
    {
        base.DOClosePanel();

        DestroyPanel();
    }
}