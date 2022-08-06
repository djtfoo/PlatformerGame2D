using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GainLife : Effect
{
    [SerializeField] private int lifeGain = 1;

    public override void TriggerEffect(Collision2D col)
    {

    }
}
