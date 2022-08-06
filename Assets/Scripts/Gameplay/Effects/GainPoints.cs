using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPoints : Effect
{
    [SerializeField] private int pointsGain = 100;

    public override void TriggerEffect(Collision2D col)
    {
        Debug.Log("Points gained: " + pointsGain);
    }
}
