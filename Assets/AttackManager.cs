using System.Linq;
using UnityEngine;
using System.Collections;

public class AttackManager : MonoBehaviour
{

    private CardManager _cardman;
    private int OpponentID = 0;

    public void Start()
    {
        _cardman = GetComponent<CardManager>();
        OpponentID = _cardman.Owner == 1 ? 2 : 1;
    }

    public void OnClick_InPlay()
    {

        var AllCards = (from o in GameObject.FindGameObjectsWithTag("Card")
            select o.GetComponent<CardManager>()).ToArray();

        var AllDefenderMinions = from m in AllCards
            where m.State == CardState.InPlay
            where m.Type == CardType.Minion
            where m.Owner == OpponentID
            where m.Defender
            select m;

        var AllMinions = from m in AllCards
            where m.State == CardState.InPlay
            where m.Owner == OpponentID
            where m.Type == CardType.Minion
            select m;

        var AllDefenderResources = from r in AllCards
            where r.State == CardState.InPlay
            where r.Owner == OpponentID
            where r.Defender
            where r.Type == CardType.Resource
            select r;

        var AllResources = from r in AllCards
            where r.State == CardState.InPlay
            where r.Owner == OpponentID
            where r.Type == CardType.Resource
            select r;

        var cards = AllDefenderMinions.ToArray();

        if (cards.Length != 0)
        {
            TargetingManager.Instance.StartTargeting(cards, _cardman);
            return;
        }

        cards = AllMinions.ToArray();

        if (cards.Length != 0)
        {
            TargetingManager.Instance.StartTargeting(cards, _cardman);
            return;
        }

        cards = AllDefenderResources.ToArray();

        if (cards.Length != 0)
        {
            TargetingManager.Instance.StartTargeting(cards, _cardman);
            return;
        }

        cards = AllResources.ToArray();

        if (cards.Length != 0)
        {
            TargetingManager.Instance.StartTargeting(cards, _cardman);
        }
    }

    public void OnFinishTargeting(GameObject obj)
    {
        var cardman = obj.GetComponent<CardManager>();

        _cardman.Tap();

        cardman.HP -= _cardman.Damage;
        _cardman.HP -= cardman.Damage;

        if(cardman.HP <= 0)
            cardman.Discard();
        if(_cardman.HP <= 0)
           _cardman.Discard();
    }
}
