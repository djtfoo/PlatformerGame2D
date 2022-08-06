using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Consumable : Object
{
    [SerializeField] private Effect[] triggerEffects;

    void OnCollisionEnter2D(Collision2D col)
    {
        // check if collision is with player
        if (col.gameObject.tag == "Player")
        {
            // Trigger effect of consumable
            TriggerConsumedEffect(col);
            // Feedback of consumable being picked up
            OnConsumedFeedback();
        }
    }

    // Effect to trigger if player picks up the consumable
    private void TriggerConsumedEffect(Collision2D col)
    {
        foreach (Effect effect in triggerEffects)
        {
            effect.TriggerEffect(col);
        }
    }

    private void OnConsumedFeedback()
    {

    }
}
