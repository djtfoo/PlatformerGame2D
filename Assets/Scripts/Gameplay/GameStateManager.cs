using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    // GameStateManager Singleton
    private static GameStateManager instance = null;
    public static GameStateManager Instance
    {
        get { return instance; }
    }

    // Game State Variables
    [Header("Game Variables")]
    [SerializeField]
    private int numLivesPerPlayer = 3;

    [Header("Object References")]
    [SerializeField] private Player[] players;  // Reference(s) to Player object

    // Game State
    [Header("Game Initializers")]
    [SerializeField] private MapLoader mapLoader;

    // Timer
    private float gameTimer = 0f;
    public float GameTimer
    {
        get { return gameTimer; }
    }
    private bool runTimer = false;

    [Header("Timer")]
    [SerializeField] private UnityEvent onTimerUpdated;

    protected void Awake()
    {
        if (instance != null)   // duplicate of this instance exists
        {
            Debug.Log("Instance of GameStateManager already exists. Deleting GameStateManager Component in " + gameObject.name);
            Destroy(this);
        }
        else
        {
            // else, assign this as the GameStateManager singleton
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (runTimer)
        {
            // Update game time
            gameTimer += Time.deltaTime;
            // Invoke timer update event
            onTimerUpdated.Invoke();
        }
    }

    /// <summary>
    /// Called to start the game at the start of the game.
    /// </summary>
    private void InitGame()
    {
        // Initialize Player stats
        foreach (Player player in players)
            player.InitPlayer(numLivesPerPlayer);

        // Start the game
        StartGame();
    }

    /// <summary>
    /// Called to start the game after a restart.
    /// </summary>
    public void StartGame()
    {
        // Reset Player stats
        foreach (Player player in players)
            player.ResetPlayer();

        // Re-initialize map and objects
        mapLoader.LoadMap();
        // Reset player position -- should be in mapLoader

        // Start the game timer
        runTimer = true;
    }

    /// <summary>
    /// Called when a Player dies to check for all Player deaths.
    /// </summary>
    public void UpdatePlayerDeaths()
    {
        // Check for player death
        bool playersDead = true;
        foreach (Player player in players)
        {
            if (!player.IsDead)
            {
                playersDead = false;
                break;
            }
        }

        // If all players are dead, restart game
        if (playersDead)
        {
            // Check if the game is over, i.e. all players have 0 lives left
            bool gameOver = true;
            foreach (Player player in players)
            {
                if (player.NumLives > 0)
                {
                    gameOver = false;
                    break;
                }
            }

            // if game over, trigger 'game over' screen
            if (gameOver)
            {
                Debug.Log("Game Over");

                runTimer = false;
                // Show 'game over' screen

            }
            // else, restart game
            else
            {
                StartCoroutine(RestartGame(1f));    // Wait 1 second before restarting game
            }
        }
    }

    private IEnumerator RestartGame(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        StartGame();
    }
}
