using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpForce = 15.0f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;


    private Rigidbody2D rb;
    private bool isGrounded; // nguoi choi da dap xuong dat
    private Animator animator;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleCrouch();
        HandleJump();
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        // di chuyen trai phai va lat nhan vat 
        float moveInput = Input.GetAxis("Horizontal");

        float currentSpeed = (Input.GetAxisRaw("Vertical") < 0) ? moveSpeed * 0.5f : moveSpeed;

        rb.linearVelocity = new Vector2(moveInput * currentSpeed, rb.linearVelocity.y);
        if (moveInput > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void HandleJump()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");

        if ((Input.GetButtonDown("Jump") || verticalInput > 0) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
        else if (verticalInput < 0)
        {
            //HandleCrouch();
        }
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }
    private void HandleCrouch()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (verticalInput < 0)
        {
            if (transform.localScale.x > 0) transform.rotation = Quaternion.Euler(0, 0, -90);
            else if (transform.localScale.x < 0) transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }


    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
}
