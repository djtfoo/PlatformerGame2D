/**
 * Created: 7 Aug 2022
 * 
 * Class: SpawnAnimatedObject
 * Spawns a GameObject with an Animator component, and allows the object to be destroyed after the animation completes.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAnimatedObject : MonoBehaviour
{
    [SerializeField] private bool destroyOnAnimComplete = true;
    [SerializeField] private float delay = 0f;

    [SerializeField] private Animator animatedObjectPrefab;

    [SerializeField] private Vector3 offsetSpawnPos = Vector3.zero;

    /// <summary>
    /// Function to call to spawn a GameObject with an Animator.
    /// </summary>
    public void SpawnObject()
    {
        // Instantiate the object
        Animator spawn = Instantiate(animatedObjectPrefab);
        // Place the object at (position of spawn point + offset)
        spawn.transform.position = transform.position + offsetSpawnPos;

        // Workaround: wait 1 frame for animation to start playing to set its destruction
        if (destroyOnAnimComplete)
            StartCoroutine(SetToDestroy(spawn));
    }

    /// <summary>
    /// Coroutine method that will destroy the GameObject at the end of the Animation Clip.
    /// </summary>
    /// <param name="anim">The Animator object</param>
    private IEnumerator SetToDestroy(Animator anim)
    {
        // Wait 1 frame
        yield return null;

        // Set object to be destroyed at the end of the animation
        Destroy(anim.gameObject, anim.GetCurrentAnimatorStateInfo(0).length + delay);
    }
}