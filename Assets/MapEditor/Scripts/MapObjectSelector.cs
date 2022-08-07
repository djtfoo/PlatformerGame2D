using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObjectSelector : MonoBehaviour
{
    private char id;
    public char Id
    {
        get { return id; }
    }

    public void StoreObjectId(char id)
    {
        this.id = id;
    }
}
