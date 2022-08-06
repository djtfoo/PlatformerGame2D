using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : Effect
{
    [SerializeField] private GameObject thisObject;

    protected void Awake()
    {
        if (thisObject == null)
            thisObject = this.gameObject;
    }

    public override void TriggerEffect(Collision2D col)
    {
        Destroy(thisObject);
    }
}
