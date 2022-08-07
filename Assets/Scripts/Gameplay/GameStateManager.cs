using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
    [Range(1, 99)]
    [SerializeField]
    private int numLivesPerPlayer = 3;

    [Header("Object References")]
    [SerializeField] private PlayerState[] players;  // Reference(s) to Player object
    public PlayerState[] Players
    {
        get { return players; }
    }

    // Game State
    [Header("Game Initializers")]
    [SerializeField] private MapLoader mapLoader;

    [Header("Game Screens")]
    [SerializeField] private GameObject gameOverScreen;

    // Timer
    private double gameTimer = 0f;
    public double GameTimer
    {
        get { return gameTimer; }
    }
    private bool runTimer = false;
    private bool waitForRestart = true;

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

        // Disable game over screen
        gameOverScreen.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitGame();
    }

    // Update is called once per frame
    void Update()
    {
        // Check for 'restart' button
        if (waitForRestart)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Reload scene
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Update is called once per frame, after Update
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
        foreach (PlayerState player in players)
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
        foreach (PlayerState player in players)
            player.ResetPlayer();

        // Re-initialize map and objects
        mapLoader.LoadMap();
        // Reset player position -- should be in mapLoader

        // Start the game timer
        runTimer = true;
    }

    /// <summary>
    /// Called when a Player dies or wins to check for all Player deaths.
    /// </summary>
    public void UpdateGameState()
    {
        /// TODO: specific logic TBD, as from a game design perspective not sure if all players would be required to win (i.e. any deaths => restart game)
        /// Currently, logic is: if 1 player has won, the level is won
        /// Game is lost only if all players have died

        // Check if a player has won
        bool playerWon = false;
        foreach (PlayerState player in players)
        {
            if (player.HasWon)
            {
                playerWon = true;
                break;
            }
        }

        if (playerWon)
        {
            // Show game win screen
            Debug.Log("You Won!");

            return;
        }

        // Otherwise, check for player death
        bool playersDead = true;
        foreach (PlayerState player in players)
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
            Debug.Log("All Players Died");

            // Check if the game is over, i.e. all players have 0 lives left
            bool gameOver = true;
            foreach (PlayerState player in players)
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
                // Show 'game over' screen
                gameOverScreen.SetActive(true);

                // Stop timer
                runTimer = false;

                // Set waiting for user input to restart
                waitForRestart = true;
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
