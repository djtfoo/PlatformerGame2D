using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    private int numLivesPerPlayer = 3;

    // GameStateManager Singleton
    private static GameStateManager instance = null;
    public static GameStateManager Instance
    {
        get { return instance; }
    }

    [SerializeField] private Player[] players;  // Reference(s) to Player object

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
