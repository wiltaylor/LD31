using System.Linq;
using UnityEngine;
using System.Collections;

public class TurnManager : MonoBehaviour
{

    public static TurnManager Instance;
    public int TurnOwner = 1;
    public AITurnTimeOut AITurnStarter;

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

    public bool CheckForVictory()
    {
        if (PlayerGlobals.Instance.EnemyResources.transform.childCount <= 0)
        {
            GameSetupManager.Instance.ShowGameOver(true);
            return true;
        }

        if (PlayerGlobals.Instance.PlayerResources.transform.childCount <= 0)
        {
            GameSetupManager.Instance.ShowGameOver(false);
            return true;
        }

        return false;
    }

    public void NextTurn()
    {
        if (TargetingManager.Instance.TargetingInProgress)
        {
            TargetingManager.Instance.CancelTargeting();
        }

        //sort all slots.
        HandManager.Instance.GetComponent<CardSlotManager>().SortCards();
        PlayerGlobals.Instance.PlayerResources.SortCards();
        PlayerGlobals.Instance.PlayerMinions.SortCards();
        PlayerGlobals.Instance.EnemyMinions.SortCards();
        PlayerGlobals.Instance.EnemyResources.SortCards();

        //Reset draw limit.
        foreach (var deck in from d in GameObject.FindGameObjectsWithTag("Deck")
            select d.GetComponent<DeckManager>())
        {
            deck.CanDraw = true;
        }

        if (CheckForVictory())
            return;

        TurnOwner = TurnOwner == 1 ? 2 : 1;

        if (TurnOwner == 1)
        {
            ResourceTracker.Instance.PlayerResources = ResourceTracker.Instance.PlayerResourcesPerTurn;
        }
        else
        {
            ResourceTracker.Instance.EnemyResources = ResourceTracker.Instance.EnemyResourcesPerTurn;
        }

        //Proccess Resources and untap
        foreach (var card in from c in GameObject.FindGameObjectsWithTag("Card")
                                 select c.GetComponent<CardManager>())
        {
            if(card.Owner != TurnOwner)
                continue;

            if(card.State != CardState.InPlay)
                continue;

            if (card.FoodPerTurn > 0)
            {
                if (TurnOwner == 1)
                {
                    ResourceTracker.Instance.PlayerFood += card.FoodPerTurn;
                }
                else
                {
                    ResourceTracker.Instance.EnemyFood += card.FoodPerTurn;
                }
            }

            if (card.GoldPerTurn > 0)
            {
                if (TurnOwner == 1)
                {
                    ResourceTracker.Instance.PlayerGold += card.GoldPerTurn;
                }
                else
                {
                    ResourceTracker.Instance.EnemyGold += card.GoldPerTurn;
                }
            }

            if (card.WoodPerTurn > 0)
            {
                if (TurnOwner == 1)
                {
                    ResourceTracker.Instance.PlayerWood += card.WoodPerTurn;
                }
                else
                {
                    ResourceTracker.Instance.EnemyWood += card.WoodPerTurn;
                }
            }

            if(card.Tapped)
                card.Untap();

            card.OnStartOfTurn();
        }


        
        //Upkeep
        foreach (var card in from c in GameObject.FindGameObjectsWithTag("Card")
                             select c.GetComponent<CardManager>())
        {
            if (card.Owner != TurnOwner)
                continue;

            if (card.State != CardState.InPlay)
                continue;

            if (card.FoodPerTurn < 0)
            {
                if (TurnOwner == 1)
                {
                    if (ResourceTracker.Instance.PlayerFood + card.FoodPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.PlayerFood += card.FoodPerTurn;    
                }
                else
                {
                    if (ResourceTracker.Instance.EnemyFood + card.FoodPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.EnemyFood += card.FoodPerTurn;
                }
            }

            if (card.GoldPerTurn < 0)
            {
                if (TurnOwner == 1)
                {
                    if (ResourceTracker.Instance.PlayerGold + card.GoldPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.PlayerGold += card.GoldPerTurn;
                }
                else
                {
                    if (ResourceTracker.Instance.EnemyGold + card.GoldPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.EnemyGold += card.GoldPerTurn;
                }
            }

            if (card.WoodPerTurn < 0)
            {
                if (TurnOwner == 1)
                {
                    if (ResourceTracker.Instance.PlayerWood + card.WoodPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.PlayerWood += card.WoodPerTurn;
                }
                else
                {
                    if (ResourceTracker.Instance.EnemyWood + card.WoodPerTurn < 0)
                        card.Discard();
                    else
                        ResourceTracker.Instance.EnemyWood += card.WoodPerTurn;
                }
            }
        }

        if (TurnOwner == 2)
        {
            AITurnStarter.StartCountDown();
        }

    }
}
