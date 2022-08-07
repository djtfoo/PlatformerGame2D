/**
 * Created: 6 Aug 2022
 * 
 * Class: MapLoader
 * Generate the map of the level from a text file.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [Header("Grid")]
    [SerializeField] private float gridXSize;
    [SerializeField] private float gridYSize;

    [Header("Map Assets")]
    // temp variable for testing
    [SerializeField] private TextAsset tempLevelData;
    [SerializeField] private GameObject[] tilePalette;

    [Header("Other Map Objects")]
    [SerializeField] private GameObject floor;

    [Header("Gameplay Objects")]
    [SerializeField] private Player player;
    [SerializeField] private CameraController camera;

    private Vector3 playerSpawnPos;
    private Vector3 cameraInitPos;

    // Other Variables
    private bool firstTime; // whether it is the first time a map is being loaded

    void Awake()
    {
        // TEMP: Player spawn position is the position of the Player in the scene
        //  (Should be a spawn point set in the level instead)
        playerSpawnPos = player.transform.position;

        // Get camera's initial position
        // (Should be a point set dependent on the player's spawn point)
        cameraInitPos = camera.transform.position;
    }

    public void LoadMap()
    {
        // if not the first time loading the map, destroy entities
        if (!firstTime)
        {
            // Destroy the existing map first
            Debug.Log("Destroy existing map first");
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        // Generate the map
        GenerateMap(tempLevelData.text);

        // Set Player at player spawn position
        player.transform.position = playerSpawnPos;

        // Set Camera at initial position
        camera.transform.position = cameraInitPos;

        // No longer the first time
        firstTime = false;
    }

    /// <summary>
    /// Generate map data from text file.
    /// </summary>
    private void GenerateMap(string textData)
    {
        // Obtain map data from CSV
        char[,] mapData = MapParser.ParseMapData(textData);

        // Generate map tiles
        for (int i = 0; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            for (int j = 0; j < mapData.GetLength(1); ++j)  // columns, horizontal axis
            {
                // TEMP
                if (mapData[i, j] != '0')
                {
                    // set as a child of this object
                    GameObject newTile = Instantiate(tilePalette[0], transform);
                    newTile.transform.position = new Vector3(j * gridXSize, -i * gridYSize);
                }
            }
        }

        // Generate border around map
        // horizontal top row
        for (int j = 0; j < mapData.GetLength(1); ++j)  // columns, horizontal axis
        {
            // TEMP
            GameObject newTile = Instantiate(tilePalette[1], transform);
            newTile.transform.position = new Vector3(j * gridXSize, gridYSize);
        }
        // vertical left column
        for (int i = -1; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            // TEMP
            GameObject newTile = Instantiate(tilePalette[1], transform);
            newTile.transform.position = new Vector3(-gridXSize, -i * gridYSize);
        }
        // vertical right column
        for (int i = -1; i < mapData.GetLength(0); ++i)  // rows, vertical axis
        {
            // TEMP
            GameObject newTile = Instantiate(tilePalette[1], transform);
            newTile.transform.position = new Vector3(mapData.GetLength(1) * gridXSize, -i * gridYSize);
        }

        // Create floor detection beneath ground for player death
        // expand floor by no. tiles in row + additional buffer space (in case player falls outside area of the map)
        floor.transform.localScale = new Vector3(floor.transform.localScale.x * 2 * mapData.GetLength(1), floor.transform.localScale.y, floor.transform.localScale.z);
        // place coordinate floor at the middle of the map
        floor.transform.position += new Vector3(0.5f * gridXSize * mapData.GetLength(1), 0f, 0f);
    }
}
