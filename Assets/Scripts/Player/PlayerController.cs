using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] public float jumpForce, moveSpeed, fallMultiplier;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isJumping, doubleJumping, doubleJumpingEnabled;
    private float moveHorizontal, moveVertical;
    private bool isFacingRight = true;

    private AudioManager audioManager;

    public Animator animator;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        doubleJumping = false;
        doubleJumpingEnabled = false;

        audioManager = FindObjectOfType<AudioManager>();

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));
        animator.SetBool("IsJumping", isJumping);

        Flip();

        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * fallMultiplier * Time.deltaTime;
        }
    }

    //Movement update
    void FixedUpdate() {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f) {
            rb.velocity = new Vector2(moveSpeed * moveHorizontal, rb.velocity.y);
            //rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
            audioManager.Play("Footsteps");
        }
        else if(moveHorizontal == 0)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        if (moveVertical > 0.1f && !isJumping || !doubleJumping && doubleJumpingEnabled) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * moveVertical);
            //rb.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            doubleJumping = !doubleJumping;

            audioManager.Play("Jump");
            
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Platform") {
            isJumping = false;
            doubleJumping = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.tag == "Platform") {
            isJumping = true;
        }
    }

    //Check if the player is on a ground
    private bool IsGrounded() {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    //Indirectly changes move speed from other scripts
    public void ChangeMoveSpeed(float change) {
        moveSpeed *= change;
    }

    //Main use to reset move speed in enemy controller script
    public void SetMoveSpeed(float speed)
    {
        moveSpeed = speed;
    }

    //Indirectly changes jump force from other scripts
    public void ChangeJumpForce(float change) {
        jumpForce += change;
    }

    //Main use to reset jump force in enemy controller script
    public void SetJumpForce(float force)
    {
        jumpForce = force;
    }

    //Enables double jumping
    public void EnableDoubleJump() {
        doubleJumpingEnabled = true;
    }

    //Flips your character around when facing left
    private void Flip() {
        if (isFacingRight && moveHorizontal < 0f || !isFacingRight && moveHorizontal > 0f) {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
