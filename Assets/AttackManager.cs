using System.Collections.Generic;
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

        var cards = GetTargets();

        if (cards.Length <= 0)
            return;

        TargetingManager.Instance.StartTargeting(cards, _cardman);
    }

    public void OnClick_InPlayAI()
    {
        var cards = GetTargets();

        if (cards.Length <= 0)
            return;

        var card = cards.First();

        MagicSummaryManager.Instance.AddItem(_cardman.CardPreviewImage, card.CardPreviewImage);
        OnFinishTargeting(card.gameObject);
    }

    public void OnFinishTargeting(GameObject obj)
    {
        var cardman = obj.GetComponent<CardManager>();

        _cardman.Tap();

        cardman.HP -= _cardman.Damage;
        _cardman.HP -= cardman.Damage;

        if (cardman.HP <= 0)
            cardman.Discard();
        if (_cardman.HP <= 0)
            _cardman.Discard();
    }

    private CardManager[] GetTargets()
    {
        var AllCards = (from o in GameObject.FindGameObjectsWithTag("Card")
                        select o.GetComponent<CardManager>()).ToArray();

        var AllDefenderMinions = (from m in AllCards
                                 where m.State == CardState.InPlay
                                 where m.Type == CardType.Minion
                                 where m.Owner == OpponentID
                                 where m.Defender
                                 select m).ToArray();
        
        if (AllDefenderMinions.Length > 0)
            return AllDefenderMinions;


        var AllMinions = (from m in AllCards
                         where m.State == CardState.InPlay
                         where m.Owner == OpponentID
                         where m.Type == CardType.Minion
                         select m).ToArray();

        if (AllMinions.Length > 0)
            return AllMinions;
        
        var AllDefenderResources = (from r in AllCards
                                   where r.State == CardState.InPlay
                                   where r.Owner == OpponentID
                                   where r.Defender
                                   where r.Type == CardType.Resource
                                   select r).ToArray();

        if (AllDefenderResources.Length > 0)
            return AllDefenderMinions;

        var AllResources = (from r in AllCards
                           where r.State == CardState.InPlay
                           where r.Owner == OpponentID
                           where r.Type == CardType.Resource
                           select r).ToArray();

        return AllResources.Length > 0 ? AllResources : null;
    }
}
