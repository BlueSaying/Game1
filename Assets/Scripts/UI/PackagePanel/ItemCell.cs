using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemCell : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public int itemIndex;
    public Item item;

    public void Init(int itemIndex, Item item)
    {
        this.itemIndex = itemIndex;
        this.item = item;

        transform.Find("Icon").GetComponent<Image>().sprite = ResourcesLoader.Instance.LoadSprite(SpriteType.ItemIcon, item.Name);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PackageManager.Instance.UpdateChosenInfo(itemIndex, PackageManager.Instance.chosenItemType);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
    }
}