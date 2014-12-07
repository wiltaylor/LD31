using System.Linq;
using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour 
{

    private CardManager _cardman;
    private int OpponentID = 0;
    public int Damage = 5;

    public void Start()
    {
        _cardman = GetComponent<CardManager>();
        OpponentID = _cardman.Owner == 1 ? 2 : 1;
    }

    public void OnCast()
    {
        var allCards = (from o in GameObject.FindGameObjectsWithTag("Card")
                        select o.GetComponent<CardManager>()).ToArray();

        var allTargets = from m in allCards
                         where m.State == CardState.InPlay
                         where m.Type == CardType.Minion || m.Type == CardType.Resource
                         where m.Owner == OpponentID
                         select m;

        var cards = allTargets.ToArray();

        if (cards.Length <= 0)
            return;

        TargetingManager.Instance.StartTargeting(cards, _cardman);
    }

    public void OnFinishTargeting(GameObject obj)
    {
        var cardman = obj.GetComponent<CardManager>();

        if (_cardman.Owner == 1)
        {
            transform.SetParent(null);
            HandManager.Instance.RemoveCardFromHand(gameObject);
            HandManager.Instance.GetComponent<CardSlotManager>().SortCards();
        }
        else
        {
            AIHandManager.Instance.RemoveCard(gameObject);
            MagicSummaryManager.Instance.AddItem(_cardman.CardPreviewImage, cardman.CardPreviewImage);
   }

        cardman.HP -= Damage;

        if (cardman.HP <= 0)
            cardman.Discard();

        _cardman.PayCost();
        _cardman.Discard();
    }

    public void OnCast_AI()
    {
        var allCards = (from o in GameObject.FindGameObjectsWithTag("Card")
                        select o.GetComponent<CardManager>()).ToArray();

        var allTargets = from m in allCards
                         where m.State == CardState.InPlay
                         where m.Type == CardType.Minion || m.Type == CardType.Resource
                         where m.Owner == OpponentID
                         select m;

        var cards = allTargets.ToArray();

        if (cards.Length <= 0)
            return;

        OnFinishTargeting(cards.First().gameObject);
    }

}
