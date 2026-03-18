using UnityEngine;

public class GameManager : MonoBehaviourSingleton<GameManager>
{
    protected override void Awake()
    {
        base.Awake();

        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            SceneLoader.Instance.LoadScene(SceneName.Scene1);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            SceneLoader.Instance.LoadScene(SceneName.Forest);
        }
    }
}