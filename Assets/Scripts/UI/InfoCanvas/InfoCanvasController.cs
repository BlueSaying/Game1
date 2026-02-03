using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InfoCanvasController : MonoBehaviour
{
    public RectTransform selectedArrow;

    public RectTransform damageNum;

    public RectTransform miniHPBar;

    public void ShowSelectedArrow()
    {
        selectedArrow.gameObject.SetActive(true);
    }
    public void HideSelectedArrow()
    {
        selectedArrow.gameObject.SetActive(false);
    }

    public void ShowDamageNum(int damage)
    {
        damageNum.gameObject.SetActive(true);
        damageNum.position = new Vector3(0, 250, 0);
        damageNum.DOMoveY(300.0f, 0.5f).OnComplete(() =>
        {
            damageNum.gameObject.SetActive(false);
        });
    }

    public void ShowHPInfo()
    {
        miniHPBar.gameObject.SetActive(true);
    }
    public void HideHPInfo()
    {
        miniHPBar.gameObject.SetActive(false);
    }

    public void UpdateHPInfo(int newHP, int maxHP)
    {
        var slider = miniHPBar.GetComponent<Slider>();
        slider.minValue = 0;
        slider.maxValue = maxHP;

        if (transform.parent.gameObject.activeSelf)
        {
            StartCoroutine(TransBar(slider, newHP));
        }
        else
        {
            slider.value = newHP;
        }

        miniHPBar.Find("HealthText").GetComponent<TMP_Text>().text = newHP.ToString() + " / " + maxHP.ToString();
    }

    private IEnumerator TransBar(Slider slider, int newValue)
    {
        float timer = 0.0f;
        float duration = 0.2f;
        float curValue = slider.value;

        while (timer < duration)
        {
            slider.value = Mathf.Lerp(curValue, newValue, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }

        slider.value = newValue;
        yield return null;
    }
}