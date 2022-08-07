using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableTile : Tile
{
    [SerializeField] ContactFilter2D hitContactFilter;  // filter for contacts with tile from below

    [SerializeField] private Effect[] triggerEffects;

    [SerializeField] private Transform tileSprite;

    [SerializeField] private float yTranslateBy = 0.035f;
    [SerializeField] private float yTranslateSpeed = 0.4f;

    void OnCollisionEnter2D(Collision2D col)
    {
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
            }
        }

    }

    // Tile effect to trigger if player hits the tile
    private void TriggerHitEffect(Collision2D col)
    {
        foreach (Effect effect in triggerEffects)
        {
            effect.TriggerEffect(col.collider);
        }
    }

    protected void OnHitFeedback()
    {
        // Shake tile from hit
        StartCoroutine(ShakeTile());

        // Play Hit SFX
        AudioManager.instance.PlaySFX("Hit");
    }

    protected IEnumerator ShakeTile()
    {
        Vector3 initialPos = tileSprite.position;
        float offsetY = 0f;

        while (offsetY < yTranslateBy)
        {
            offsetY += yTranslateSpeed * Time.deltaTime;
            if (offsetY > yTranslateBy)
                offsetY = yTranslateBy;

            tileSprite.position = initialPos + new Vector3(0f, offsetY, 0f);
            yield return null;
        }
        while (offsetY > 0f)
        {
            offsetY -= yTranslateSpeed * Time.deltaTime;
            if (offsetY < 0f)
                offsetY = 0f;

            tileSprite.position = initialPos + new Vector3(0f, offsetY, 0f);
            yield return null;
        }
    }


}
