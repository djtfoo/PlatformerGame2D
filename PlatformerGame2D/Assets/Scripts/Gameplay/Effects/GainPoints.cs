using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainPoints : Effect
{
    [SerializeField] private int pointsGain = 100;

    [SerializeField] private string sfxName = "GainPoints";

    public override void TriggerEffect(Collider2D col)
    {
        PlayerState player = col.gameObject.GetComponent<PlayerState>();
        if (player != null)
            player.IncrementScore(pointsGain);
        else
            Debug.Log("GainPoints Effect Component on " + gameObject.name + " triggered, but did not collide with Player");

        // Play SFX
        if (!string.IsNullOrEmpty(sfxName))
            AudioManager.instance.PlaySFX(sfxName);
    }
}
