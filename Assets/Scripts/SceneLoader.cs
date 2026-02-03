using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// 场景名称
/// </summary>
public enum SceneName
{
    /// <summary>
    /// 启动场景，不得在游戏过程中调用
    /// </summary>
    //BootstrapScene,

    MainMenu,

    Scene1,
}

public class SceneLoader : MonoBehaviourSingleton<SceneLoader>
{
    bool isSceneLoading = false;

    public List<GameObject> dontDestroyOnLoadObjs;

    /// <summary>
    /// 一次性事件
    /// </summary>
    public event UnityAction OnSceneStartChange;
    /// <summary>
    /// 一次性事件
    /// </summary>
    public event UnityAction OnSceneFinishChanage;

    public Image sceneLoadAnimatorImage;

    protected override void Awake()
    {
        base.Awake();

        DontDestroyOnLoad(gameObject);
        foreach (var obj in dontDestroyOnLoadObjs)
        {
            DontDestroyOnLoad(obj);
        }
    }

    void Start()
    {
        // 加载完Bootstrap后直接加载MainMenu
        LoadScene(SceneName.MainMenu);
    }

    public void LoadScene(SceneName sceneName)
    {
        if (isSceneLoading) return;
        isSceneLoading = true;

        // 每次切换场景前都要放到最前面
        sceneLoadAnimatorImage.transform.SetAsLastSibling();
        sceneLoadAnimatorImage.DOFade(1.0f, 0.6f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
        {
            // 仅触发一次
            OnSceneStartChange?.Invoke();
            OnSceneStartChange = null;
            // 永久触发
            EventCenter.Instance.NotifyEvent(EventType.OnSceneSwitchStart);

            AsyncOperation op = SceneManager.LoadSceneAsync(sceneName.ToString());
            op.completed += (AsyncOperation op) =>
            {
                isSceneLoading = false;

                // 仅调用一次
                OnSceneFinishChanage?.Invoke();
                OnSceneFinishChanage = null;
                // 永久触发
                EventCenter.Instance.NotifyEvent(EventType.OnSceneSwitchComplete);

                sceneLoadAnimatorImage.transform.SetAsLastSibling();
                sceneLoadAnimatorImage
                .DOFade(0.0f, 0.6f)
                .SetEase(Ease.InQuad);
            };
        });
    }

    private void Update()
    {


    }
}