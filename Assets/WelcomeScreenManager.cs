using UnityEngine;
using System.Collections;

public class WelcomeScreenManager : MonoBehaviour
{
    public GameObject Tutorial;

    public void ExitWelcome()
    {
        gameObject.SetActive(false);
    }

    public void DoTutorial()
    {
        gameObject.SetActive(false);
        Tutorial.SetActive(true);
    }
}
