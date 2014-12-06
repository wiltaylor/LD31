﻿using System;
using System.Linq;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum CardType
{
    Resource,
    Minion,
    Magic
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
    private Image _image;
    private GameObject _cardObject;


    public void Start()
    {
        _image = GetComponent<Image>();
        _cardObject = GetComponentInChildren<Image>().gameObject;
    }

    public void HighlightCard()
    {
        _image.enabled = true;
    }

    public void ClearHighlight()
    {
        _image.enabled = false;
    }

    public void Tap()
    {
        if (Tapped)
            return ;

        Tapped = true;

        _cardObject.transform.rotation = Quaternion.Euler(0, 0, 90);

    }

    public void Untap()
    {
        if (!Tapped)
            return;

        Tapped = false;

        _cardObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnClick()
    {
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

                    ResourceTracker.Instance.PlayerFood -= FoodCost;
                    ResourceTracker.Instance.PlayerGold -= GoldCost;
                    ResourceTracker.Instance.PlayerWood -= WoodCost;
                    State = CardState.InPlay;
                }
                else
                {
                    if (ResourceTracker.Instance.EnemyResources < 1)
                        return;

                    transform.SetParent(PlayerGlobals.Instance.EnemyResources.transform, false);
                    PlayerGlobals.Instance.EnemyResources.SortCards();
                    AIHandManager.Instance.RemoveCard(gameObject);
                    ResourceTracker.Instance.EnemyResources--;

                    ResourceTracker.Instance.EnemyFood -= FoodCost;
                    ResourceTracker.Instance.EnemyGold -= GoldCost;
                    ResourceTracker.Instance.EnemyWood -= WoodCost;
                    State = CardState.InPlay;
                }
            }

            if (Type == CardType.Minion)
            {
                if (Owner == 1)
                {
                    transform.SetParent(PlayerGlobals.Instance.PlayerMinions.transform, false);
                    PlayerGlobals.Instance.PlayerMinions.SortCards();
                    HandManager.Instance.RemoveCardFromHand(gameObject);

                    ResourceTracker.Instance.PlayerFood -= FoodCost;
                    ResourceTracker.Instance.PlayerGold -= GoldCost;
                    ResourceTracker.Instance.PlayerWood -= WoodCost;
                    State = CardState.InPlay;
                }
                else
                {
                    transform.SetParent(PlayerGlobals.Instance.EnemyMinions.transform, false);
                    PlayerGlobals.Instance.EnemyMinions.SortCards();
                    AIHandManager.Instance.RemoveCard(gameObject);

                    ResourceTracker.Instance.EnemyFood -= FoodCost;
                    ResourceTracker.Instance.EnemyGold -= GoldCost;
                    ResourceTracker.Instance.EnemyWood -= WoodCost;
                    State = CardState.InPlay;
                }
            }

            if (Type == CardType.Magic)
            {
                //TODO: Targeting system.
            }
        }


    }

    public void OnMouseOver()
    {
        ImagePreviewManager.Instance.SetCard(CardPreviewImage, Name, CardText, Type, HP, Damage, GoldCost, WoodCost, FoodCost);
    }

    public void Discard()
    {
        State = CardState.Discard;
        Destroy(gameObject);
    }
}
