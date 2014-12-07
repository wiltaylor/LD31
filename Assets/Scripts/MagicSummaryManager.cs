using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagicSummaryManager : MonoBehaviour
{

    public static MagicSummaryManager Instance;
    public GameObject Panel;
    public Scrollbar Scrollbar;

    public float StartY = -2177;
    public float StartX = -3f;
    public float YSpace = 130f;
    public Sprite ArrowImage;

    private Canvas _canvas;


    private readonly List<MagicActionInfo> _magicList = new List<MagicActionInfo>();
    
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _canvas = GetComponent<Canvas>();
        }
        else
        {
            Destroy(gameObject);
        }

        Accept();
    }

    public void Accept()
    {
        _canvas.enabled = false;
        _magicList.Clear();
        foreach (Transform tran in Panel.transform)
            Destroy(tran.gameObject);

        

    }

    public void Show()
    {

        if(_magicList.Count <= 0)
            return;

        Scrollbar.value = 0;

        _canvas.enabled = true;
        var row = 0;

        foreach (var item in Enumerable.Reverse(_magicList))
        {
            var container = new GameObject("Container").AddComponent<HorizontalLayoutGroup>();
            container.childAlignment = TextAnchor.MiddleLeft;
            var rect = container.GetComponent<RectTransform>();
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 100f);
            rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 500f);
            container.spacing = 60;


            var cardImage = new GameObject("Card").AddComponent<Image>();
            cardImage.sprite = item.Card;
            cardImage.SetNativeSize();
            cardImage.transform.SetParent(container.transform, false);

            var targetArrow = new GameObject("Arrow").AddComponent<Image>();
            targetArrow.sprite = ArrowImage;
            targetArrow.SetNativeSize();
            targetArrow.transform.SetParent(container.transform, false);

            var targetImage = new GameObject("Target").AddComponent<Image>();
            targetImage.sprite = item.Target;
            targetImage.SetNativeSize();
            targetImage.transform.SetParent(container.transform, false);
            
            container.transform.SetParent(Panel.transform);
            rect.localPosition = new Vector3(StartX, StartY + (row * YSpace));

            row++;
        }
    }

    public void AddItem(Sprite card, Sprite target)
    {
        _magicList.Add( new MagicActionInfo() { Card = card, Target = target});
    }
}

public struct MagicActionInfo
{
    public Sprite Card;
    public Sprite Target;
}
