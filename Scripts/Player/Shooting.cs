using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    public GameObject ready;

    private float shootTimer;
    private float shootTime = 10f;

    private void Start()
    {
        shootTimer = 0f;
    }

    private void Update()
    {
        if (!EscapeMenu.isPaused)
        {
            if (shootTimer > 0)
            {
                shootTimer -= Time.deltaTime;
                ready.SetActive(false);
            }
            else
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos.z = 5.23f;
                Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.parent.position);
                mousePos.x = mousePos.x - objectPos.x;
                mousePos.y = mousePos.y - objectPos.y;

                ready.SetActive(true);

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject b = Instantiate(bullet, transform.position, Quaternion.identity);
                    b.GetComponent<Bullet>().target = "Bullet";
                    b.GetComponent<Bullet>().dir = mousePos;
                    shootTimer = shootTime;
                    GetComponentInParent<PlayerMovement>().stats.takeDamage(1); 
                }
            }
        }
    }
}
