using System.Linq;
using UnityEngine;
using System.Collections;

public class AITurnManager : MonoBehaviour
{
    public static AITurnManager Instance;

    public DeckManager Deck; 
    
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

    public void RunTurn()
    {
        //Look in hand for Resources and minions to cast.
        CastResources();

        //Look for Magic to cast.
        CastMagic();

        //Buy more cards.
        Deck.OnDraw();

        //Look for resources that have tap abilities that can be called.

        //Attack with non defender minions.

        //End Turn
        MagicSummaryManager.Instance.Show();
        TurnManager.Instance.NextTurn();

    }

    private void CastResources()
    {
        foreach (var card in from c in AIHandManager.Instance.Cards
                                 select c.GetComponent<CardManager>())
        {
            if(card.Type != CardType.Resource && card.Type != CardType.Minion)
                continue;

            card.OnClick();

        }
    }

    private void CastMagic()
    {
        foreach (var card in from c in AIHandManager.Instance.Cards
                             select c.GetComponent<CardManager>())
        {
            if (card.Type != CardType.Magic)
                continue;

            card.OnClick();

        }
    }
}
