using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogModel
{
    public List<int> indices { get; private set; }
    public List<string> speakers { get; private set; }
    public List<string> contents { get; private set; }
    public List<int> next { get; private set; }

    public int currentIndex { get; private set; }

    public Dictionary<string, Sprite> speakerSprites { get; private set; }

    public event UnityAction<string, string, Sprite> OnNextDialog;
    public event UnityAction OnEndDialog;

    public DialogModel(DialogueName dialogueName)
    {
        indices = new List<int>();
        speakers = new List<string>();
        contents = new List<string>();
        next = new List<int>();

        currentIndex = 0;

        speakerSprites = new Dictionary<string, Sprite>();

        // 从Resources中加载Dialog
        LoadDialog(dialogueName);
    }

    public void NextDialog()
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

    private void LoadDialog(DialogueName dialogueName)
    {
        TextAsset dialogueText = DataLoader.LoadDialogue(DialogueName.Dialogue1);
        string[] rows = dialogueText.text.Split('\n');

        for (int i = 1; i < rows.Length; i++)
        {
            string[] cells = rows[i].Split(',');
            if (cells[0] == "") continue;

            indices.Add(int.Parse(cells[0]));
            speakers.Add(cells[1]);
            contents.Add(cells[2]);
            next.Add(int.Parse(cells[3]));
        }
    }

    public void UpdateDialog()
    {
        // 判断对话是否已经结束
        if (currentIndex < 0)
        {
            OnEndDialog?.Invoke();
            return;
        }

        string speaker;
        string content;
        Sprite image;

        int targetRow = 0;
        while (targetRow < indices.Count && indices[targetRow] != currentIndex)
        {
            targetRow++;
        }
        if (targetRow >= indices.Count) return;

        speaker = speakers[targetRow];
        content = contents[targetRow];
        if (!speakerSprites.ContainsKey(speaker))
        {
            Sprite sprite = ResourcesLoader.Instance.LoadSprite(SpriteType.DialogueSpeakers, speaker);
            if (sprite != null)
            {
                speakerSprites.Add(speaker, sprite);
            }
        }
        image = speakerSprites[speaker];

        OnNextDialog?.Invoke(speaker, content, image);
    }
}