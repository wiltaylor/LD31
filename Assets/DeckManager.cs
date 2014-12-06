using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class DeckManager : MonoBehaviour
{

    public int Owner = 0;
    public string[] DeckListing;

    private Stack<string> _deck;

    public void Start()
    {
        BuildDeck();
    }

    public void OnDraw()
    {
        if (_deck.Count == 0)
        {
            gameObject.SetActive(false);
            return;
        }
            

        if (Owner == 1)
        {
            if (!HandManager.Instance.CanDraw())
                return;

            var card = _deck.Pop();
            var cardObj = Resources.Load<GameObject>(card);

            HandManager.Instance.AddCard(Instantiate(cardObj));
        }
    }

    public void BuildDeck()
    {
        _deck = Shuffle(DeckListing);
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
