using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] holeLocation;
    [SerializeField] private int limit;
    private Animator anim;
    private int count = 0;
    public float spawnTime;
    private List<int> list = new List<int>();

    private void Start()
    {
        //anim = GetComponent<Animator>();
        StartCoroutine(spawnItem());
    }

    IEnumerator spawnItem()
    {
        while (count < limit)
        {
            yield return new WaitForSeconds(spawnTime);
Redo:
            int r = Random.Range(0, holeLocation.Length);
            if (list.Contains(r)) goto Redo;
            list.Add(r);

            //anim.SetBool("blink", true);
            yield return new WaitForSeconds(1f);
            //play animation
            holeLocation[r].GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, 0);

            //enable the script and collider
            //holeLocation[r].GetComponent<EdgeCollider2D>().enabled = true;
            holeLocation[r].GetComponent<Hole>().enabled = true;
            holeLocation[r].GetComponent<Hole>().startSpawn();

            count++;
            if (count == limit)
            {
                //Destroy(gameObject);
                StopCoroutine(spawnItem());
            }
        }
    }
}
