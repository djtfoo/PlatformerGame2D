/**
 * Created: 6 Aug 2022
 * 
 * Class: MapLoader
 * Generate the map of the level from a text file.
 * 
 * Reference for 2D platformer movement with Rigidbody2D: https://www.youtube.com/watch?v=RWf3mpDaE5g&ab_channel=EmilioBlacksmith
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;

    [SerializeField] ContactFilter2D groundContactFilter;

    private Vector3 localScale;
    private Rigidbody2D rb;

    private float horizontalMovement = 0f;
    private bool justJumped = false;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Process Keys
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && IsOnGround())
        {
            justJumped = true;
        }

        // Flip sprite
        if (horizontalMovement < 0f)
            transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
        else if (horizontalMovement > 0f)
            transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
    }

    private void FixedUpdate()
    {
        // if player started moving, set 'Walk' animation to true
        if (horizontalMovement != 0f)
        {
            if (animator != null) animator.SetBool("Walk", true);
        }
        // else if player stopped moving, set 'Walk' animation to false
        else
        {
            if (animator != null) animator.SetBool("Walk", false);
        }

        // Move
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        // Apply jump
        if (justJumped)
        {
            // Add force upwards to Rigidbody2D
            justJumped = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Play SFX
            AudioManager.instance.PlaySFX("Jump");
        }
    }

    private bool IsOnGround()
    {
        return rb.IsTouching(groundContactFilter);
    }
}
