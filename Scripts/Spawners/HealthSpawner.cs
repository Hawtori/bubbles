using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Score;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoint;
    [SerializeField] private GameObject health;
    [SerializeField] private int limit;
    [SerializeField] private int maxPickups;
    public int count = 0;
    public float spawnTime;
    private bool updateSpawn = false;
    private bool running = true;

    private void Start()
    {
        StartCoroutine(spawnItem());
    }

    private void Update()
    {
        if (updateSpawn)
        {
            spawnTime *= Points.Instance.getScore() / 50f;
            updateSpawn = false;
        }
        if (!running) StartCoroutine(spawnItem());
    }

    IEnumerator spawnItem()
    {
        running = true;
        while (count < limit)
        {
            yield return new WaitForSeconds(spawnTime);
            int r = Random.Range(0, spawnPoint.Length);
            if (spawnPoint[r] == null) continue;
            GameObject h = Instantiate(health, spawnPoint[r].transform.position, Quaternion.identity);
            h.transform.parent = GameObject.Find("===Pickup===").transform;
            Destroy(spawnPoint[r]);
            count++;
            if (count == maxPickups)
            {
                Destroy(gameObject);
                StopCoroutine(spawnItem());
            }
            updateSpawn = true;
        }
        running = false;
    }
}
