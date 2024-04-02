using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //general movement variables
    private float dirX = 0f;
    public Rigidbody2D myRigidBody;
    [SerializeField] private float jumpForce = 4;
    [SerializeField] private float moveSpeed = 5;

    //sprint vars
    public float sprintSpeed = 10;
    private bool sprint = false;

    //hang time vars
    [SerializeField] private float hangTime = 0.3f;
    private float hangCount;

    //jump buffer vars
    [SerializeField] private float jumpBuffer = 0.3f;
    private float bufferCount;

    //colider for ground checking 
    [SerializeField] LayerMask GroundLayer;
    [SerializeField] private float colliderRadius = 0.1f;
    [SerializeField] Transform GroundCheckCollider;

    //animation stuff
    public SpriteRenderer mySprite;
    public Animator myAnimator;

    // Update is called once per frame
    void Update()
    {
        //horizontal movement
        dirX = (Input.GetAxisRaw("Horizontal"));
        isSprinting();
        if (sprint == true)
        {
            myRigidBody.velocity = new Vector2(dirX * sprintSpeed, myRigidBody.velocity.y);
        }
        //if player is knocked, the velocity inputs are taken from the knockback function
        else if (gameObject.GetComponent<PlayerLife>().isKnocked)
        {
            return;
        }
        else
        {
            myRigidBody.velocity = new Vector2(dirX * moveSpeed, myRigidBody.velocity.y);
        }

        //hang time - for smoother jumps - adds a timer in which player can jump even if in the air
        if (isGrounded() && myRigidBody.velocity.y < 0.05f)
        {
            hangCount = hangTime;
        }
        else
        {
            hangCount -= Time.deltaTime;
        }
        //jump buffer - for smoother jumps - adds a timer so imput counts longer
        if (Input.GetKeyDown(KeyCode.Space))
        {
            bufferCount = jumpBuffer;
        }
        else
        {
            bufferCount -= Time.deltaTime;
        }
        //jump - enhanced by buffer and hang
        if (bufferCount > 0 && hangCount > 0)
        {
            myRigidBody.velocity = Vector2.up * jumpForce;
            bufferCount = 0f;
            hangCount = 0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && myRigidBody.velocity.y > 0)
        {
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }

        //updates animation
        UpdateAnimationState();
    }
    //finds out if player collider overlaps with the ground - player is grounded
    public bool isGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheckCollider.position, colliderRadius, GroundLayer);
    }
    //binds sprint for the player
    public void isSprinting()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            sprint = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            sprint = false;
        }
    }
    //finds out the state of the player for animation purposes
    private void UpdateAnimationState()
    {
        if (isGrounded())
        {
            myAnimator.SetBool("isGrounded", true);
        }
        else
        {
            myAnimator.SetBool("isGrounded", false);
        }
        if (myRigidBody.velocity.x > 0f)
        {
            myAnimator.SetBool("isRunning", true);
            mySprite.flipX = false;
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
        else if (myRigidBody.velocity.x < 0f)
        {
            myAnimator.SetBool("isRunning", true);
            gameObject.transform.localScale = new Vector3(-1, 1, 1);
            
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }
}


