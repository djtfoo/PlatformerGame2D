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

    // Timer
    private float gameTimer = 0f;
    public float GameTimer
    {
        get { return gameTimer; }
    }
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
        StartGame();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update game time
        gameTimer += Time.deltaTime;
        // Invoke timer update event
        onTimerUpdated.Invoke();
    }

    /// <summary>
    /// Called to start the game at the start of the game or after a reset.
    /// </summary>
    public void StartGame()
    {
        foreach (Player player in players)
            player.ResetPlayer();
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

        // If all players are dead, restart game with lives lost
        if (playersDead)
        {
            // TODO: Trigger 'game over' screen

            // TODO: Reset game
        }
    }
}
