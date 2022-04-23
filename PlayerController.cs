using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    private bool isJumping, doubleJumping, doubleJumpingEnabled;
    private float moveHorizontal, moveVertical;
    private bool isFacingRight = true;

    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        isJumping = false;
        doubleJumping = false;
        doubleJumpingEnabled = false;
    }

    // Update is called once per frame
    void Update() {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        Flip();
    }

    //Movement update
    void FixedUpdate() {
        if (moveHorizontal > 0.1f || moveHorizontal < -0.1f) {
            rb.AddForce(new Vector2(moveHorizontal * moveSpeed, 0f), ForceMode2D.Impulse);
        }
        if (moveVertical > 0.1f && !isJumping || !doubleJumping && doubleJumpingEnabled) {
            rb.AddForce(new Vector2(0f, moveVertical * jumpForce), ForceMode2D.Impulse);
            doubleJumping = !doubleJumping;
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
        moveSpeed += change;
    }

    //Indirectly changes jump force from other scripts
    public void ChangeJumpForce(float change) {
        jumpForce += change;
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
