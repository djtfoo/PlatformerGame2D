using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : Effect
{
    public override void TriggerEffect(Collision2D col)
    {
        Debug.Log("Player got hurt");

        Player player = col.gameObject.GetComponent<Player>();
        if (player != null)
            player.SetPlayerDead();
        else
            Debug.Log("HurtPlayer Effect Component on " + gameObject.name + " triggered, but did not collide with Player");
    }
}
