using UnityEngine;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

public class HandManager : MonoBehaviour
{
    public static HandManager Instance { get; private set; }

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
