using UnityEngine;

public class boxCollider : MonoBehaviour
{
    public float moveSpeed = 5f;  
    public float groundCheckDistance = 1f;  

    private Rigidbody2D rb;  

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");  
        float moveY = Input.GetAxis("Vertical");   

        Debug.Log("MoveX: " + moveX + ", MoveY: " + moveY);

        Vector2 movement = new Vector2(moveX, moveY) * moveSpeed * Time.deltaTime;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance);
        if (hit.collider != null)
        {
            rb.MovePosition((Vector2)transform.position + movement);
        }
    }
}
