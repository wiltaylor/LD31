using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MagicSummaryManager : MonoBehaviour
{

    public static MagicSummaryManager Instance;
    public GameObject Panel;

    public float MagicStartX = -218;
    public float MagicStartY = 130;
    public float ArrowStartX = -27;
    public float ArrowStartY = 122;
    public float TargetStartX = 168;
    public float TargetStartY = 130;
    public float YShift = -128;

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

        _canvas.enabled = true;
        var row = 0;

        foreach (var item in _magicList)
        {
            var cardImage = new GameObject("Card").AddComponent<Image>();
            cardImage.sprite = item.Card;
            cardImage.transform.SetParent(Panel.transform, false);
            cardImage.transform.position = new Vector3(MagicStartX, MagicStartY + (YShift * row));

            var targetImage = new GameObject("Target").AddComponent<Image>();
            targetImage.sprite = item.Target;
            targetImage.transform.SetParent(Panel.transform, false);
            targetImage.transform.position = new Vector3(TargetStartX, TargetStartY + (YShift * row));
            

            var targetArrow = new GameObject("Arrow").AddComponent<Image>();
            targetArrow.sprite = ArrowImage;
            targetArrow.transform.SetParent(Panel.transform, false);
            targetArrow.transform.position = new Vector3(ArrowStartX, ArrowStartY + (YShift * row));

            


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
