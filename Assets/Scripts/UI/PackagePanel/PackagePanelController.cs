public class PackagePanelController : UIController
{
    public PackagePanelView view;

    public PlayerModel PlayerModel => PlayerManager.Instance.PlayerModel;

    protected override void Awake()
    {
        base.Awake();

        view.AddListenerToEquipmentButton(OnEquipmentButtonClicked);
        view.AddListenerToFaBaoButton(OnFaBaoButtonClicked);
        view.AddListenerToCombatItemButton(OnCombatItemButtonClicked);
        view.AddListenerToMiscellaneousButton(OnMiscellaneousButtonClicked);

        UpdatePlayerInfo();
        EventCenter.Instance.RegisterEvent(EventType.OnPlayerInfoChanged, UpdatePlayerInfo);
    }

    public override void ClosePanel()
    {
        base.ClosePanel();
        EventCenter.Instance.RemoveEvent(EventType.OnPlayerInfoChanged, UpdatePlayerInfo);
    }

    private void UpdatePlayerInfo()
    {
        PlayerModel model = PlayerManager.Instance.PlayerModel;

        view.UpdatePlayerInfoHP(model.curHP, model.maxHP);
        view.UpdatePlayerInfoMP(model.curMP, model.maxMP);
        view.UpdatePlayerInfoMana(model.mana);
        view.UpdatePlayerInfoStrength(model.strength);
        view.UpdatePlayerInfoSpeed(model.speed);
        view.UpdatePlayerInfoDefence(model.defence);
    }

    public void UpdateDescription(string name, string detail)
    {
        view.UpdateItemName(name);
        view.UpdateItemDetail(detail);
    }

    #region 事件集
    private void OnEquipmentButtonClicked()
    {
        view.UpdateItemScroll(PackageManager.Instance.equipment);
        PackageManager.Instance.UpdateChosenInfo(0, ItemType.Equipment);
    }
    private void OnFaBaoButtonClicked()
    {
        view.UpdateItemScroll(PackageManager.Instance.faBao);
        PackageManager.Instance.UpdateChosenInfo(0, ItemType.FaBao);
    }
    private void OnCombatItemButtonClicked()
    {
        view.UpdateItemScroll(PackageManager.Instance.combatItem);
        PackageManager.Instance.UpdateChosenInfo(0, ItemType.CombatItem);
    }
    private void OnMiscellaneousButtonClicked()
    {
        view.UpdateItemScroll(PackageManager.Instance.miscellaneous);
        PackageManager.Instance.UpdateChosenInfo(0, ItemType.Miscellaneous);
    }
    #endregion
}