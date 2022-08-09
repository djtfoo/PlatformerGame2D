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
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoBehaviour
{
    [Header("Control Variables")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;

    [Header("Contact Filters")]
    [SerializeField] ContactFilter2D groundContactFilter;

    [Header("Events")]
    [SerializeField] private UnityEvent onJump;

    private Vector3 localScale;
    private Rigidbody2D rb;
    private Animator animator;

    private float horizontalMovement = 0f;
    private float prevHorizontalMovement = 0f;
    private bool justJumped = false;

    private bool userInput = true;


    public void EnableUserInput(bool enabled)
    {
        userInput = enabled;
    }

    public void EnableRigidbody(bool enabled)
    {
        rb.simulated = enabled;
    }

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = transform.localScale;

        animator = GetComponent<Animator>();
    }

    private void CheckForUserInput()
    {
        // Process Keys
        horizontalMovement = Input.GetAxisRaw("Horizontal");

        if (Input.GetButtonDown("Jump") && IsOnGround())
        {
            justJumped = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = 0f;
        if (userInput)
            CheckForUserInput();

        // Check for change in walk state if change in Input occurs
        if (prevHorizontalMovement != horizontalMovement)
        {
            // Flip sprite
            if (horizontalMovement < 0f)
                transform.localScale = new Vector3(-localScale.x, localScale.y, localScale.z);
            else if (horizontalMovement > 0f)
                transform.localScale = new Vector3(localScale.x, localScale.y, localScale.z);
        }

        // if player is walking on the ground, set 'Walk' animation to true
        if (horizontalMovement != 0f && IsOnGround())
        {
            if (animator != null) animator.SetBool("Walk", true);
        }
        // else if player stopped moving, set 'Walk' animation to false
        else
        {
            if (animator != null) animator.SetBool("Walk", false);
        }

        // assign current to prev for next frame check
        prevHorizontalMovement = horizontalMovement;
    }

    private void FixedUpdate()
    {
        // Move
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);

        // Apply jump
        if (justJumped)
        {
            // Add force upwards to Rigidbody2D
            justJumped = false;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // Trigger events on Jump
            onJump.Invoke();
        }
    }

    private bool IsOnGround()
    {
        return rb.IsTouching(groundContactFilter);
    }
}
