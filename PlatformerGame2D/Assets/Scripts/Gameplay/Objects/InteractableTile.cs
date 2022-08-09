/**
 * Created: 7 Aug 2022
 * 
 * Class: InteractableTile
 * A derived class of Tile, which triggers a set of assigned Effects if the Player collides with it.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableTile : Tile
{
    [SerializeField] ContactFilter2D hitContactFilter;  // filter contacts to detect collision from below the Tile

    [SerializeField] private Effect[] triggerEffects;

    [SerializeField] private Transform tileSprite;

    [SerializeField] private float yTranslateBy = 0.035f;
    [SerializeField] private float yTranslateSpeed = 0.4f;

    [SerializeField] private int numInteractionsLeft = 1;
    [SerializeField] private UnityEvent onInteractivityEnded;

    /// <summary>
    /// Callback function by Unity's engine when a collision occurs with the GameObject this script is attached to.
    /// </summary>
    /// <param name="col">Collider that this GameObject collided with</param>
    void OnCollisionEnter2D(Collision2D col)
    {
        if (numInteractionsLeft <= 0)   // tile no longer interactable
            return;

        // check if collision is with player
        if (col.gameObject.tag == "Player")
        {
            // check if player collided with the tile from below
            if (this.GetComponent<Collider2D>().IsTouching(col.collider, hitContactFilter))
            {
                // Trigger effect of tile
                TriggerHitEffect(col);
                // Feedback of tile being hit
                OnHitFeedback();

                // reduce interactions
                numInteractionsLeft -= 1;
                if (numInteractionsLeft == 0)
                    onInteractivityEnded.Invoke();
            }
        }
    }

    /// <summary>
    /// Tile effect to trigger, if the Player hits the tile.
    /// </summary>
    /// <param name="col">The Collider that triggered the Effect</param>
    private void TriggerHitEffect(Collision2D col)
    {
        foreach (Effect effect in triggerEffects)
        {
            effect.TriggerEffect(col.collider);
        }
    }

    /// <summary>
    /// Visual and audio feedback upon hitting the Tile.
    /// </summary>
    protected void OnHitFeedback()
    {
        // Shake tile from hit
        StartCoroutine(ShakeTile());

        // Play Hit SFX
        AudioManager.instance.PlaySFX("Hit");
    }

    /// <summary>
    /// Coroutine that shows the visual animation of the tile being shaken.
    /// </summary>
    protected IEnumerator ShakeTile()
    {
        // save the initial position of the sprite
        Vector3 initialPos = tileSprite.position;
        float offsetY = 0f;

        // move the sprite upwards
        while (offsetY < yTranslateBy)
        {
            // calculate the updated offset
            offsetY += yTranslateSpeed * Time.deltaTime;
            if (offsetY > yTranslateBy)
                offsetY = yTranslateBy;

            tileSprite.position = initialPos + new Vector3(0f, offsetY, 0f);    // update the sprite position
            yield return null;
        }
        // move the sprite back downwards to its original position
        while (offsetY > 0f)
        {
            // calculate the updated offset
            offsetY -= yTranslateSpeed * Time.deltaTime;
            if (offsetY < 0f)
                offsetY = 0f;

            tileSprite.position = initialPos + new Vector3(0f, offsetY, 0f);    // update the sprite position
            yield return null;
        }
    }


}
