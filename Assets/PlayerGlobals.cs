using UnityEngine;
using System.Collections;

public class PlayerGlobals : MonoBehaviour {

    public static PlayerGlobals Instance { get; private set; }
    public CardSlotManager PlayerResources;
    public CardSlotManager PlayerMinions;
    public CardSlotManager EnemyResources;
    public CardSlotManager EnemyMinions;

    public int GoldCostPerDraw = 10;

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
}
