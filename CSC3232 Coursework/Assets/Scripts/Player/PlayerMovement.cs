using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variables

    [Header("References")]
    [Space(10)]
    [SerializeField]
    private PlayerCollision collision;
    [SerializeField]
    private PlayerCombat playerCombat;
    private Rigidbody2D rb;
    [SerializeField]
    private PlayerSounds playerSounds;
    [SerializeField]
    private PlayerAnimation playerAnimation;

    [Header("Movement")]
    [Space(10)]
    [SerializeField]
    private float damageBumpForce;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float acceleration;
    [SerializeField]
    private float frictionAmount;
    [SerializeField]
    private float crouchFrictionModifier;
    [SerializeField]
    [Range(0, 5)]
    private bool lastFrameFriction;
    private bool isFriction;
    private bool canMove;
    private float crouching;
    private bool isCrouching;
    private bool lastFrameGrounded;
    private bool grounded;
    private float xInput;
    private float xVelocity;
    private float yVelocity;

    [Space(5)]
    [Header("Jumping")]
    [SerializeField]
    private float yDeathHeight;
    [SerializeField]
    [Range(0, 1)]
    private float coyoteTime;
    [SerializeField]
    [Range(0, 1)]
    private float jumpBuffer;
    private float coyoteTimer;
    private float jumpBufferTimer;
    [Space(5)]
    [SerializeField]
    private float jumpForce;
    [SerializeField]
    private float endJumpForce;
    private bool endJumpForceApplied;
    [Space(5)]
    [SerializeField]
    private float airborneControl;
    private bool jumping;

    private float weight;

    #endregion

    #region Getters/Setters

    public float GetXVelocity()
    {
        return xVelocity;
    }

    public float GetYVelocity()
    {
        return yVelocity;
    }

    public float GetXInput()
    {
        return xInput;
    }

    public float GetRunSpeed()
    {
        return runSpeed;
    }

    public bool GetIsFriction()
    {
        return isFriction;
    }

    public bool GetCrouching()
    {
        return isCrouching;
    }

    public void UpdateWeight(float newWeight)
    {
        rb.mass = newWeight;
    }

    public void ResetWeight()
    {
        rb.mass = weight;
    }

    public Vector2 GetPosition()
    {
        return rb.position;
    }

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
        weight = rb.mass;
        grounded = false;
        isFriction = false;
    }

    // Update for physics
    void FixedUpdate()
    {

        //if the player is dead, disable all their stuff
        if (playerCombat.GetCurrentHealth() <= 0)
        {
            //stop it from moving
            canMove = false;
            //disable the collider that allows enemies to see it
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }


        xVelocity = rb.velocity.x;
        yVelocity = rb.velocity.y;

        //find the difference between the max speed the player could be moving, to the current speed they are moving
        float targetSpeed = xInput * runSpeed;
        float difference = targetSpeed - xVelocity;
        float friction;

        //determine the amount of friction on the player, depending on their speed
        friction = Mathf.Min(Mathf.Abs(xVelocity), frictionAmount);
        friction *= Mathf.Sign(xVelocity);

        //set movement force, depending on how close the player is to their top speed
        //(the closer to top speed, the lower the force)
        float movement = Mathf.Pow(Mathf.Abs(difference), acceleration) * Mathf.Sign(difference);

        // if player is in the air, they can't control themselves as well
        float airControl = 1;
        if (!grounded)
        {
            airControl = airborneControl;
        }

        lastFrameFriction = isFriction;

        isFriction = false;

        //if player not entering in x movement command, or if they are moving other direction to input, apply friction
        if (!canMove || (Mathf.Abs(xInput) < 0.01f) || (grounded && Mathf.Abs(xInput) > 0.01f && targetSpeed * xVelocity < 0 && Mathf.Abs(xVelocity) > 0.01f))
        {
            //if the player is moving, friction is true
            if (Mathf.Abs(xVelocity) > 0.01f)
            {
                isFriction = true;
            }

            //if the player is crouching, more friction
            float crouchFriction = 1;
            if (isCrouching)
            {
                crouchFriction = crouchFrictionModifier;
            }

            //add a force oppising the player's movement to slow them down
            rb.AddForce(Vector2.right * -friction * airControl * crouchFriction, ForceMode2D.Impulse);
        }
        //if player is moving
        else if (Mathf.Abs(movement) > 0.01f)
        {
            //add the movement force to the player
            rb.AddForce(Vector2.right * movement * airControl);
        }

        if (lastFrameFriction != isFriction && !lastFrameFriction)
        {
            playerSounds.PlaySkid();
        }

    }

    //if the player hits into an enemy, this is called
    public void DamageBump(Vector3 direction)
    {
        rb.AddForce(direction * damageBumpForce, ForceMode2D.Impulse);
    }

    //jump
    private void Jump()
    {
        playerSounds.PlayJump();
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    //reducing jump height
    private void EndJump()
    {
        rb.AddForce(Vector2.down * endJumpForce * rb.velocity.y, ForceMode2D.Impulse);
    }



    // Update is called once per frame
    void Update()
    {

        //if player is out of bounds they should die
        if (transform.position.y < yDeathHeight && playerCombat.GetCurrentHealth() > 0)
        {
            //deals damage to the player equal to their max health
            playerCombat.TakeDamage(5);
        }

        lastFrameGrounded = grounded;

        //check if the player is grounded
        grounded = collision.GetGrounded();

        //if player has just landed, play sound and make effect
        if (lastFrameGrounded != grounded && !lastFrameGrounded)
        {
            playerSounds.PlayLandJump();
            playerAnimation.CreateDust();
        }

        //a timer that checks how long a player has been not grounded for 
        if (grounded)
        {
            coyoteTimer = coyoteTime;
        }
        else
        {
            coyoteTimer -= Time.deltaTime;
            //to fix attacking bug
            playerCombat.SetAttacking(false);
        }

        // when the player enters in the jump, set the jump buffer to its max
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferTimer = jumpBuffer;
        }
        else
        {
            jumpBufferTimer -= Time.deltaTime;
        }

        //get the left/right input
        xInput = Input.GetAxisRaw("Horizontal");

        crouching = Input.GetAxisRaw("Vertical");

        if (crouching < 0 && grounded)
        {
            isCrouching = true;
        }
        else
        {
            isCrouching = false;
        }

        //if the player has been off the ground for less than the coyote time, and entered the jump input within the buffer time, jump
        //(allows for more forgiving movement, ensures that inputs a couple frames early/late don't get missed)
        if (coyoteTimer > 0f && jumpBufferTimer > 0f && !jumping && !playerCombat.GetAttacking())
        {
            Jump();
            jumping = true;
            coyoteTimer = 0f;
            jumpBufferTimer = 0f;
        }

        // if the player is jumping and lets go of jump button, reduce jump height
        if (!endJumpForceApplied && jumping && ((!Input.GetButton("Jump") && rb.velocity.y > 3f) || playerCombat.GetIsHurt()))
        {
            EndJump();
            endJumpForceApplied = true;
        }
        //if player not moving up on y, they are not jumping (but can still be falling)
        if (rb.velocity.y < 0)
        {
            jumping = false;
            endJumpForceApplied = false;
        }

        //if the player is attacking or crouching or DEAD, they can't move
        if (playerCombat.GetAttacking() || isCrouching || playerCombat.GetCurrentHealth() == 0 || playerCombat.GetIsHurt())
        {
            canMove = false;
        }
        else
        {
            canMove = true;
        }

    }

}
