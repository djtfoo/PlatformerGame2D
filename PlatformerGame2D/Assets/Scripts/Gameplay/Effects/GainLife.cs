/**
 * Created: 6 Aug 2022
 * 
 * Class: GainLife
 * An Effect to increase the life of a Player when triggered.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainLife : Effect
{
    [SerializeField] private int lifeGain = 1;

    public override void TriggerEffect(Collider2D col)
    {

    }
}
