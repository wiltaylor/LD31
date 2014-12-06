using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardSlotManager : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{

    public float FirstCardX = -214.05f;
    public float FirstCardY = -0.67632f;
    public float SpaceBetwenCards = 1f;
    public float CardWidth = 60f;
    public bool PlayerOwned = false;

    public void Start()
    {
        SortCards();
    }

    public void SortCards()
    {
        var cards = from c in GetComponentsInChildren<CardManager>()
                    select c.gameObject;

        var index = 0;

        foreach (var c in cards)
        {
            var rt = c.GetComponent<RectTransform>();
            rt.position = new Vector3(FirstCardX + ((CardWidth + SpaceBetwenCards) * index) + transform.position.x, FirstCardY + transform.position.y);
            index++;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Dropped: " + gameObject.name);
        TableTopManager.Instance.CurrentlyOver = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (TableTopManager.Instance.CurrentlyOver == gameObject)
            TableTopManager.Instance.CurrentlyOver = null;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        TableTopManager.Instance.CurrentlyOver = gameObject;
    }
}
