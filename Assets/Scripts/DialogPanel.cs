using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum DialogueType
{
    Dialogue1
}

public class DialogPanel : MonoBehaviour
{
    //public TextAsset dialogueText;

    public Image speakerImage;

    public TMP_Text speakerText;
    public TMP_Text contentText;

    private List<int> indices;
    private List<string> speakers;
    private List<string> content;
    private List<int> next;

    private int currentIndex = 0;

    private Dictionary<string, Sprite> speakerSprites;

    void Awake()
    {
        indices = new List<int>();
        speakers = new List<string>();
        content = new List<string>();
        next = new List<int>();

        currentIndex = 0;

        speakerSprites = new Dictionary<string, Sprite>();

        StartDialogue(DialogueType.Dialogue1);
    }

    /// <summary>
    /// 开启对话
    /// </summary>
    /// <param name="dialogueType"></param>
    public void StartDialogue(DialogueType dialogueType)
    {
        LoadDialogue("Dialogue/" + dialogueType.ToString());

        currentIndex = 0;

        UpdateDialog();
    }

    public void NextDialogue()
    {
        int targetRow = 0;
        while (targetRow < indices.Count && indices[targetRow] != currentIndex)
        {
            targetRow++;
        }
        if (targetRow >= indices.Count) return;

        currentIndex = next[targetRow];

        UpdateDialog();
    }

    private void UpdateDialog()
    {
        if (currentIndex == -1) ClosePanel();

        int targetRow = 0;
        while (targetRow < indices.Count && indices[targetRow] != currentIndex)
        {
            targetRow++;
        }
        if (targetRow >= indices.Count) return;

        // 显示层内容
        speakerText.text = speakers[targetRow];
        contentText.text = content[targetRow];

        if (!speakerSprites.ContainsKey(speakerText.text))
        {
            Sprite sprite = Resources.Load<Sprite>("Sprites/DialogueSpeakers/" + speakerText.text);
            if (sprite != null)
            {
                speakerSprites.Add(speakerText.text, sprite);
            }
        }

        speakerImage.sprite = speakerSprites[speakerText.text];
    }

    private void LoadDialogue(string dialoguePath)
    {
        TextAsset dialogueText = Resources.Load<TextAsset>(dialoguePath);
        string[] rows = dialogueText.text.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(',');
            if (cells[0] == "") continue;

            indices.Add(int.Parse(cells[0]));
            speakers.Add(cells[1]);
            content.Add(cells[2]);
            next.Add(int.Parse(cells[3]));
        }
    }

    private void ClosePanel()
    {
        Destroy(gameObject);
    }
}