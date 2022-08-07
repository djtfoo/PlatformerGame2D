using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapEditorCursor : MonoBehaviour
{
    [Header("Map Editor Objects")]
    [SerializeField] private Camera camera;
    [SerializeField] private Transform cursorObject;
    [SerializeField] private MapEditor mapEditor;

    [Header("Map Editor Controls")]
    [SerializeField] private float scrollSpeed = 2f;
    [SerializeField] private float panSpeed = 0.5f;

    private bool isPanning = false;
    [SerializeField] private char selectedId = '\0';    // id of selected Object
    private int selectedSizeX = 0;    // sizeX of selected Object
    private int selectedSizeY = 0;    // sizeY of selected Object
    private Vector2Int gridCoord = Vector2Int.zero;
    private Vector2Int prevGridCoord = Vector2Int.zero;
    private bool hasPaintedThisTile = false;

    // Update is called once per frame
    void Update()
    {
        // User controls
        GetUserInput();

        // Detect position of cursor in world space when not panning
        if (isPanning || selectedId == '\0')
        {
            // Disable cursor object
            cursorObject.gameObject.SetActive(false);
        }
        else
        {
            // Determine position of cursor
            Vector3 cameraPoint = Input.mousePosition;
            cameraPoint.z = -camera.transform.position.z;
            Vector3 pos = camera.ScreenToWorldPoint(cameraPoint);

            // Determine which grid the cursor is resting on
            gridCoord = MapGenerator.Instance.GetGrid(pos);
            // If cursor is out of range, disable it
            if (gridCoord.x < 0 || gridCoord.x >= MapGenerator.Instance.MapWidth ||
                gridCoord.y < 0 || gridCoord.y >= MapGenerator.Instance.MapHeight)
            {
                cursorObject.gameObject.SetActive(false);
            }
            else    // cursor is in range
            {
                // Enable cursor object
                cursorObject.gameObject.SetActive(true);

                // Snap cursor object to grid
                cursorObject.position = MapGenerator.Instance.GetGridPosition(gridCoord, selectedSizeX, selectedSizeY);

                // Check for user click to paint tile
                if (!hasPaintedThisTile)
                    PaintTile();
            }

            if (prevGridCoord != gridCoord) // changed grids
                hasPaintedThisTile = false;
            prevGridCoord = gridCoord;
        }
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
            isPanning = true;
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
            isPanning = false;
        }
    }

    private void PaintTile()
    {
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())    // paint tile
        {
            mapEditor.UpdateObjectOccupancy(gridCoord, selectedId);
            hasPaintedThisTile = true;
        }
    }

    public void AssignObjectId(MapObjectSelector data)
    {
        // get object data
        selectedId = data.Id;
        ObjectData objData = MapGenerator.Instance.GetObjectData(selectedId);

        // get size of sprite
        selectedSizeX = objData.sizeX;
        selectedSizeY = objData.sizeY;

        // update sprite
        SpriteRenderer sr = objData.obj.GetComponentInChildren(typeof(SpriteRenderer)) as SpriteRenderer;
        if (sr != null)
            cursorObject.GetComponent<SpriteRenderer>().sprite = sr.sprite;
    }
}
