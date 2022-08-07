using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : Effect
{
    public override void TriggerEffect(Collider2D col)
    {
        Debug.Log("Player got hurt");

        PlayerState player = col.gameObject.GetComponent<PlayerState>();
        if (player != null)
            player.SetPlayerDead();
        else
            Debug.Log("HurtPlayer Effect Component on " + gameObject.name + " triggered, but did not collide with Player");
    }
}
