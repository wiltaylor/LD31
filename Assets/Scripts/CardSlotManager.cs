using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CardSlotManager : MonoBehaviour
{

    public float FirstCardX = -214.05f;
    public float FirstCardY = -0.67632f;
    public float SpaceBetwenCards = 1f;
    public float CardWidth = 60f;
    public int Owner = 0;

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
}
