using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnHit : Effect
{
    [SerializeField] private SpriteRenderer thisSprite = null;

    protected void Awake()
    {
        if (thisSprite != null)
            thisSprite.enabled = false;
    }

    public override void TriggerEffect(Collider2D col)
    {
        if (thisSprite != null)
            thisSprite.enabled = true;
    }
}
