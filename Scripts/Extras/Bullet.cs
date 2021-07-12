using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;
using Score;

public class Bullet : MonoBehaviour
{
    public Stats stat;
    private Rigidbody2D rb;
    public string target;
    public Vector2 dir;

    private void Start()
    {
        stat = new Stats(10f, 2);
        rb = GetComponent<Rigidbody2D>();   
    }

    private void FixedUpdate()
    {
        rb.velocity = dir.normalized * stat.getSpeed();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Boundary")) { die(); return; }
        if (collision.CompareTag(target))
        {
            if(collision.GetComponent<PlayerMovement>() != null)
            {
                collision.GetComponent<PlayerMovement>().stats.takeDamage(stat.getDamage());
            }
            if (collision.GetComponent<Bullet>() != null) { collision.GetComponent<Bullet>().die(); Points.Instance.addScore(3); }
            die();
        }
    }

    public void die()
    {
        AudioManager.Instance.Play("bubble");
        Destroy(gameObject);
    }
}
