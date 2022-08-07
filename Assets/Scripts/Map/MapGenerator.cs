using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    // MapGenerator Singleton
    private static MapGenerator instance = null;
    public static MapGenerator Instance
    {
        get { return instance; }
    }

    // Object Pool
    [SerializeField] private GlobalMapData globalMapDataSO;
    public GlobalMapData GlobalMapDataSO
    {
        get { return globalMapDataSO; }
    }
    private Dictionary<char, GameObject> objectPool;

    protected void Awake()
    {
        if (instance != null)   // duplicate of this instance exists
        {
            Debug.Log("Instance of MapGenerator already exists. Deleting MapGenerator Component in " + gameObject.name);
            Destroy(this);
        }
        else
        {
            // else, assign this as the MapGenerator singleton
            instance = this;
        }

        objectPool = new Dictionary<char, GameObject>();
        foreach (ObjectData data in globalMapDataSO.objectData)
        {
            objectPool.Add(data.id, data.obj);
        }
    }


    public void GenerateMap(char[,] mapData, Transform parent, bool isEditor)
    {
        // Generate map tiles
        for (int i = 0; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            for (int j = 0; j < mapData.GetLength(1); ++j)  // columns, horizontal axis
            {
                char tileId = mapData[i, j];
                if (isEditor || tileId != '0')  // if in editor, draw empty tiles too, else only draw if it is not an empty tile
                {
                    // set as a child of this object
                    GameObject newTile = Instantiate(objectPool[tileId], parent);
                    newTile.transform.position = new Vector3(j * globalMapDataSO.gridXSize, -i * globalMapDataSO.gridYSize);
                }
            }
        }

        // Generate border around map
        GameObject borderTilePrefab = objectPool['B'];
        // horizontal top row
        for (int j = 0; j < mapData.GetLength(1); ++j)  // columns, horizontal axis
        {
            GameObject newTile = Instantiate(borderTilePrefab, parent);
            newTile.transform.position = new Vector3(j * globalMapDataSO.gridXSize, globalMapDataSO.gridYSize);
        }
        // vertical left column
        for (int i = -1; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            GameObject newTile = Instantiate(borderTilePrefab, parent);
            newTile.transform.position = new Vector3(-globalMapDataSO.gridXSize, -i * globalMapDataSO.gridYSize);
        }
        // vertical right column
        for (int i = -1; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            GameObject newTile = Instantiate(borderTilePrefab, parent);
            newTile.transform.position = new Vector3(mapData.GetLength(1) * globalMapDataSO.gridXSize, -i * globalMapDataSO.gridYSize);
        }
    }
}
