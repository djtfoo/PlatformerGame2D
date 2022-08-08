/**
 * Created: 6 Aug 2022
 * 
 * Class: MapLoader
 * Generate the map of the level from a text file.
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [Header("Map Data")]
    [SerializeField] private string assetToLoad = "/Maps/ToLoad.txt";

    [Header("Other Map Objects")]
    [SerializeField] private GameObject floor;

    [Header("Gameplay Objects")]
    [SerializeField] private PlayerState player;
    [SerializeField] private CameraController camera;

    private Vector3 playerSpawnPos;
    private Vector3 cameraInitPos;

    // Other Variables
    private bool firstTime = true; // whether it is the first time a map is being loaded

    private string mapData;

    void Awake()
    {
        // TEMP: Player spawn position is the position of the Player in the scene
        //  (Should be a spawn point set in the level instead)
        playerSpawnPos = player.transform.position;

        // Get camera's initial position
        // (Should be a point set dependent on the player's spawn point)
        cameraInitPos = camera.transform.position;

        // Load TextAsset from Resources folder
        string pathFile = Application.dataPath + "/StreamingAssets/" + assetToLoad;
        Debug.Log(pathFile);
        if (File.Exists(pathFile))
        {
            string fileToLoad = File.ReadAllText(pathFile);
            Debug.Log(fileToLoad);
            string pathFileToLoad = Application.dataPath + "/StreamingAssets/" + fileToLoad;
            mapData = File.ReadAllText(pathFileToLoad);
        }
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
        GenerateMap(mapData);

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
        // Obtain map data from text file
        char[,] mapData = MapParser.ParseMapData(textData);

        MapGenerator.Instance.GenerateMap(mapData, transform, false);

        // Create floor detection beneath ground for player death
        // expand floor by no. tiles in row + additional buffer space (in case player falls outside area of the map)
        floor.transform.localScale = new Vector3(floor.transform.localScale.x * 2 * mapData.GetLength(1), floor.transform.localScale.y, floor.transform.localScale.z);
        // place coordinate floor at the middle of the map
        floor.transform.position += new Vector3(0.5f * MapGenerator.Instance.GlobalMapDataSO.gridXSize * mapData.GetLength(1), 0f, 0f);
    }
}
