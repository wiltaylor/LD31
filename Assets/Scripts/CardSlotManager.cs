using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardSlotManager : MonoBehaviour
{

    public float FirstCardX = -214.05f;
    public float FirstCardY = -0.67632f;
    public float SpaceBetwenCards = 1f;
    public float CardWidth = 60f;
    public int Owner = 0;
    public int CardsBeforeScroll = 8;

    private Slider _slider;

    public void Start()
    {
        for (var i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.name == "Slider")
                _slider = child.gameObject.GetComponent<Slider>();
        }

        SortCards();
    }

    public void SetRow()
    {
        SortCards();
    }

    public void SortCards()
    {
        var cards = (from c in GetComponentsInChildren<CardManager>()
            orderby c.TargetPriority
            select c.gameObject).ToArray();

        var index = 0;

        foreach (var c in cards)
        {
            var rt = c.GetComponent<RectTransform>();
            rt.position = new Vector3(FirstCardX + ((CardWidth + SpaceBetwenCards) * index) + transform.position.x, FirstCardY + transform.position.y);
            index++;
        }
    }
}
