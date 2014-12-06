using System.Linq;
using UnityEngine;
using System.Collections;

public class AITurnManager : MonoBehaviour
{
    public static AITurnManager Instance;
    
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
        //Look in hand for Resources to cast.
        CastResources();

        //Look for Minions to cast.

        //Look for Magic to cast.

        //Buy more cards.

        //Look for resources that have tap abilities that can be called.

        //Attack with non defender minions.

        //End Turn
        TurnManager.Instance.NextTurn();

    }

    private void CastResources()
    {
        foreach (var card in from c in AIHandManager.Instance.Cards
                                 select c.GetComponent<CardManager>())
        {
            if(card.Type != CardType.Resource)
                continue;

            card.OnClick();

        }
    }
}
