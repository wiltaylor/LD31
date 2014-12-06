using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ResourceManager : MonoBehaviour
{

    public int Owner = 0;
    public Text FoodText;
    public Text WoodText;
    public Text GoldText;

    public void FixedUpdate()
    {
        if (Owner == 1)
        {
            FoodText.text = ResourceTracker.Instance.PlayerFood.ToString();
            GoldText.text = ResourceTracker.Instance.PlayerGold.ToString();
            WoodText.text = ResourceTracker.Instance.PlayerWood.ToString();

        }

        if (Owner == 2)
        {
            FoodText.text = ResourceTracker.Instance.EnemyFood.ToString();
            GoldText.text = ResourceTracker.Instance.EnemyGold.ToString();
            WoodText.text = ResourceTracker.Instance.EnemyWood.ToString();
        }
    }
}
