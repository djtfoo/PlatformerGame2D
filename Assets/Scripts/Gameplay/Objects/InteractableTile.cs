using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class InteractableTile : Tile
{
    [SerializeField] ContactFilter2D hitContactFilter;  // filter for contacts with tile from below

    [SerializeField] private Effect[] triggerEffects;

    [SerializeField] private Transform tileSprite;

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
            effect.TriggerEffect(col);
        }
    }

    protected void OnHitFeedback()
    {
        // Shake tile from hit
        StartCoroutine(ShakeTile());
    }

    protected IEnumerator ShakeTile()
    {
        float yTranslateBy = 0.015f;
        float yTranslateSpeed = 0.25f;
        while (tileSprite.localPosition.y < yTranslateBy)
        {
            tileSprite.localPosition += new Vector3(0f, yTranslateSpeed * Time.deltaTime, 0f);
            if (tileSprite.localPosition.y > yTranslateBy)
                tileSprite.localPosition = new Vector3(tileSprite.localPosition.x, yTranslateBy, 0f);
            yield return null;
        }
        while (tileSprite.localPosition.y > 0f)
        {
            tileSprite.localPosition -= new Vector3(0f, yTranslateSpeed * Time.deltaTime, 0f);
            if (tileSprite.localPosition.y < 0f)
                tileSprite.localPosition = new Vector3(tileSprite.localPosition.x, 0f, 0f);
            yield return null;
        }
    }


}
