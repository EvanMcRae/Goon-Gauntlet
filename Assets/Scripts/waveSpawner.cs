using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public int reqKills = 0;
    public int enemies = 3;
    public static int kills = 0; //whenerver an enemies dies, its death function or whatever will add 1 to kills
    public int counter = 0;

    public GameObject enemy1prefab;
    public GameObject player;
    private IEnumerator coru;
    public bool nextWave = false;

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

        coru = waitAndSpawn();
        StartCoroutine(coru);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(kills);

        if (kills >= reqKills && !nextWave)
        {
            StartCoroutine(waitAndSpawn());
        }
        if(counter == 10)
        {
            print("beat the game");
        }
    }


    void spawnWave(int spawns)
    {
        //spawn enemies in arena
        for(int i = 0; i < spawns; i++)
        {
            float randomX = Random.Range(-17f, 20f);
            float randomY = Random.Range(-13f, 8f);
            Instantiate(enemy1prefab, new Vector3(randomX, randomY), new Quaternion(0f, 0f, 0f, 0f));
        }
    }

    IEnumerator waitAndSpawn()
    {
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
