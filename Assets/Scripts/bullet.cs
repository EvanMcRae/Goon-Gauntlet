using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float damage;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
       
        if(player.GetComponent<playerMovement>().direction == 2)
        {
            rb.velocity = new Vector2(10f, 0f);
        }
        else if(player.GetComponent<playerMovement>().direction == 1)
        {
            rb.velocity = new Vector2(-10f, 0f);
        }

        int x = Random.Range(0, 10);
        if(x == 1 || x == 0 || x == 2 || x== 3 || x==4 || x==5 || x==6 || x==7 || x==8)
        {
            GameObject.Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyMelee>().ApplyDamage(1);
            GameObject.Destroy(gameObject);
        }
        else if(other.tag == "wall")
        {
            GameObject.Destroy(gameObject);
        }
    }
}
