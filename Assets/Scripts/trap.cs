using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public SpriteRenderer sr, sr2;
    public bool killMode = false;
    private IEnumerator coru;
    private bool waiting = false;

    // Start is called before the first frame update
    void Start()
    {
        coru = Wait();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            print("trapdoor stepped on");
            if (killMode == false)
            {
                StartCoroutine(Wait());
            }
            else
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyMelee>().ApplyDamage(3);
                }
                else if (collision.gameObject.CompareTag("Player"))
                {
                    collision.GetComponent<playerHealth>().takeDamage();
                }
            }
        }
    }*/

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            print("trapdoor stepped on");
            if (!killMode)
            {
                if (!waiting)
                {
                    GetComponent<SimpleFlash>().Flash(2.0f, 6, false);
                    StartCoroutine(Wait());
                }
            }
            else
            {
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.GetComponent<EnemyMelee>().ApplyDamage(3);
                }
                else if (collision.gameObject.CompareTag("Player"))
                {
                    collision.GetComponent<playerHealth>().takeDamage();
                }
            }
        }
    }

    IEnumerator Wait()
    {
        waiting = true;
        //print("waiting?");
        yield return new WaitForSeconds(2f);
        sr.enabled = false;
        sr2.enabled = true;
        killMode = true;
        yield return new WaitForSeconds(4f);
        sr.enabled = true;
        sr2.enabled = false;
        killMode = false;
        waiting = false;
    }
}
