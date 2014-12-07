using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DeckManager : MonoBehaviour
{

    public int Owner = 0;
    public string[] DeckListing;
    public bool CanDraw = true;

    private Stack<string> _deck;
    private Image _image;

    public void Awake()
    {
        _image = GetComponent<Image>();
        //BuildDeck();
    }

    public void OnDraw()
    {
        if (!CanDraw)
            return;

        CanDraw = false;

        if (Owner == 1)
        {
            if (ResourceTracker.Instance.PlayerGold - PlayerGlobals.Instance.GoldCostPerDraw < 0)
                return;

            ResourceTracker.Instance.PlayerGold -= PlayerGlobals.Instance.GoldCostPerDraw;
        }

        if (Owner == 2)
        {
            if (ResourceTracker.Instance.EnemyGold - PlayerGlobals.Instance.GoldCostPerDraw < 0)
                return;

            ResourceTracker.Instance.EnemyGold -= PlayerGlobals.Instance.GoldCostPerDraw;
        }
        Draw(1);
    }

    public void Draw(int Qty)
    {
        for (var q = 0; q < Qty; q++)
        {
            if (_deck.Count == 0)
            {
                _image.enabled = false;
                return;
            }


            if (Owner == 1)
            {
                if (!HandManager.Instance.CanDraw())
                    return;

                var card = _deck.Pop();
                var cardObj = Resources.Load<GameObject>(card);

                HandManager.Instance.AddCard((GameObject)Instantiate(cardObj));
            }

            if (Owner == 2)
            {
                if (!AIHandManager.Instance.CanDraw())
                    return;

                var card = _deck.Pop();
                var cardObj = Resources.Load<GameObject>(card);

                AIHandManager.Instance.AddCard((GameObject)Instantiate(cardObj));
            }
        }
    }

    public void BuildDeck()
    {
        _deck = Shuffle(DeckListing);
        _image.enabled = true;
    }

    public static Stack<T> Shuffle<T>(IList<T> list)
    {
        var rng = new System.Random();
        var n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }

        return new Stack<T>(list);
    }
}
