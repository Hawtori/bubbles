using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashSpeed;
    public float dashTime;
    public float dashCooldown;
    private float dashTimer;
    private float dashCooldownTimer;
    private bool isDashing = false;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dashTimer = dashTime;
    }

    private void Update()
    {
        if (PlayerMovement.Instance.canMove)
        {
            getInputs();
        }
    }

    private void FixedUpdate()
    {
        if (isDashing) executeDash(); 
    }

    private void getInputs()
    {
        if (Input.GetButtonDown("Jump") && dashCooldownTimer <= 0)
        {
            dashTimer = dashTime;
            isDashing = true;
        }
        if (dashCooldownTimer > 0) dashCooldownTimer -= Time.deltaTime;
    }

    private void executeDash()
    {
        if (dashTimer <= 0)
        {
            isDashing = false;
            PlayerMovement.Instance.getRigidbody().velocity = Vector2.zero;
            dashCooldownTimer = dashCooldown;
        }
        else
        {
            dashTimer -= Time.deltaTime;
            rb.velocity += PlayerMovement.Instance.getMovement().normalized * dashSpeed;
        }
    }
}
