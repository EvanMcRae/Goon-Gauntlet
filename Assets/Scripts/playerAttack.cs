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
    private bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        animator.runtimeAnimatorController = normalAnimator;
        attacking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B) && !attacking && weapon != Weapon.NONE)
        {
            Attack();
        }
        
        // TEMP - testing dice roll
        if (Input.GetKeyDown(KeyCode.R))
        {
            rollDice();
        }

        if (attacking)
        {
            //detect which weapon is selected
            //detect what direction the player is facing
            if (weapon != Weapon.GUN)
            {
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

                foreach (Collider2D enemy in hitEnemies)
                {
                    Debug.Log("we hit " + enemy.name);
                    

                    if (weapon == Weapon.CLAW)
                    {
                        // TODO how to differentiate claw?
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                        attacking = false;
                    }
                    else if(weapon == Weapon.SCISSORS)
                    {
                        // TODO add enemy knockback
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                        attacking = false;
                    }
                    else if (weapon == Weapon.SCISSORS)
                    {
                        // TODO add invulnerability period to enemies so this isn't too OP
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(0.2f);
                    }
                    else if (weapon == Weapon.FAN)
                    {
                        // TODO blow enemies in close range back
                    }
                    else if (weapon == Weapon.MAGNET)
                    {
                        // TODO bring enemies in far range closer
                    }
                }
            }
            else
            {
                //implement shooting mechanincs 
            }
        }
    }

    void Attack()
    {
        attacking = true;
        animator.runtimeAnimatorController = attackAnimator;
        weaponAnimator.SetBool("attack", true);
        float duration = 0.0f;
        switch (weapon)
        {
            case Weapon.CLAW:
            case Weapon.GLOVE:
            case Weapon.GUN:
                duration = 0.2f;
                break;
            case Weapon.SCISSORS:
            case Weapon.MAGNET:
            case Weapon.FAN:
                duration = 0.5f;
                break;
        }
        StartCoroutine(EndAttack(duration));
    }

    IEnumerator EndAttack(float time)
    {
        yield return new WaitForSeconds(time);
        animator.runtimeAnimatorController = normalAnimator;
        weaponAnimator.SetBool("attack", false);
        attacking = false;
    }

    public void rollDice()
    {
        int picked = 0;
        do {
            picked = Random.Range(1, 7);
        } while (picked == (int) weapon);
        weapon = (Weapon) picked;
        weaponAnimator.runtimeAnimatorController = weaponAnimators[(int) weapon];
    }
}
