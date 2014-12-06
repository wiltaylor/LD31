using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardDragManager : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    public CardManager CardObject;

    private bool _dragging = false;
    private GameObject _icon;
    private Image _cardImage;

    public void Start()
    {
        _cardImage = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _dragging = false;

        if (CardObject.Owner != 1)
            return;

        _icon = new GameObject("Icon");
        var img = _icon.AddComponent<Image>();
        img.sprite = _cardImage.sprite;
        img.SetNativeSize();


        var tlt = TableTopManager.Instance.transform.parent;
        _icon.transform.SetParent(tlt, false);

        DoDragPosition();
        _dragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!_dragging)
            return;

        DoDragPosition();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("Drop End : " + gameObject.name);

       if (!_dragging)
            return;

        if (TableTopManager.Instance.CurrentlyOver != null && TableTopManager.Instance.CurrentlyOver != gameObject)
        {
            var slotman = TableTopManager.Instance.CurrentlyOver.GetComponent<CardSlotManager>();

            if (slotman != null)
            {
                CardObject.transform.SetParent(slotman.transform, false);
                slotman.SortCards();
                TableTopManager.Instance.CurrentlyOver = null;
            }

        }

        Destroy(_icon);
    }

    public void DoDragPosition()
    {
        _icon.transform.position = Input.mousePosition;
    }
}
