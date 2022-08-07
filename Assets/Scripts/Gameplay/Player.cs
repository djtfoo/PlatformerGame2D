using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
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

    [Header("Events")]
    [SerializeField] private UnityEvent onScoreChanged;
    [SerializeField] private UnityEvent onLivesChanged;

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
    }

    public void SetPlayerDead()
    {
        // Set Player's death
        isDead = true;

        // Lose 1 life
        SetNumLives(numLives - 1);

        // Update GameStateManager
        GameStateManager.Instance.UpdatePlayerDeaths();
    }

    public void IncrementScore(int gain)
    {
        Debug.Log("Points gained: " + gain);
        SetScore(score + gain);
    }

    private void SetScore(int newScore)
    {
        // Set score
        score = newScore;

        // Invoke score changed event
        onScoreChanged.Invoke();
    }

    private void SetNumLives(int newLives)
    {
        // Set lives
        numLives = newLives;

        // Invoke lives changed event
        onLivesChanged.Invoke();
    }
}
