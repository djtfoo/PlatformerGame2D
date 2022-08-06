/**
 * Created: 6 Aug 2022
 * 
 * Class: CameraController
 * Class for camera to track the player character's horizontal movement.
 * Tracks only in a forward direction (player is not allowed to move "backwards").
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Transform objectToTrack;

    private float currXPos;

    private float maxXPos;  // stop tracking once this point is reached

    // Start is called before the first frame update
    void Start()
    {
        currXPos = objectToTrack.position.x;
        // TODO: update maxXPos limit
        maxXPos = 1000f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // if the tracked object has moved forward
        if (objectToTrack.position.x > currXPos && objectToTrack.position.x < maxXPos)
        {
            // update this object's position
            transform.position += new Vector3(objectToTrack.position.x - currXPos, 0f, 0f);
            currXPos = objectToTrack.position.x;
        }
    }
}
