using Assets.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private GameObject bulletPrefab; 
    [SerializeField] private Transform firePoint;
    [SerializeField] private Camera mainCamera;     

    private Rigidbody2D rb;
    private Animator animator;
    private UIManager uiManager;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        uiManager = UIManager.Instance;

    }

    void Update()
    {
        HandleMovement();
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu" || SceneManager.GetActiveScene().name == "LobbyScene")
        {
            return;
        }

        PlayerData playerData = SaveSystem.LoadPlayer();
        if (playerData.CurrentMana < 5)
        {
            return;  
        }

        uiManager.ModifyStats(mana: -5);
        playerData.CurrentMana -= 5;
        SaveSystem.SavePlayer(playerData);

        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - firePoint.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        
        bullet.GetComponent<PlayerBulletController>().SetDirection(direction);
    }

    private void HandleMovement()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed;

        if (playerInput.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (playerInput.x < 0) transform.localScale = new Vector3(-1, 1, 1);

        animator.SetBool("isRunning", playerInput != Vector2.zero);
    }
}
