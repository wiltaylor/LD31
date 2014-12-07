using UnityEngine;
using System.Collections;

public class TrainUnitManager : MonoBehaviour
{

    public GameObject UnitToTrain;
    private CardManager _cardMan;

    public void Start()
    {
        _cardMan = GetComponent<CardManager>();
    }

    public void OnClick_InPlay()
    {
        if (_cardMan.Owner != 1)
            return;

        if (_cardMan.Tapped)
            return;

        var NewUnit = Instantiate(UnitToTrain);
        var cardman = NewUnit.GetComponent<CardManager>();
        cardman.Owner = 1;

        if (ResourceTracker.Instance.PlayerFood - cardman.FoodCost < 0)
            return;

        if (ResourceTracker.Instance.PlayerGold - cardman.GoldCost < 0)
            return;

        if (ResourceTracker.Instance.PlayerWood - cardman.WoodCost < 0)
            return;

        if (cardman.Type == CardType.Minion)
        {
            NewUnit.transform.SetParent(PlayerGlobals.Instance.PlayerMinions.transform);
            PlayerGlobals.Instance.PlayerMinions.SortCards();
            cardman.State = CardState.InPlay;
        }

        if (cardman.Type == CardType.Resource)
        {
            NewUnit.transform.SetParent(PlayerGlobals.Instance.PlayerResources.transform);
            PlayerGlobals.Instance.PlayerResources.SortCards();
            cardman.State = CardState.InPlay;
        }

        if (cardman.Type == CardType.Magic)
        {
            if (!HandManager.Instance.CanDraw())
            {
                Destroy(NewUnit);
                return;
            }

            HandManager.Instance.AddCard(NewUnit);
            return;
        }
        cardman.PayCost();
        _cardMan.Tap();
    }

    public void OnClick_InPlayAI()
    {
        if (_cardMan.Owner != 2)
            return;

        if (_cardMan.Tapped)
            return;

        var NewUnit = Instantiate(UnitToTrain);
        var cardman = NewUnit.GetComponent<CardManager>();
        cardman.Owner = 2;

        if (ResourceTracker.Instance.EnemyFood - cardman.FoodCost < 0)
            return;

        if (ResourceTracker.Instance.EnemyGold - cardman.GoldCost < 0)
            return;

        if (ResourceTracker.Instance.EnemyWood - cardman.WoodCost < 0)
            return;

        if (cardman.Type == CardType.Minion)
        {
            NewUnit.transform.SetParent(PlayerGlobals.Instance.EnemyMinions.transform);
            PlayerGlobals.Instance.EnemyMinions.SortCards();
        }

        if (cardman.Type == CardType.Resource)
        {
            NewUnit.transform.SetParent(PlayerGlobals.Instance.EnemyResources.transform);
            PlayerGlobals.Instance.EnemyResources.SortCards();
        }

        if (cardman.Type == CardType.Magic)
        {
            if (!AIHandManager.Instance.CanDraw())
            {
                Destroy(NewUnit);
                return;
            }

            AIHandManager.Instance.AddCard(NewUnit);
            _cardMan.Tap();
            return;
        }
        cardman.PayCost();
        _cardMan.Tap();
    }
}
