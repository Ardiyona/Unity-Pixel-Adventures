using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerLife PL;

    private Rigidbody2D myRigidBody2D;
    private BoxCollider2D myBoxCollider2D;
    private SpriteRenderer sprite;
    private Animator anim;

    //Particle
    public ParticleSystem runDust;
    public ParticleSystem jumpDust;

    [SerializeField] private LayerMask isGroundedLayer;
    [SerializeField] private LayerMask isWalledLayer;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private float castDistance;

    //Player Movement & Flipping
    private float dirX;
    private bool facingRight = true;

    //Jumping
    [SerializeField] private float speed = 7f;
    [SerializeField] private float jumpForce = 48f;
    [SerializeField] private bool canDoubleJump;

    //Wall Jumping
    private bool isWallJumping;
    private float wallJumpingDirection;
    private float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    private float wallJumpingDuration = 0.4f;
    private Vector2 wallJumpingPower = new Vector2(10f, 14f);

    //Wall Sliding
    private float wallSlidingSpeed = 2f;
    [SerializeField] private bool isWallSliding;
    [SerializeField] private Transform wallCheck;

    [SerializeField] private AudioSource jumpSoundEffect;

    private enum PlayerState { idle, running, jumping, falling, wallSliding }

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody2D = GetComponent<Rigidbody2D>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PauseMenu.isPaused == false)
        {

            dirX = Input.GetAxisRaw("Horizontal");

            CheckInput();

            anim.SetBool("isGrounded", IsGrounded());
            anim.SetBool("isWallSliding", isWallSliding);
            UpdateAnimationState();

            if (IsGrounded())
            {
                canDoubleJump = true;
            }

            WallSlide();
            WallJump();

            if (!isWallJumping)
            {
                Flip();
            }

            FixedUpdate();
        }
    }

    private void FixedUpdate()
    {
        ParcticleState();
        if (!isWallJumping)
        {
            myRigidBody2D.velocity = new Vector2(dirX * speed, myRigidBody2D.velocity.y);
        }
    }

    private void CheckInput()
    {
        if (Input.GetButtonDown("Jump"))
        {
            JumpButton();
        }
    }

    private void Flip()
    {
        if (facingRight && dirX < 0f || !facingRight && dirX > 0f)
        {
            facingRight = !facingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void UpdateAnimationState()
    {
        PlayerState state;

        if (dirX > 0f)
        {
            state = PlayerState.running;
        }
        else if (dirX < 0f)
        {
            state = PlayerState.running;
        }
        else
        {
            state = PlayerState.idle;
        }

        if (myRigidBody2D.velocity.y > .1f)
        {
            state = PlayerState.jumping;
        }
        else if (myRigidBody2D.velocity.y < -.1f)
        {
            state = PlayerState.falling;
        }

        if (isWallSliding)
        {
            state = PlayerState.wallSliding;
        }

        if (IsWalled() && !IsGrounded() && dirX != 0)
        {
            state = PlayerState.wallSliding;
        }

        anim.SetInteger("state", (int)state);
    }

    public bool IsGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, isGroundedLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position - transform.up * castDistance, boxSize);
        Gizmos.DrawSphere(wallCheck.position, 0.2f);
    }
    */
    private void JumpButton()
    {

        if (IsGrounded())
        {
            jumpSoundEffect.Play();
            Jump();
            CreateJumpDust();
        }
        else if (canDoubleJump)
        {
            jumpSoundEffect.Play();
            canDoubleJump = false;
            Jump();
        }
    }

    private void Jump()
    {
        myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpForce);
    }

    private void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0f)
        {
            jumpSoundEffect.Play();
            isWallJumping = true;
            myRigidBody2D.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                facingRight = !facingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }

            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheck.position, 0.2f, isWalledLayer);
    }

    private void WallSlide()
    {
        if (IsWalled() && !IsGrounded() && dirX != 0)
        {
            isWallSliding = true;
            myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, Mathf.Clamp(myRigidBody2D.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }
        else
        {
            isWallSliding = false;
        }
    }

    private void CreateRunDust()
    {
        runDust.Play();
    }
    private void CreateJumpDust()
    {
        jumpDust.Play();
    }

    private void ParcticleState()
    {
        if (IsGrounded() == true && PL.died == false)
        {
            if ((dirX < 0f) || (dirX > 0f))
            {
                CreateRunDust();
            }
        }
    }
}