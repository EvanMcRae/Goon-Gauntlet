using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Image weaponDisplay;
    public Sprite[] weaponSprites;

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        animator.runtimeAnimatorController = normalAnimator;
        attacking = false;
        weaponDisplay = GameObject.FindGameObjectWithTag("WeaponDisplay").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponDisplay.sprite = weaponSprites[(int) weapon];

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
                   // Debug.Log("we hit " + enemy.name);
                    

                    if (weapon == Weapon.CLAW)
                    {
                        // TODO how to differentiate claw?
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                        enemy.GetComponent<EnemyMelee>().stun();
                        attacking = false;
                    }
                    else if(weapon == Weapon.GLOVE)
                    {
                        enemy.GetComponent<EnemyMelee>().knockBack();
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
                        enemy.GetComponent<EnemyMelee>().fanPush();
                    }
                    else if (weapon == Weapon.MAGNET)
                    {
                        enemy.GetComponent<EnemyMelee>().magnetPull();
                    }
                }
            }
            else
            {
                GameObject obj = Instantiate(prefab, attackPoint.transform.position, Quaternion.identity);
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
        if(weapon == Weapon.FAN)
        {
            attackRange = 1.5f;
        }
        else if(weapon == Weapon.SCISSORS || weapon == Weapon.GLOVE || weapon == Weapon.CLAW)
        {
            attackRange = .5f;
        }
        else if (weapon == Weapon.MAGNET)
        {
            attackRange = 3f;
        }
    }
}
