using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private GameObject[] bullet;
    [SerializeField] private List<int> order = new List<int>();
    private int bulletIndex = 0;
    private int index = 0;
    private int maxIndex;

    private float shootTimer;
    public float shootTime;

    private bool running = false;   

    private void Start()
    {
        maxIndex = transform.childCount;
        shootTimer = shootTime;
    }

    private void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }
        else
        {
            //can shoot
            //index of which child to shoot towards
            if (index == maxIndex) index = 0;
            //instantiate bullet
            if (!running) StartCoroutine(shoot());
        }
    }

    IEnumerator shoot()
    {
        running = true;
        //yield return new WaitForSeconds(0.5f);
        Vector3 position = transform.position;
        position.z = -5;
        //b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -5);

        Vector4 c = GetComponent<SpriteRenderer>().color;
        for (int i = 0; i < 2; i++) {
            GetComponent<SpriteRenderer>().color = new Vector4(1, 0, 0, 1);
            yield return new WaitForSeconds(0.125f);
            GetComponent<SpriteRenderer>().color = c;
            yield return new WaitForSeconds(0.125f);
        }

        GameObject b = Instantiate(bullet[order[bulletIndex]], position, Quaternion.identity);
        //give bullet direction and target
        b.GetComponent<Bullet>().dir = transform.GetChild(index).position - transform.position;
        b.GetComponent<Bullet>().target = "Player";
        //reset shootTimer
        shootTimer = shootTime;
        index++;
        bulletIndex++;
        if (bulletIndex == order.Count) bulletIndex = 0;
        running = false;
        StopCoroutine(shoot());
    }

}
