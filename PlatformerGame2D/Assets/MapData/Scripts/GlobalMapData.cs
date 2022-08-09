/**
 * Created: 7 Aug 2022
 * 
 * File: GlobalMapData.cs
 * GlobalMapData is a ScriptableObject that holds information about the game map shared between the game and editor.
 * ObjectData represents information about the types of Objects in the game.
 */
using UnityEngine;

[System.Serializable]
public class ObjectData
{
    // Name
    public char id;
    // Object
    public GameObject obj;
    // Size (no. grids)
    public int sizeX;
    public int sizeY;
}

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GlobalMapDataScriptableObject", order = 1)]
public class GlobalMapData : ScriptableObject
{
    public float gridXSize = 0.96f;
    public float gridYSize = 0.96f;
    public int mapHeight = 16;
    public ObjectData[] objectData;
}
