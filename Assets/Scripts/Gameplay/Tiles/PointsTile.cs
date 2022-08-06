using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsTile : InteractableTile
{
    [SerializeField] private int numberHitsAllowed = 1;

    private bool hitsAllowed = true;

    protected override void TriggerHitEffect()
    {
        if (hitsAllowed)
        {
            Debug.Log("Points obtained!");
            numberHitsAllowed -= 1;
            if (numberHitsAllowed == 0)
                hitsAllowed = false;
        }

    }
}
