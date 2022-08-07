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
