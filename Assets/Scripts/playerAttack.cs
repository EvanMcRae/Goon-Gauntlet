using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public enum Weapon
    {
        NONE, MAGNET, CLAW, GLOVE, GUN, SCISSORS, FAN
    }

    public Weapon weapon;

    public RuntimeAnimatorController[] weaponAnimators;
    public RuntimeAnimatorController normalAnimator, attackAnimator;
    public Animator animator, weaponAnimator;

    public Transform attackPoint;
    public LayerMask enemyLayers;
    public float attackRange = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator.runtimeAnimatorController = normalAnimator;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Attack();
            if (weapon != Weapon.GUN)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                    if (weapon == Weapon.CLAW)
                    {

                    }
                    else if (weapon == Weapon.GLOVE)
                    {

                    }
                }
            }
            else
            {
                //implement shooting mechanincs 
            }
        }
    
        // TEMP - resetting attack display
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.runtimeAnimatorController = normalAnimator;
            weaponAnimator.SetBool("attack", false);
        }
        
        // TEMP - testing dice roll
        if (Input.GetKeyDown(KeyCode.R))
        {
            rollDice();
        }    
    }

    void Attack()
    {
        animator.runtimeAnimatorController = attackAnimator;
        weaponAnimator.SetBool("attack", true);
        //StartCoroutine(PerformAttack());
    }

    public void rollDice()
    {
        int picked = 0;
        do {
            picked = Random.Range(1, 7);
        } while (picked == (int) weapon);
        weapon = (playerAttack.Weapon) picked;
        weaponAnimator.runtimeAnimatorController = weaponAnimators[(int) weapon];
    }

    IEnumerator PerformAttack()
    {
        //detect which weapon is selected
        //detect what direction the player is facing
        if(weapon != Weapon.GUN)
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log("we hit " + enemy.name);
                enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                if(weapon == Weapon.CLAW)
                {

                }
                else if(weapon == Weapon.GLOVE)
                {

                }
            }
        }
        else
        {
            //implement shooting mechanincs 
        }
        yield return null;
    }
}
