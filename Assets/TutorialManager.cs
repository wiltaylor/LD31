using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public Sprite[] TutorialPages;
    public Image TutorialImageBox;

    private int _page = 0;

    public void Start()
    {
        TutorialImageBox.sprite = TutorialPages[_page];
    }

    public void OnBackClicked()
    {
        _page--;

        if (_page <= 0)
            _page = 0;
        else
            _page--;

        TutorialImageBox.sprite = TutorialPages[_page];
    }

    public void OnNextClicked()
    {
        _page++;

        if (_page >= TutorialPages.Length)
            _page = TutorialPages.Length - 1;

        TutorialImageBox.sprite = TutorialPages[_page];
    }

    public void OnExitTutorialClicked()
    {
        gameObject.SetActive(false);
    }

}
