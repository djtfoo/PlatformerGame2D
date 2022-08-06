using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int score = 0;
    private int numLives = 3;

    private bool isDead = false;
    public bool IsDead
    {
        get { return isDead; }
    }

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
        score += gain;
        Debug.Log("Points gained: " + gain);
    }
}
