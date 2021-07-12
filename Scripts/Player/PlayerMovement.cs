using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Score;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject[] cannons;
    public Stats stats;
    public static PlayerMovement Instance { get; set; }
    public HealthBar healthBar;
    private Rigidbody2D rb;
    private CircleCollider2D box;

    private Animator anim;
    private Vector2 movement;
    public bool canMove = true;

    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool isDashing = false;

    private int counter = 1;

    private void Start()
    {
        Instance = this;
        dashTimer = dashTime;
        stats = new Stats(8, 4.5f, 5);
        healthBar.setMaxHealth(stats.getHealth());
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody2D>();
        box = GetComponent<CircleCollider2D>();
        anim = GetComponentInChildren<Animator>();
        InvokeRepeating("updateScore", 20f, 10f);
    }

    private void Update()
    {
        updateHealth();
        if (canMove) getInputs();
        else movement = Vector2.zero;
    }

    private void FixedUpdate()
    {
        movePlayer();
        if (isDashing) executeDash();
    }

    private void getInputs()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump") && dashCooldownTimer <= 0)
        {
            dashTimer = dashTime;
            isDashing = true;
        }
        if (dashCooldownTimer > 0) { dashCooldownTimer -= Time.deltaTime; anim.SetBool("Dash ready", false); }
        if(dashCooldownTimer <= 0) anim.SetBool("Dash ready", true);
    }

    private void movePlayer()
    {
        float pen = 1f;
        if ((movement.x > 0.5f || movement.x < -0.5f) && (movement.y > 0.5f || movement.y < -0.5f))
        {
            pen = 1.15f;
        }

        rb.velocity = movement.normalized * stats.getSpeed() * pen;
    }

    private void executeDash()
    {
        if (dashTimer <= 0)
        {
            box.enabled = true;
            isDashing = false;
            rb.velocity = Vector2.zero;
            dashCooldownTimer = dashCooldown;
        }
        else
        {
            box.enabled = false;
            dashTimer -= Time.deltaTime;
            rb.velocity += movement.normalized * dashSpeed;
        }
    }

    private void updateHealth()
    {
        if (stats.getHealth() > healthBar.getMaxHealth()) healthBar.setMaxHealth(stats.getHealth());
        healthBar.setHealth(stats.getHealth());
        if (stats.getHealth() <= 0)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(2);
        }
    }

    private void updateScore()
    {
        Points.Instance.addScore(51);
        if(Points.Instance.getScore() > 500*counter)
        {
            foreach(GameObject cannon in cannons)
            {
                cannon.GetComponent<Cannon>().shootTime -= 0.5f;
            }
            counter++;
        }
        Debug.Log("added 51");
    }

    public Rigidbody2D getRigidbody()
    {
        return rb;
    }

    public Vector2 getMovement()
    {
        return movement;
    }
}
