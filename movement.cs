using UnityEngine;
using TMPro;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float speed = 2f;
    public float jumpForce = 1f;
    public int maxHealth = 3;
    public int score = 0;


    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer spriteRenderer;

    private bool isGrounded;
    private int currentHealth;

    private Vector2 checkpointPosition;

    public TMP_Text scoreText;
    public Image[] healthHearts;

    public Transform groundCheck;
    public LayerMask groundLayer;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");

        // Движение влево/вправо
        rb.linearVelocity = new Vector2(moveInput * speed, rb.linearVelocity.y);

        // Поворот персонажа
        if (moveInput > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else if (moveInput < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);

        // Анимация бега
        anim.SetBool("isRunning", moveInput != 0);

        // Прыжок
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            anim.SetTrigger("Jump");
            isGrounded = false;
        }
    }
    void FixedUpdate()
    {
        {
            // Проверка земли Рэйкастом
            RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.1f, groundLayer);
            isGrounded = hit.collider != null;
        }

        // Проверка падения
        if (!isGrounded && rb.linearVelocity.y < -0.1f)
        {
            anim.SetBool("isFalling", true);
        }
        else
        {
            anim.SetBool("isFalling", false);
        }
    }

 

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.ResetTrigger("isFalling");
            anim.ResetTrigger("Jump");
        }

      
    }
} 
