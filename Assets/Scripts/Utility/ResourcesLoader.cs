using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// 专门用来管理从Resources文件加载东西的类
public class ResourcesLoader : Singleton<ResourcesLoader>
{
    // Prefab
    // 武器
    private Dictionary<string, GameObject> weaponDic = new Dictionary<string, GameObject>();
    private static readonly string weaponPath = "Prefabs/Weapons/";

    // 玩家
    private Dictionary<string, GameObject> playerDic = new Dictionary<string, GameObject>();
    private static readonly string playerPath = "Prefabs/Player";

    // 敌人
    private Dictionary<string, GameObject> enemyDic = new Dictionary<string, GameObject>();
    private static readonly string enemyPath = "Prefabs/Enemies/";

    // 特效
    private Dictionary<string, GameObject> effectDic = new Dictionary<string, GameObject>();
    private static readonly string effectPath = "Prefabs/Effects/";

    // UI
    private Dictionary<string, GameObject> panelDic = new Dictionary<string, GameObject>();
    private static readonly string panelPath = "Prefabs/Panels/";

    // 音效
    private static readonly string audioPath = "Audios/";

    // Sprite
    private Dictionary<string, Sprite> spriteDic = new Dictionary<string, Sprite>();
    private static readonly string spritePath = "Sprites/";

    private ResourcesLoader() { }

    public GameObject LoadWeapon(string weaponName)
    {
        if (weaponDic.ContainsKey(weaponName)) return weaponDic[weaponName];

        // NOTE:此处可优化，因为每次都要遍历整个文件
        GameObject newWeapon = Resources.LoadAll<GameObject>(weaponPath).Where(x => x.name == weaponName).ToArray()[0];
        weaponDic.Add(weaponName, newWeapon);
        return newWeapon;
    }

    public GameObject LoadPlayer(string playerName)
    {
        if (playerDic.ContainsKey(playerName)) return playerDic[playerName];

        GameObject newPlayer = Resources.LoadAll<GameObject>(playerPath).Where(x => x.name == playerName).ToArray()[0];
        playerDic.Add(playerName, newPlayer);
        return newPlayer;
    }

    public GameObject LoadEnemy(string enemyName)
    {
        if (enemyDic.ContainsKey(enemyName)) return enemyDic[enemyName];

        GameObject newEnemy = Resources.LoadAll<GameObject>(enemyPath).Where(x => x.name == enemyName).ToArray()[0];
        enemyDic.Add(enemyName, newEnemy);
        return newEnemy;
    }

    public GameObject LoadEffect(string effectName)
    {
        if (effectDic.ContainsKey(effectName)) return effectDic[effectName];

        GameObject newEffect = Resources.LoadAll<GameObject>(effectPath).Where(x => x.name == effectName).ToArray()[0];
        effectDic.Add(effectName, newEffect);
        return newEffect;
    }

    public GameObject LoadPanel(string sceneName, string panelName)
    {
        if (panelDic.ContainsKey(sceneName + panelName)) return panelDic[sceneName + panelName];

        GameObject newPanel = Resources.LoadAll<GameObject>(panelPath + sceneName).Where(x => x.name == panelName).ToArray()[0];
        panelDic.Add(sceneName + panelName, newPanel);
        return newPanel;
    }

    public AudioClip LoadAudioClip(string audioType, string audioName)
    {
        return Resources.LoadAll<AudioClip>(audioPath + audioType).Where(x => x.name == audioName).ToArray()[0];
    }

    public Sprite LoadSprite(string spriteType, string spriteName)
    {
        if (spriteDic.ContainsKey(spriteName)) return spriteDic[spriteName];

        Sprite newSprite = Resources.LoadAll<Sprite>(spritePath + spriteType).Where(x => x.name == spriteName).ToArray()[0];
        spriteDic.Add(spriteName, newSprite);
        return newSprite;
    }
}