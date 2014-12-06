using UnityEngine;
using System.Collections;

public class ResourceTracker : MonoBehaviour
{
    public int PlayerFood = 0;
    public int PlayerGold = 0;
    public int PlayerWood = 0;
    public int EnemyFood = 0;
    public int EnemyGold = 0;
    public int EnemyWood = 0;
    public int PlayerResources = 1;
    public int PlayerResourcesPerTurn = 1;
    public int EnemyResources = 1;
    public int EnemyResourcesPerTurn = 1;

    public int TurnOwner = 1;
    public int Turn = 0;

    public static ResourceTracker Instance { get; private set; }

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
