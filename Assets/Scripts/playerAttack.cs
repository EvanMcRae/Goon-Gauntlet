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
        }
        // TEMP - testing dice roll
        if (Input.GetKeyDown(KeyCode.R))
        {
            rollDice();
        }    
    }

    void Attack()
    {
        // TEMP CODE - gets arm moving
        if (Input.GetKeyDown(KeyCode.B))
        {
            animator.runtimeAnimatorController = attackAnimator;
            weaponAnimator.SetBool("attacking", true);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            animator.runtimeAnimatorController = normalAnimator;
            weaponAnimator.SetBool("attacking", true);
        }
        //StartCoroutine(PerformAttack());
    }

    void rollDice()
    {
        int picked = Random.Range(1, 7);
        weapon = (playerAttack.Weapon) picked;
        weaponAnimator.runtimeAnimatorController = weaponAnimators[(int) weapon];
    }

    IEnumerator PerformAttack()
    {
        yield return null;
    }
}
