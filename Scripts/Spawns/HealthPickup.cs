using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement.Instance.stats.addHealth();
            Destroy(gameObject);
            if(GameObject.Find("Health spawner") != null)
            GameObject.Find("Health spawner").GetComponent<HealthSpawner>().count--;
        }
    }
}
