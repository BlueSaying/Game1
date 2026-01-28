using UnityEngine.Events;

public class PlayerModel
{
    public string Name { get; private set; }
    public int HP { get; private set; }
    public int Level { get; private set; }

    private event UnityAction<PlayerModel> OnPlayerModelChanged;



    public void AddListener(UnityAction<PlayerModel> func)
    {
        OnPlayerModelChanged += func;
    }

    public void RemoveListener(UnityAction<PlayerModel> func)
    {
        OnPlayerModelChanged -= func;
    }

    public void LevelUp()
    {
        OnPlayerModelChanged?.Invoke(this);
    }
}