using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPoints : Effect
{
    [SerializeField] private int pointsGain = 100;

    public override void TriggerEffect(Collision2D col)
    {
        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
            player.IncrementScore(pointsGain);
        else
            Debug.Log("GainPoints Effect Component on " + gameObject.name + " triggered, but did not collide with Player");
    }
}