/**
 * Created: 7 Aug 2022
 * 
 * Class: PlayerState
 * Stores the game state information of the Player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(PlayerControl))]
public class PlayerState : MonoBehaviour
{
    private int score = 0;
    public int Score
    {
        get { return score; }
    }
    private int numLives = 3;
    public int NumLives
    {
        get { return numLives; }
    }

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

    private bool hasWon = false;
    public bool HasWon
    {
        get { return hasWon; }
    }

    [Header("Events")]
    [SerializeField] private UnityEvent onScoreChanged;
    [SerializeField] private UnityEvent onLivesChanged;
    [SerializeField] private UnityEvent onPlayerHurt;

    /// <summary>
    /// Called to reset the player at the start of the game.
    /// </summary>
    public void InitPlayer(int startingLives)
    {
        SetNumLives(startingLives);
        ResetPlayer();
    }

    /// <summary>
    /// Called to reset the player at the start of the game or after a reset.
    /// </summary>
    public void ResetPlayer()
    {
        SetScore(0);
        isDead = false;

        // Enable user input and rigidbody
        GetComponent<PlayerControl>().EnableUserInput(true);
        GetComponent<PlayerControl>().EnableRigidbody(true);
    }

    /// <summary>
    /// Method to be called to set the player and game state after they die.
    /// </summary>
    public void SetPlayerDead()
    {
        // Set Player's death
        isDead = true;

        // Lose 1 life
        SetNumLives(numLives - 1);

        // Update GameStateManager
        GameStateManager.Instance.UpdateGameState();

        // Disable user input and rigidbody
        GetComponent<PlayerControl>().EnableUserInput(false);
        GetComponent<PlayerControl>().EnableRigidbody(false);

        // Trigger Hurt event
        onPlayerHurt.Invoke();
    }

    /// <summary>
    /// Method to be called to set the player and game state after they win the game.
    /// </summary>
    public void SetPlayerWon()
    {
        // Set Player win
        hasWon = true;

        // Update GameStateManager
        GameStateManager.Instance.UpdateGameState();

        // Disable user input
        GetComponent<PlayerControl>().EnableUserInput(false);
    }

    /// <summary>
    /// Increment the Player's score.
    /// </summary>
    /// <param name="gain">Amount to increment score by</param>
    public void IncrementScore(int gain)
    {
        Debug.Log("Points gained: " + gain);
        SetScore(score + gain);
    }

    /// <summary>
    /// Set the player's score to a particular value, and invoke the onScoreChanged event to trigger other actions.
    /// </summary>
    /// <param name="newScore">The player's new score</param>
    private void SetScore(int newScore)
    {
        // Set score
        score = newScore;

        // Invoke score changed event
        onScoreChanged.Invoke();
    }

    /// <summary>
    /// Set the player's number of lives to a particular value, and invoke the onLivesChanged event to trigger other actions.
    /// </summary>
    /// <param name="newScore">The player's new score</param>
    private void SetNumLives(int newLives)
    {
        // Set lives
        numLives = newLives;

        // Invoke lives changed event
        onLivesChanged.Invoke();
    }
}
