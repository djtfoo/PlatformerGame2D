using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    [Tooltip("Width in no. of grids for creating new maps")]
    [Range(1, 100)]
    [SerializeField] private int mapWidth = 20;

    // Global Map Data
    [SerializeField] private GlobalMapData globalMapDataSO;

    [Header("Create New Map File")]
    [Tooltip("Assign a map to edit it, leave it empty to create new map")]
    [SerializeField] private string mapFilePath = "/Maps/test.txt";

    [Header("Map Editor Interface")]
    [SerializeField] private Transform selectionBox;
    [SerializeField] private MapObjectSelector selectionItem;

    // Storing of occupancy grid
    private char[,] mapData;
    private Dictionary<Vector2Int, GameObject> objectOccupancy;

    // Start is called before the first frame update
    void Start()
    {
        objectOccupancy = new Dictionary<Vector2Int, GameObject>();

        try
        {
            // Obtain map data from text file
            string filepath = Application.dataPath + "/StreamingAssets/" + mapFilePath;
            string data = File.ReadAllText(filepath);

            mapData = MapParser.ParseMapData(data);
        }
        catch (Exception e)
        {
            Debug.LogWarning(mapFilePath + " is invalid.");

            // create empty map
            int mapHeight = globalMapDataSO.mapHeight;
            mapData = new char[mapHeight, mapWidth];
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    mapData[i, j] = '0';
        }
        finally
        {
            // Generate map data
            MapGenerator.Instance.GenerateMap(mapData, transform, true, objectOccupancy);

            // Generate object selection list
            foreach (ObjectData data in MapGenerator.Instance.GlobalMapDataSO.objectData)
            {
                // Create selection item
                MapObjectSelector item = Instantiate(selectionItem, selectionBox);
                item.gameObject.SetActive(true);
                // get sprite
                SpriteRenderer sr = data.obj.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
                if (sr != null)
                    item.GetComponent<Image>().sprite = sr.sprite;

                // make a reference to object data
                item.StoreObjectId(data.id);
            }
            // disable the default selection item
            selectionItem.gameObject.SetActive(false);
        }
    }

    public void UpdateObjectOccupancy(Vector2Int gridCoord, char id)
    {
        // If object already exists, destroy it
        if (objectOccupancy.ContainsKey(gridCoord))
            Destroy(objectOccupancy[gridCoord]);

        // Create instance of object
        MapGenerator.Instance.InstantiateObject(id, transform, gridCoord, true, objectOccupancy);

        // Update map data
        mapData[gridCoord.y, gridCoord.x] = id;
    }

    public void SaveMap()
    {
        // Serialize mapData as text data
        string serializedData = MapParser.SerializeMapData(mapData);

        // Write to map file
        string filepath = Application.dataPath + "/StreamingAssets/" + mapFilePath;
        File.WriteAllText(filepath, serializedData);

        Debug.Log("Saved successfully.");
    }
}
