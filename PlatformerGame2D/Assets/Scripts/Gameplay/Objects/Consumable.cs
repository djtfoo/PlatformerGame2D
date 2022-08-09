/**
 * Created: 7 Aug 2022
 * 
 * Class: Consumable
 * Stores the game state information of the Player.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Object
{
    [SerializeField] private Effect[] triggerEffects;

    void OnTriggerEnter2D(Collider2D col)
    {
        // check if collision is with player
        if (col.gameObject.tag == "Player")
        {
            // Trigger effect of consumable
            TriggerConsumedEffect(col);
        }
    }

    /// <summary>
    /// Effect to trigger if player picks up the Consumable.
    /// </summary>
    /// <param name="col">The Collider that triggered the Effect</param>
    private void TriggerConsumedEffect(Collider2D col)
    {
        foreach (Effect effect in triggerEffects)
        {
            effect.TriggerEffect(col);
        }
    }
}
