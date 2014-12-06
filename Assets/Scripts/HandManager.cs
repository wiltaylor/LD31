using UnityEngine;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }
    public int MaxHandSize = 7;
    private CardSlotManager _slotManager;
    private int CardsInHand = 0;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        _slotManager = GetComponent<CardSlotManager>();
    }

    public void RemoveCardFromHand(GameObject card)
    {
        CardsInHand--;
        _slotManager.SortCards();
    }

    public bool CanDraw()
    {
        return CardsInHand < MaxHandSize;
    }

    public void AddCard(GameObject card)
    {
        CardsInHand++;
        card.transform.SetParent(transform, false);
        _slotManager.SortCards();
    }


}
