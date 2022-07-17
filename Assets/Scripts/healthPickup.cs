using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    private Transform target;
    public float force;

    public GameObject healthEffect;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void despawn()
    {
        Instantiate(healthEffect, new Vector3(transform.position.x, transform.position.y, -6f), Quaternion.identity);
        GameObject.Destroy(gameObject);
    }

    public void magnetPull()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * (3));
    }

    public void fanPush()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * -(force - 380));
    }

}
