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
        //TODO: Click code.
    }

    public void OnMouseOver()
    {
        ImagePreviewManager.Instance.SetCard(CardPreviewImage, Name, CardText, Type, HP, Damage, GoldCost, WoodCost, FoodCost);
    }

}
