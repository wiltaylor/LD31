using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType
{
    Resource,
    Minion,
    Magic,
    ResourceAndMinion
}

public enum CardState
{
    InHand,
    InPlay,
    Discard
}

public class CardManager : MonoBehaviour
{
    public string Name;
    public CardType Type;
    public int GoldPerTurn = 0;
    public int FoodPerTurn = 0;
    public int WoodPerTurn = 0;
    public int GoldCost = 0;
    public int FoodCost = 0;
    public int WoodCost = 0;
    public int HP = 100;
    public bool Defender = false;
    public int Damage = 0;
    public bool Tapped;
    public int Owner = 0;
    public Sprite CardPreviewImage;
    public string CardText;
    public CardState State;
    public bool SummonSickness = true;
    private Image _image;
    private GameObject _cardObject;
    private bool _highlighted;
    public int TargetPriority = 100;
    public Text ZZZText;
    public Text CombatText;

    public void UpdateCard()
    {
        if (SummonSickness && State == CardState.InPlay)
            ZZZText.text = "ZZZ";
        else
            ZZZText.text = "";

        if (Type == CardType.Magic)
        {
            CombatText.text = "";
        }
        else
        {
            CombatText.text = Damage + "/" + HP;
        }
    }


    public void Start()
    {
        _image = GetComponent<Image>();
        _cardObject = GetComponentInChildren<Image>().gameObject;
        UpdateCard();
    }

    public void HighlightCard()
    {
        _image.enabled = true;
        _highlighted = true;
    }

    public void ClearHighlight()
    {
        _image.enabled = false;
        _highlighted = false;
    }

    public void Tap()
    {
        if (Tapped)
            return ;

        Tapped = true;

        _cardObject.transform.rotation = Quaternion.Euler(0, 0, 90);
        UpdateCard();

    }

    public void Untap()
    {
        if (!Tapped)
            return;

        Tapped = false;

        _cardObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        UpdateCard();
    }

    public void OnClick()
    {
        if (TargetingManager.Instance.TargetingInProgress)
        {
            if (_highlighted)
            {
                TargetingManager.Instance.FinishTargeting(this);
                return;
            }

            return;
        }


        if (State == CardState.InHand)
        {
            if (Owner == 1)
            {
                if (ResourceTracker.Instance.PlayerFood - FoodCost < 0)
                    return;
                if (ResourceTracker.Instance.PlayerGold - GoldCost < 0)
                    return;
                if (ResourceTracker.Instance.PlayerWood - WoodCost < 0)
                    return;
            }
            else
            {
                if (ResourceTracker.Instance.EnemyFood - FoodCost < 0)
                    return;
                if (ResourceTracker.Instance.EnemyGold - GoldCost < 0)
                    return;
                if (ResourceTracker.Instance.EnemyWood - WoodCost < 0)
                    return;
            }

            if (Type == CardType.Resource)
            {
                if (Owner == 1)
                {
                    if (ResourceTracker.Instance.PlayerResources < 1)
                        return;

                    transform.SetParent(PlayerGlobals.Instance.PlayerResources.transform, false);
                    PlayerGlobals.Instance.PlayerResources.SortCards();
                    HandManager.Instance.RemoveCardFromHand(gameObject);
                    ResourceTracker.Instance.PlayerResources--;

                    PayCost();
                    State = CardState.InPlay;
                    UpdateCard();
                }
                else
                {
                    if (ResourceTracker.Instance.EnemyResources < 1)
                        return;

                    transform.SetParent(PlayerGlobals.Instance.EnemyResources.transform, false);
                    PlayerGlobals.Instance.EnemyResources.SortCards();
                    AIHandManager.Instance.RemoveCard(gameObject);
                    ResourceTracker.Instance.EnemyResources--;

                    PayCost();
                    State = CardState.InPlay;
                    UpdateCard();
                }
            }

            if (Type == CardType.Minion)
            {

                if (Owner == 1)
                {
                    transform.SetParent(PlayerGlobals.Instance.PlayerMinions.transform, false);
                    PlayerGlobals.Instance.PlayerMinions.SortCards();
                    HandManager.Instance.RemoveCardFromHand(gameObject);

                    PayCost();
                    State = CardState.InPlay;
                    UpdateCard();
                }
                else
                {
                    transform.SetParent(PlayerGlobals.Instance.EnemyMinions.transform, false);
                    PlayerGlobals.Instance.EnemyMinions.SortCards();
                    AIHandManager.Instance.RemoveCard(gameObject);

                    PayCost();
                    State = CardState.InPlay;
                    UpdateCard();
                }
            }

            if (Type == CardType.Magic)
            {
                if (Owner == 1)
                {
                    gameObject.SendMessage("OnCast");
                }
                else
                {
                    gameObject.SendMessage("OnCast_AI");
                }
            }

            return;
        }

        if (State == CardState.InPlay && !Tapped)
        {
            if (SummonSickness && Type == CardType.Minion)
                return;

            if(Owner == 1)
                gameObject.SendMessage("OnClick_InPlay");
            else
                gameObject.SendMessage("OnClick_InPlayAI");
        }

    }

    public void OnMouseOver()
    {
        ImagePreviewManager.Instance.SetCard(CardPreviewImage, Name, CardText, Type, HP, Damage, GoldCost, WoodCost, FoodCost, GoldPerTurn, WoodPerTurn, FoodPerTurn);
    }

    public void Discard()
    {
        transform.SetParent(null);
        State = CardState.Discard;
        Destroy(gameObject);
    }

    public void PayCost()
    {
        if (Owner == 1)
        {
            ResourceTracker.Instance.PlayerFood -= FoodCost;
            ResourceTracker.Instance.PlayerGold -= GoldCost;
            ResourceTracker.Instance.PlayerWood -= WoodCost;
        }
        else
        {
            ResourceTracker.Instance.EnemyFood -= FoodCost;
            ResourceTracker.Instance.EnemyGold -= GoldCost;
            ResourceTracker.Instance.EnemyWood -= WoodCost;
        }
    }

    public void OnStartOfTurn()
    {
        if (State == CardState.InPlay)
            SummonSickness = false;

        UpdateCard();
    }
}
