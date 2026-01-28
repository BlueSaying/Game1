using UnityEngine;

public class DialogController : UIController
{
    private DialogView view;
    private DialogModel model;

    protected override void Start()
    {
        base.Start();

        view = GetComponent<DialogView>();
        model = new DialogModel(DialogueName.Dialogue1);

        view.nextButton.onClick.AddListener(OnNextButtonClicked);
        model.OnNextDialog += view.NextDialog;
        model.OnEndDialog += EndDialog;

        // 绑定后还要调用一下，将初始数据显示出来
        model.UpdateDialog();
    }

    #region 事件集
    private void OnNextButtonClicked()
    {
        // Controller 对 Model更新
        model.NextDialog();
    }

    private void EndDialog()
    {
        UIManager.Instance.ClosePanel(this.name);
    }
    #endregion
}