using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Audio;

public class Hole : MonoBehaviour
{
    private void Start()
    {
    }

    public void startSpawn()
    {
        StartCoroutine(spawn());       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Vector3 pos = transform.position;
            pos.z = -5;
            collision.GetComponent<Transform>().position = pos;
            collision.GetComponent<PlayerMovement>().canMove = false;
            collision.GetComponent<PlayerMovement>().stats.takeDamage(50);
            //StartCoroutine(small());
            Time.timeScale = 0.25f;
        }
    }

    IEnumerator spawn()
    {
        float alpha = 1f;
        while(alpha > -0.125f)
        {
            yield return new WaitForSeconds(0.125f);
            GetComponent<SpriteRenderer>().color = new Vector4(1, 1, 1, alpha);
            alpha -= 0.125f;
        }
        yield return new WaitForSeconds(0.5f);
        GetComponent<EdgeCollider2D>().enabled = true;
        CameraShake.ShakeOnce(0.15f, 1.15f);
        AudioManager.Instance.Play("block");
        StopCoroutine(spawn());
    }

    IEnumerator small()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSecondsRealtime(0.25f);
            GameObject.Find("Player").transform.localScale -= Vector3.one/10;
        }
    }

}
