using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float life = 3;
    public float speed;
    public float stoppingDistance;
    public float force;
    private Transform target;

    public Rigidbody2D rb;

    public static bool movement = true;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance && movement == true)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            //movement = false;
        }
        
        GetComponent<SpriteRenderer>().flipX = target.position.x - transform.position.x < 0;
    }

    public void ApplyDamage(float damage)
    {
        // MethodBase methodBase = MethodBase.GetCurrentMethod();
        // Debug.Log(methodBase.Name);
        
        damage = Mathf.Abs(damage);
        // transform.GetComponent<Animator>().SetBool("Hit", true);
        life -= damage;
        if (life < 0) life = 0;
        GetComponent<SimpleFlash>().Flash(1.0f, 2, true);

        if(life == 0)
        {
            waveSpawner.kills++;
            GameObject.Destroy(gameObject);
        }
    }

    void lunge()
    {
        //maybe gets players position in a holder, then moves towards it

        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * force);
        //print("moves toward player");
    }

    void moveAgain()
    {
        if(movement == false)
        {
            movement = true;
            rb.velocity = new Vector2(0f,0f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            target.GetComponent<playerHealth>().takeDamage();
        }
    }

}
