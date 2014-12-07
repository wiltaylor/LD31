using System.Linq;
using UnityEngine;
using System.Collections;

public class FireballScript : MonoBehaviour 
{

    private CardManager _cardman;
    private int OpponentID = 0;
    public int Damage = 5;
    public bool DestroyTarget = false;
    public CardType Type = CardType.Minion;
    public bool LimitType = false;

    public void Start()
    {
        _cardman = GetComponent<CardManager>();
        OpponentID = _cardman.Owner == 1 ? 2 : 1;
    }

    public void OnCast()
    {
        var allCards = (from o in GameObject.FindGameObjectsWithTag("Card")
                        select o.GetComponent<CardManager>()).ToArray();

        CardManager[] cards;
        if (LimitType)
        {
            cards = (from m in allCards
                where m.State == CardState.InPlay
                where m.Type == Type
                where m.Owner == OpponentID
                select m).ToArray();

        }
        else
        {
            cards = (from m in allCards
                where m.State == CardState.InPlay
                where m.Type == CardType.Minion || m.Type == CardType.Resource
                where m.Owner == OpponentID
                select m).ToArray();
        }

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

        if (DestroyTarget)
            cardman.HP = 0;
        else
            cardman.HP -= Damage;

        if (cardman.HP <= 0)
        {
            cardman.Discard();
            SoundManager.Instance.RemoveCard();
        }

        cardman.UpdateCard();

        _cardman.PayCost();
        _cardman.Discard();
    }

    public void OnCast_AI()
    {
        var allCards = (from o in GameObject.FindGameObjectsWithTag("Card")
                        select o.GetComponent<CardManager>()).ToArray();

        CardManager[] cards;
        if (LimitType)
        {
            cards = (from m in allCards
                     where m.State == CardState.InPlay
                     where m.Type == Type
                     where m.Owner == OpponentID
                     select m).ToArray();

        }
        else
        {
            cards = (from m in allCards
                     where m.State == CardState.InPlay
                     where m.Type == CardType.Minion || m.Type == CardType.Resource
                     where m.Owner == OpponentID
                     select m).ToArray();
        }


        if (cards.Length <= 0)
            return;

        OnFinishTargeting(cards.First().gameObject);
    }

    public void OnCancelTargeting()
    {
        return;
    }

}
