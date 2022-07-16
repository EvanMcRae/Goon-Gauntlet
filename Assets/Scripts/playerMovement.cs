using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

public class playerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    private float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public RuntimeAnimatorController normalAnimator, attackAnimator;

    Vector2 movement;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        dashTime = startDashTime;
        animator.runtimeAnimatorController = normalAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");


        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            //moving left
            direction = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") == 1)
        {
            //moving right
            direction = 2;
        }
        else if (Input.GetAxisRaw("Vertical") == 1)
        {
            direction = 3;
        }
        else if (Input.GetAxisRaw("Vertical") == -1)
        {
            direction = 4;
        }

        if (dashTime <= 0)
        {
            //direction = 0;
            dashTime = startDashTime;
            moveSpeed = walkSpeed;
        }
        else
        {
            dashTime -= Time.deltaTime;
            if (direction == 1 && Input.GetKeyDown(KeyCode.Space))
            {

                moveSpeed = dashSpeed;
                print("try to dash left");
            }
            else if (direction == 2 && Input.GetKeyDown(KeyCode.Space))
            {
                moveSpeed = dashSpeed;
            }
            else if (direction == 3 && Input.GetKeyDown(KeyCode.Space))
            {
                moveSpeed = dashSpeed;
            }
            else if (direction == 4 && Input.GetKeyDown(KeyCode.Space))
            {
                moveSpeed = dashSpeed;
            }
        }
        
        animator.SetInteger("direction", direction);
        animator.SetBool("moving", movement.magnitude > 0);

        // TEMP CODE - gets arm moving
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.runtimeAnimatorController = attackAnimator;

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.runtimeAnimatorController = normalAnimator;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

}
