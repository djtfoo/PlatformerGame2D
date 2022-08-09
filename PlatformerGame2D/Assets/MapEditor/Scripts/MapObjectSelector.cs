/**
 * Created: 7 Aug 2022
 * 
 * Class: MapObjectSelector
 * Defines information about an Object to for identification and transferring data between different parts of the Map Editor interface.
 */
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

    /// <summary>
    /// Save a provided Object id to this Component.
    /// </summary>
    /// <param name="id">Object id</param>
    public void StoreObjectId(char id)
    {
        this.id = id;
    }
}
