using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trap : MonoBehaviour
{
    public SpriteRenderer sr;
    public bool killMode = false;
    private IEnumerator coru;
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
    }

    IEnumerator Wait()
    {
        //print("waiting?");
        yield return new WaitForSeconds(2f);
        sr.enabled = false;
        killMode = true;
        yield return new WaitForSeconds(9);
        sr.enabled = true;
        killMode = false;
    }
}
