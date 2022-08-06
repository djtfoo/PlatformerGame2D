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

    [SerializeField] private UnityEvent onScoreChanged;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Called to reset the player at the start of the game or after a reset.
    /// </summary>
    public void ResetPlayer()
    {
        score = 0;
        isDead = false;
    }

    public void SetPlayerDead()
    {
        // Set Player's death
        isDead = true;

        // Disable Player Controls
        this.GetComponent<PlayerControl>().enabled = false;

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
}
