using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public int reqKills = 0;
    public int enemies = 3;
    public static int kills = 0; //whenerver an enemies dies, its death function or whatever will add 1 to kills
    public int counter = 1;

    public GameObject enemy1prefab;
    public GameObject player;
    private IEnumerator coru;
    public bool nextWave = false;
    public bool won = false;
    public AudioClip nextWaveSound;

    // Start is called before the first frame update
    void Start()
    {

        /*
         * surround arena with possible spawn locations, whenever an enemy
         * spawns, it'll be at one of those, choosen randomly?
         * or, spawn enemies randomly in the arena, instead of just outside then them coming in
         * we'll have a variable for 'kills' or maybe 'spawns'
         * which will be the amount of enemies spawned/killed to complete the wave
         * increase after each wave
         * before each wave, roll dice
         * staggered enemy spawning (don't spawn the whole wave at once)?
         * or, spawn the whole wave at once?
         * this might be easier 
         * maybe have a variable that tracks waves
        */
        player.GetComponent<playerAttack>().rollDice();
        reqKills = enemies;
        spawnWave(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (kills >= reqKills && !nextWave && !won)
        {
            StartCoroutine(waitAndSpawn());
        }
        if(counter >= 10)
        {
            won = true;
        }

        bool someoneCanAttack = false;
        EnemyMelee[] enemies = GameObject.FindObjectsOfType<EnemyMelee>();
        foreach (EnemyMelee e in enemies)
        {
            if (e.chance == 1)
                someoneCanAttack = true;
        }
        if (!someoneCanAttack)
        {
            foreach (EnemyMelee e in enemies)
            {
                e.chance = Random.Range(0, 6);
            }
        }
    }


    void spawnWave(int spawns)
    {
        //spawn enemies in arena
        for(int i = 0; i < spawns; i++)
        {
            float randomX = Random.Range(-18f, 18f);
            float randomY = Random.Range(-9f, 9f);
            GameObject enemy = Instantiate(enemy1prefab, new Vector3(randomX, randomY), new Quaternion(0f, 0f, 0f, 0f));
            enemy.GetComponent<EnemyMelee>().life += counter;
        }
    }

    IEnumerator waitAndSpawn()
    {
        player.GetComponent<playerAttack>().PlaySound(nextWaveSound);
        nextWave = true;
        counter++;
        kills = 0;
        enemies += 2;
        yield return new WaitForSeconds(5);
        player.GetComponent<playerAttack>().rollDice();
        yield return new WaitForSeconds(5);
        reqKills = enemies;
        spawnWave(enemies);
        nextWave = false;
    }
}
