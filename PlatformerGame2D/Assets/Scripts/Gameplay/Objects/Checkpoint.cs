using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Checkpoint : WinGameObject
{
    protected override void CheckWinCondition()
    {
        // Check if Players who have yet to win have crossed this checkpoint
        foreach (PlayerState player in GameStateManager.Instance.Players)
        {
            if (!player.HasWon)
            {
                // Check position
                if (player.transform.position.x >= transform.position.x)
                {
                    // Set Player win
                    player.SetPlayerWon();

                    // Trigger flag animating out of checkpoint
                    TriggerOnWin();
                }
            }
        }
    }

    private void TriggerOnWin()
    {
        GetComponent<Animator>().enabled = true;
    }
}
