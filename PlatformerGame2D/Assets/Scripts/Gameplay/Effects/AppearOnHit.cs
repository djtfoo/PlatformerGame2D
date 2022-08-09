/**
 * Created: 6 Aug 2022
 * 
 * Class: AppearOnHit
 * An Effect to enable a SpriteRenderer when triggered.
 */
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
