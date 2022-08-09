/**
 * Created: 6 Aug 2022
 * 
 * Class: Effect
 * Abstract class for defining behaviours that can be called upon during a collision.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    /// <summary>
    /// Callback function by Unity's engine when a collision occurs with the GameObject this script is attached to.
    /// </summary>
    /// <param name="col">Collider that this object collided with</param>
    public abstract void TriggerEffect(Collider2D col);
}
