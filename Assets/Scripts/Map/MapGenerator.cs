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

    // Global Map Data
    [SerializeField] private GlobalMapData globalMapDataSO;
    public GlobalMapData GlobalMapDataSO
    {
        get { return globalMapDataSO; }
    }
    private Dictionary<char, ObjectData> objectPool;    // map between object id and object prefab

    private int mapWidth = 0;   // in no. grids
    public int MapWidth
    {
        get { return mapWidth; }
    }
    private int mapHeight = 0;  // in no. grids
    public int MapHeight
    {
        get { return mapHeight; }
    }

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

        objectPool = new Dictionary<char, ObjectData>();
        foreach (ObjectData data in globalMapDataSO.objectData)
        {
            objectPool.Add(data.id, data);
        }
        mapHeight = globalMapDataSO.mapHeight;
    }

    public ObjectData GetObjectData(char id)
    {
        if (objectPool.ContainsKey(id))
            return objectPool[id];

        return null;
    }

    public void InstantiateObject(char id, Transform parent, Vector2Int coord, bool isEditor, Dictionary<Vector2Int, GameObject> objectGrid)
    {
        GameObject newTile = Instantiate(objectPool[id].obj, parent);
        newTile.transform.position = GetGridPosition(coord, objectPool[id].sizeX, objectPool[id].sizeY);

        if (isEditor)
        {
            if (!objectGrid.ContainsKey(coord))
                objectGrid.Add(coord, newTile);
            else
                objectGrid[coord] = newTile;
        }
    }

    public void GenerateMap(char[,] mapData, Transform parent, bool isEditor, Dictionary<Vector2Int, GameObject> objectGrid = null)
    {
        mapWidth = mapData.GetLength(1);

        // Generate map tiles
        for (int i = 0; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            for (int j = 0; j < mapData.GetLength(1); ++j)  // columns, horizontal axis
            {
                char tileId = mapData[i, j];
                if (isEditor || tileId != '0')  // if in editor, draw empty tiles too, else only draw if it is not an empty tile
                {
                    // set as a child of this object
                    InstantiateObject(tileId, parent, new Vector2Int(j, i), isEditor, objectGrid);
                }
            }
        }

        // Generate border around map
        GameObject borderTilePrefab = objectPool['B'].obj;
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

    public Vector2Int GetGrid(Vector3 pos)
    {
        Vector2Int gridPos = Vector2Int.zero;
        gridPos.x = (int)((pos.x + 0.5f * globalMapDataSO.gridXSize) / globalMapDataSO.gridXSize);
        gridPos.y = (int)((-pos.y + 0.5f * globalMapDataSO.gridYSize) / globalMapDataSO.gridYSize);

        return gridPos;
    }

    public Vector3 GetGridPosition(Vector2Int gridPos, int sizeX, int sizeY)
    {
        Vector3 pos = Vector3.zero;
        // get position of 1x1 grid
        pos.x = gridPos.x * globalMapDataSO.gridXSize;
        pos.y = -gridPos.y * globalMapDataSO.gridYSize;

        // get offset based on size
        pos.x += (sizeX - 1) * 0.5f * globalMapDataSO.gridXSize;
        pos.y += (sizeY - 1) * 0.5f * globalMapDataSO.gridYSize;

        return pos;
    }
}
