using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    private float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator, weaponAnimator;

    Vector2 movement;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    public int direction;

    private bool attacking;

    public GameObject dashEffect;
    public AudioClip dashSound;

    // Start is called before the first frame update
    void Start()
    {
        direction = 2;
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GetComponent<playerHealth>().dead)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");


            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                //moving left
                direction = 1;
                bullet.facingRight = false;
            }
            else if (Input.GetAxisRaw("Horizontal") == 1)
            {
                //moving right
                direction = 2;
                bullet.facingRight = true;
            }
            else if (Input.GetAxisRaw("Vertical") == 1)
            {
                direction = 3;
                bullet.facingRight = true;
            }
            else if (Input.GetAxisRaw("Vertical") == -1)
            {
                direction = 4;
                bullet.facingRight = true;
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
                if (direction == 1 && movement.magnitude > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("dash")))
                {
                    GetComponent<playerAttack>().PlaySound(dashSound);
                    moveSpeed = dashSpeed;
                    Instantiate(dashEffect, new Vector3(transform.position.x, transform.position.y, -6f), Quaternion.identity);
                }
                else if (direction == 2 && movement.magnitude > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("dash")))
                {
                    GetComponent<playerAttack>().PlaySound(dashSound);
                    moveSpeed = dashSpeed;
                    Instantiate(dashEffect, new Vector3(transform.position.x, transform.position.y, -6f), Quaternion.identity);
                }
                else if (direction == 3 && movement.magnitude > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("dash")))
                {
                    GetComponent<playerAttack>().PlaySound(dashSound);
                    moveSpeed = dashSpeed;
                    Instantiate(dashEffect, new Vector3(transform.position.x, transform.position.y, -6f), Quaternion.identity);
                }
                else if (direction == 4 && movement.magnitude > 0 && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("dash")))
                {
                    GetComponent<playerAttack>().PlaySound(dashSound);
                    moveSpeed = dashSpeed;
                    Instantiate(dashEffect, new Vector3(transform.position.x, transform.position.y, -6f), Quaternion.identity);
                }
            }

            animator.SetInteger("direction", Mathf.Clamp(direction, 1, 2));
            weaponAnimator.SetInteger("direction", Mathf.Clamp(direction, 1, 2));

            animator.SetBool("moving", movement.magnitude > 0);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

    }

    void FixedUpdate()
    {
        if (!GetComponent<playerHealth>().dead)
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
