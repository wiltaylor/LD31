using System.ComponentModel;
using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public static SoundManager Instance;
    public AudioSource Music;
    public AudioSource CardRemoveSFX;
    public AudioSource Lose;
    public AudioSource Win;

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

    public void ToggleMusic()
    {
        if (Music.isPlaying)
        {
            Music.Stop();
        }
        else
        {
            Music.Play();
        }
    }

    public void RemoveCard()
    {
        CardRemoveSFX.Play();
    }

    public void WinSound()
    {
        Win.Play();
    }

    public void LoseSound()
    {
        Lose.Play();
    }

}
