/**
 * Created: 6 Aug 2022
 * 
 * Class: DestroyObject
 * An Effect to destroy a GameObject when triggered.
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : Effect
{
    [SerializeField] private GameObject thisObject;
    [Tooltip("Name of SFX to play upon object destroy")]
    [SerializeField] private string sfxNameOnDestroy = "";

    protected void Awake()
    {
        if (thisObject == null)
            thisObject = this.gameObject;
    }

    public override void TriggerEffect(Collider2D col)
    {
        // Destroy object
        Destroy(thisObject);

        // Play SFX
        if (!string.IsNullOrEmpty(sfxNameOnDestroy))
        {
            AudioManager.instance.PlaySFX(sfxNameOnDestroy);
        }
    }
}
