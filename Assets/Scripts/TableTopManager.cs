using UnityEngine;
using System.Collections;

public class TableTopManager : MonoBehaviour 
{
    public static TableTopManager Instance { get; private set; }

    public GameObject CurrentlyOver;
    public GameObject DragDropHolder;

    // Use this for initialization
    void Awake()
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
}
