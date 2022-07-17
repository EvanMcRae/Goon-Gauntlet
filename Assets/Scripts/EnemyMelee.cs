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

    public bool movement = true;
    public bool canAttack = false;
    private bool stunned = false;
    private bool dead = false;
    public int chance = 3;

    private IEnumerator coru1;

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 6);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance && movement && !stunned)
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
        transform.GetComponent<Animator>().SetBool("hit", true);
        life -= damage;
        if (life < 0) life = 0;
        GetComponent<SimpleFlash>().Flash(0.5f, 2, true);

        if(life == 0)
        {
            if (!dead)
                waveSpawner.kills++;
            dead = true;
            int x = Random.Range(0, 5);
            if(x == 1)
            {
                GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
            }
            GameObject.Destroy(gameObject);
        }
    }

    public void knockBack()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * -force);
    }

    public void fanPush()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * -(force-380));
    }

    public void magnetPull()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * (force - 398));
    }

    public void stun()
    {
        coru1 = stunAndwWait();
        StartCoroutine(coru1);
    }

    void lunge()
    {
        //maybe gets players position in a holder, then moves towards it
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * force);
       // print("moves toward player");
    }

    void moveAgain()
    {
        if (!movement)
        {
            movement = true;
            rb.velocity = new Vector2(0f,0f);
        }
    }

    void allowAttacking()
    {
        canAttack = true;
        movement = false;
        rb.velocity = new Vector2(0f, 0f);
        
        EnemyMelee[] enemies = GameObject.FindObjectsOfType<EnemyMelee>();
        foreach (EnemyMelee e in enemies)
        {
            e.chance = Random.Range(0, 6);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && canAttack)
        {
            target.GetComponent<playerHealth>().takeDamage();
            canAttack = false;
            GetComponent<Animator>().SetTrigger("slam");
        }
    }

    IEnumerator stunAndwWait()
    {
        stunned = true;
        rb.velocity = new Vector2(0f, 0f);
        yield return new WaitForSeconds(5);
        stunned = false;
    }

}
