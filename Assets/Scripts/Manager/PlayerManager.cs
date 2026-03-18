using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
    public PlayerController PlayerController { get; private set; }

    public PlayerUnit PlayerUnit { get; private set; }
    public PlayerModel PlayerModel => PlayerUnit.Model;

    public Transform Player { get; private set; }

    private void Start()
    {
        Player = transform.Find("Player");
        PlayerController = Player.GetComponent<PlayerController>();
        PlayerUnit = Player.GetComponent<PlayerUnit>();

        // 保险措施
        DisablePlayer();

        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchStart, DisablePlayer);
        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchComplete, () =>
        {
            // 如果当前没有加载到主菜单
            if (SceneManager.GetActiveScene().name != SceneName.MainMenu.ToString())
            {
                DisablePlayer();
                ReSetPlayerPositionAndRotation();
                EnablePlayer();
            }
        });
    }

    public void ReSetPlayerPositionAndRotation()
    {
        
        SetPlayerPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void SetPlayerPositionAndRotation(Vector3 pos, Quaternion rot)
    {
        Player.position = pos;
        Player.rotation = rot;
    }

    public void FreezePlayerMove()
    {
        PlayerController?.FreezeMove();
    }

    public void UnFreezePlayerMove()
    {
        PlayerController?.UnFreezeMove();
    }

    public void DisablePlayer()
    {
        Player.gameObject.SetActive(false);
    }

    public void EnablePlayer()
    {
        Player.gameObject.SetActive(true);
    }
}