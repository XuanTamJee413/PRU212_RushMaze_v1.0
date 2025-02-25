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
        UpdateAnimation();
    }

    private void HandleMovement()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(moveInputX * moveSpeed, moveInputY * moveSpeed);

        if (moveInputX > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (moveInputX < 0) transform.localScale = new Vector3(-1, 1, 1);
    }


    private void UpdateAnimation()
    {
        bool isMoving = rb.linearVelocity.magnitude > 0.1f;

        animator.SetBool("PlayerRun", isMoving);
        animator.SetBool("PlayerIdle", !isMoving);
    }
}
