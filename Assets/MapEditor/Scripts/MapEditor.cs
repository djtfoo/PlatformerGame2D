using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapEditor : MonoBehaviour
{
    [Tooltip("Width in no. of grids for creating new maps")]
    [SerializeField] private int mapWidth;
    [Tooltip("Height in no. of grids, leave this fixed")]
    [SerializeField] private int mapHeight = 16;


    [Tooltip("Assign a map to edit it, leave it empty to create new map")]
    [SerializeField] private TextAsset mapDataFile;

    [Header("Map Editor Scene Objects")]
    [SerializeField] private Camera camera;

    [Header("Map Editor Interface")]
    [SerializeField] private Transform selectionBox;
    [SerializeField] private MapObjectSelector selectionItem;

    [Header("Map Editor Controls")]
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float panSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        char[,] mapData;
        if (mapDataFile != null)
        {
            // Obtain map data from CSV
            mapData = MapParser.ParseMapData(mapDataFile.text);
        }
        else
        {
            mapData = new char[mapHeight, mapWidth];
            for (int i = 0; i < mapHeight; i++)
                for (int j = 0; j < mapWidth; j++)
                    mapData[i, j] = '0';

        }

        // Generate map data
        MapGenerator.Instance.GenerateMap(mapData, transform, true);

        // Generate object selection list
        foreach (ObjectData data in MapGenerator.Instance.GlobalMapDataSO.objectData)
        {
            MapObjectSelector item = Instantiate(selectionItem, selectionBox);
            item.gameObject.SetActive(true);
            // get sprite
            SpriteRenderer sr = data.obj.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
            if (sr != null)
                item.GetComponent<Image>().sprite = sr.sprite;

            selectionItem.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // User controls
        GetUserInput();
    }

    private void GetUserInput()
    {
        // Zoom in/out scroll
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        camera.orthographicSize = Mathf.Clamp(5, camera.orthographicSize - scrollSpeed * scroll, 8);

        // Pan
        if (Input.GetMouseButtonDown(2))    // start pan
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetMouseButton(2))  // panning
        {
            float h = Input.GetAxis("Mouse X");
            float v = Input.GetAxis("Mouse Y");

            // translate camera by this amount
            camera.transform.position += panSpeed * new Vector3(-h, -v, 0f);
        }
        if (Input.GetMouseButtonUp(2))    // stop pan
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
