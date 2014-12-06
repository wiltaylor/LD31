using UnityEngine;
using System.Collections;

public class TargetingManager : MonoBehaviour
{

    public static TargetingManager Instance;
    public bool TargetingInProgress { get; private set; }

    private CardManager[] _cards;
    private CardManager _callback;

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

    public void StartTargeting(CardManager[] cards, CardManager callback)
    {
        TargetingInProgress = true;
        _cards = cards;
        _callback = callback;

        foreach(var c in cards)
            c.HighlightCard();
    }

    public void FinishTargeting(CardManager card)
    {
        TargetingInProgress = false;
        foreach (var c in _cards)
            c.ClearHighlight();

        _callback.SendMessage("OnFinishTargeting", card.gameObject);
        _cards = null;
        _callback = null;
    }

    public void CancelTargeting()
    {
        TargetingInProgress = false;
        foreach (var c in _cards)
            c.ClearHighlight();

        _callback.SendMessage("OnCancelTargeting");
        _cards = null;
        _callback = null;
    }
    
}
