using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public enum DialogueName
{
    Dialogue1
}

public class DialogView : UIView
{
    public Image speakerImage;

    public TMP_Text speakerText;
    public TMP_Text contentText;

    public Button nextButton;

    protected override void DOOpenPanel()
    {
        base.DOOpenPanel();
    }

    protected override void DOClosePanel()
    {
        base.DOClosePanel();

        DestroyPanel();
    }

    public void NextDialog(string speaker, string content, Sprite image)
    {
        speakerText.text = speaker;
        contentText.text = content;
        speakerImage.sprite = image;
    }

    public void AddListenerToNextButton(UnityAction action)
    {
        nextButton.onClick.AddListener(action);
    }
}