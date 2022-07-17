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
    private bool attacking, visualAttacking;
    public Image weaponDisplay, weaponBackground;
    public Sprite[] weaponSprites;
    public Sprite abilityOn, abilityCooldown;
    public float attackCooldown = 0.5f;
    private AudioSource[] sources;
    public AudioClip[] weaponSounds;

    public GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        sources = transform.GetComponents<AudioSource>();
        animator.runtimeAnimatorController = normalAnimator;
        attacking = false;
        visualAttacking = false;
        weaponDisplay = GameObject.FindGameObjectWithTag("WeaponDisplay").GetComponent<Image>();
        weaponBackground = GameObject.FindGameObjectWithTag("WeaponBackground").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        weaponDisplay.sprite = weaponSprites[(int) weapon];

        if (Input.GetKeyDown(KeyCode.B) && !attacking && weapon != Weapon.NONE)
        {
            Attack();
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
                    if (weapon == Weapon.CLAW && !enemy.gameObject.CompareTag("healthPack"))
                    {
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                        enemy.GetComponent<EnemyMelee>().stun();
                        attacking = false;
                    }
                    else if(weapon == Weapon.GLOVE && !enemy.gameObject.CompareTag("healthPack"))
                    {
                        enemy.GetComponent<EnemyMelee>().knockBack();
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(1);
                        attacking = false;
                    }
                    else if (weapon == Weapon.SCISSORS && !enemy.gameObject.CompareTag("healthPack"))
                    {
                        enemy.GetComponent<EnemyMelee>().ApplyDamage(0.2f);
                    }
                    else if (weapon == Weapon.FAN)
                    {
                        if (enemy.gameObject.CompareTag("healthPack"))
                        {
                            enemy.GetComponent<healthPickup>().fanPush();
                        }
                        else
                        {
                            enemy.GetComponent<EnemyMelee>().fanPush();
                        }
                    }
                    else if (weapon == Weapon.MAGNET)
                    {
                        if (enemy.gameObject.CompareTag("healthPack"))
                        {
                            enemy.GetComponent<healthPickup>().magnetPull();
                        }
                        else
                        {
                            enemy.GetComponent<EnemyMelee>().magnetPull();
                        }
                    }
                }
            }
            else if (visualAttacking)
            {
                GameObject obj = Instantiate(prefab, attackPoint.transform.position, Quaternion.identity);
            }
        }

        if (attacking)
        {
            weaponBackground.sprite = abilityCooldown;
        }
        else
        {
            weaponBackground.sprite = abilityOn;
        }
    }

    void Attack()
    {
        attacking = true;
        visualAttacking = true;
        animator.runtimeAnimatorController = attackAnimator;
        weaponAnimator.SetBool("attack", true);
        PlaySound(weaponSounds[(int)weapon]);
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
        visualAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
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
}
