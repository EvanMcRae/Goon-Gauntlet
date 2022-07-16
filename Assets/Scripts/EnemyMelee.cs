using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public float life;
    public float speed;
    public float stoppingDistance;
    public float force;
    private Transform target;

    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            //start lunge animation
        }
    }

    void ApplyDamage(float damage)
    {
        // MethodBase methodBase = MethodBase.GetCurrentMethod();
        // Debug.Log(methodBase.Name);
        
        damage = Mathf.Abs(damage);
        // transform.GetComponent<Animator>().SetBool("Hit", true);
        life -= damage;
        if (life < 0) life = 0;

        if(life == 0)
        {
            waveSpawner.kills++;
        }
    }

    void lunge()
    {
        //maybe gets players position in a holder, then moves towards it

        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * force);
        print("moves toward player");
    }
}
