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
    public bool dead = false;
    public int chance = 3;

    private IEnumerator coru1;

    public GameObject prefab;
    private AudioSource[] sources;
    public AudioClip hurtSound, deathSound, lungeSound;

    // Start is called before the first frame update
    void Start()
    {
        chance = Random.Range(0, 6);
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        sources = transform.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > stoppingDistance && movement && !stunned && !dead)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            //movement = false;
        }
        
        if (!dead)
            GetComponent<SpriteRenderer>().flipX = target.position.x - transform.position.x < 0;
    }

    public void ApplyDamage(float damage)
    {
        // MethodBase methodBase = MethodBase.GetCurrentMethod();
        // Debug.Log(methodBase.Name);
        
        if (!dead)
        {
            damage = Mathf.Abs(damage);
            transform.GetComponent<Animator>().SetTrigger("hit");
            life -= damage;
            if (life < 0) life = 0;
            GetComponent<SimpleFlash>().Flash(0.5f, 2, true);

            if(life == 0)
            {
                waveSpawner.kills++;
                dead = true;
                int x = Random.Range(0, 6);
                if(x == 1)
                {
                    GameObject obj = Instantiate(prefab, transform.position, Quaternion.identity);
                }
                StartCoroutine(Die());
            }
            else
            {
                PlaySound(hurtSound);
            }
        }
    }

    public void knockBack()
    {
        if (!dead)
        {
            Vector3 dir = target.transform.position - transform.position;
            dir = dir.normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * -force);
        }
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
        PlaySound(lungeSound);
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
        if (other.gameObject.CompareTag("Player") && canAttack && !dead)
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

    public void PlaySound(AudioClip clip)
    {
        foreach (AudioSource source in sources)
        {
            if (source.clip == clip && source.isPlaying)
            {
                if (source.time < 0.2f && source.isPlaying) return;
                else source.Stop();
            }
        }
        for (int index = sources.Length - 1; index >= 0; index--)
        {
            if (!sources[index].isPlaying)
            {
                sources[index].clip = clip;
                sources[index].loop = false;
                sources[index].Play();
                return;
            }
        }
    }

    public IEnumerator Die()
    {
        PlaySound(deathSound);
        GetComponent<Animator>().SetTrigger("dead");
        GetComponent<Collider2D>().enabled = false;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        // TODO add particles, change animation state, change wait time
        yield return new WaitForSeconds(2.0f);
        GameObject.Destroy(gameObject);
    }

}
