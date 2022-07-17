using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : MonoBehaviour
{
    private Transform target;
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
        print("tried to despawn");
        GameObject.Destroy(gameObject);
    }

    public void magnetPull()
    {
        Vector3 dir = target.transform.position - transform.position;
        dir = dir.normalized;
        GetComponent<Rigidbody2D>().AddForce(dir * (2));
    }

}
