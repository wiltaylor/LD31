using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class AIHandManager : MonoBehaviour
{

    public int MaxCardsInHand = 7;
    public static AIHandManager Instance;

    private readonly List<GameObject> _cards = new List<GameObject>();

    public GameObject[] Cards
    {
        get { return _cards.ToArray(); }
    }

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

    public void RemoveCard(GameObject card)
    {
        _cards.Remove(card);
    }

    public bool CanDraw()
    {
        return _cards.Count < MaxCardsInHand;
    }

    public void AddCard(GameObject card)
    {
        var cardman = card.GetComponent<CardManager>();
        cardman.Owner = 2;

        card.transform.SetParent(transform, false);

        _cards.Add(card);
    }

    public void ClearHand()
    {
        _cards.Clear();
    }
}
