using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveSpawner : MonoBehaviour
{
    public int reqKills = 0;
    public int enemies = 3;
    public static int kills = 0; //whenerver an enemies dies, its death function or whatever will add 1 to kills
    public int counter = 0;
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

        reqKills = enemies;
        spawnWave(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (kills == reqKills)
        {
            counter++;
            kills = 0;
            enemies += 2;
            reqKills = enemies;
            spawnWave(enemies);
        }
        if(counter == 10)
        {
            print("beat the game");
        }
    }


    void spawnWave(int spawns)
    {
        //spawn enemies in arena
    }

}
