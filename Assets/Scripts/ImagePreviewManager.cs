using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImagePreviewManager : MonoBehaviour {

    public static ImagePreviewManager Instance { get; private set; }
    public Text TitleText;
    public Text CardText;
    public Text TypeText;
    public Text CombatText;
    public Image CardImage;
    public Text FoodCostText;
    public Text GoldCostText;
    public Text WoodCostText;
    public Sprite CardTemplate;
    public Sprite BackOfCard;
    public Image FoodIcon;
    public Image WoodIcon;
    public Image GoldIcon;

    private Image _image;

    // Use this for initialization
	void Awake () 
    {
	    if (Instance == null)
	    {
	        Instance = this;
	    }
	    else
	    {
	        Destroy(gameObject);
	    }

	    _image = GetComponent<Image>();

	    SetNoCard();

    }

    public void SetNoCard()
    {
        CardImage.enabled = false;
        _image.sprite = BackOfCard;
        FoodIcon.enabled = false;
        WoodIcon.enabled = false;
        GoldIcon.enabled = false;
        FoodCostText.text = "";
        WoodCostText.text = "";
        GoldCostText.text = "";
        TypeText.text = "";
        CombatText.text = "";
        TitleText.text = "";
        CardText.text = "";

    }

    public void SetCard(Sprite image, string title, string text, CardType type, int hp, int damage, int gold, int wood, int food)
    {
        _image.sprite = CardTemplate;
        FoodIcon.enabled = true;
        WoodIcon.enabled = true;
        GoldIcon.enabled = true;

        CardImage.enabled = true;
        CardImage.sprite = image;
        TitleText.text = title;
        CardText.text = text;
        FoodCostText.text = food.ToString();
        WoodCostText.text = wood.ToString();
        GoldCostText.text = gold.ToString();

        switch (type)
        {
            case CardType.Magic:
                TypeText.text = "Magic";
                CombatText.text = "";
                break;
            case CardType.Minion:
                TypeText.text = "Minion";
                CombatText.text = string.Format("{0}/{1}", damage, hp);
                break;
            case CardType.Resource:
                TypeText.text = "Resource";
                CombatText.text = string.Format("{0}/{1}", damage, hp);
                break;
        }

        

    }
	

}
