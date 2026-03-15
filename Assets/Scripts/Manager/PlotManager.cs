using UnityEngine;
using UnityEngine.Playables;

public enum PlotName
{
    Test1,
    Test2,
}

public class PlotManager : MonoBehaviourSingleton<PlotManager>
{

    protected override void Awake()
    {
        base.Awake();
    }

    public void LoadPlot(PlotName plotName)
    {
        var plot = ResourcesLoader.Instance.LoadPlot(plotName);
        GameObject.Find("Director").GetComponent<PlayableDirector>().Play(plot);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadPlot(PlotName.Test1);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            LoadPlot(PlotName.Test2);
        }
    }
}