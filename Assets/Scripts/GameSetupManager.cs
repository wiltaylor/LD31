using System.Linq;
using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class GameSetupManager : MonoBehaviour
{

    public static GameSetupManager Instance;

    public GameObject TownHall;
    public Canvas GameOverScreen;
    public Text GameOverText;

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

    public void Start()
    {
        NewGame();
    }

    public void ShowGameOver(bool playerWon)
    {
        MagicSummaryManager.Instance.Accept();
        GameOverScreen.enabled = true;
        GameOverText.text = playerWon ? "Game Over: You Won!" : "Game Over: You Lost!";
    }

    public void NewGame()
    {
        //Clear up GameOverScreen
        GameOverScreen.enabled = false;

        //Remove all cards
        foreach (var i in GameObject.FindGameObjectsWithTag("Card"))
        {
            i.transform.SetParent(null);
            Destroy(i);
        }

        //Clear hands
        HandManager.Instance.ClearHand();
        AIHandManager.Instance.ClearHand();

        //Create town halls for both players.
        var playerTH = Instantiate(TownHall).GetComponent<CardManager>();
        var enemyTH = Instantiate(TownHall).GetComponent<CardManager>();

        playerTH.Owner = 1;
        playerTH.State = CardState.InPlay;
        playerTH.transform.SetParent(PlayerGlobals.Instance.PlayerResources.transform, false);
        PlayerGlobals.Instance.PlayerResources.SortCards();

        enemyTH.Owner = 2;
        enemyTH.State = CardState.InPlay;
        enemyTH.transform.SetParent(PlayerGlobals.Instance.EnemyResources.transform, false);
        PlayerGlobals.Instance.EnemyResources.SortCards();

        //Setting initial Resources.
        ResourceTracker.Instance.PlayerFood = 1000;
        ResourceTracker.Instance.PlayerGold = 1000;
        ResourceTracker.Instance.PlayerWood = 1000;
        ResourceTracker.Instance.PlayerResources = 1;
        ResourceTracker.Instance.PlayerResourcesPerTurn = 1;

        ResourceTracker.Instance.EnemyFood = 1000;
        ResourceTracker.Instance.EnemyGold = 1000;
        ResourceTracker.Instance.EnemyWood = 1000;
        ResourceTracker.Instance.EnemyResources = 1;
        ResourceTracker.Instance.EnemyResourcesPerTurn = 1;

        //Shuffle Decks.
        foreach (var i in from o in GameObject.FindGameObjectsWithTag("Deck")
            select o.GetComponent<DeckManager>())
        {
            i.BuildDeck();
            i.Draw(HandManager.Instance.MaxHandSize);
        }

        //sort all slots.
        HandManager.Instance.GetComponent<CardSlotManager>().SortCards();
        PlayerGlobals.Instance.PlayerResources.SortCards();
        PlayerGlobals.Instance.PlayerMinions.SortCards();
        PlayerGlobals.Instance.EnemyMinions.SortCards();
        PlayerGlobals.Instance.EnemyResources.SortCards();
    }
}
