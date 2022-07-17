using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public AudioClip nextWaveSound, winSound;

    public Transform textBox1;
    public Text waveDisplay, goonDisplay;

    // Start is called before the first frame update
    void Start()
    {
        counter = 1;
        kills = 0;
        reqKills = 3;
        enemies = 3;

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
        waveDisplay = GameObject.FindGameObjectWithTag("WaveDisplay").GetComponent<Text>();
        goonDisplay = GameObject.FindGameObjectWithTag("GoonDisplay").GetComponent<Text>();
        textBox1.position = new Vector3(textBox1.position.x, 5000f, textBox1.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        waveDisplay.text = "Wave " + counter;
        goonDisplay.text = "Goons remaining: " + (reqKills - kills);

        if (counter == 10 && kills >= reqKills)
        {
            won = true;
            StopCoroutine(waitAndSpawn());
            StartCoroutine(waitAndWin());
        }

        if (kills >= reqKills && !nextWave && !won)
        {
            StartCoroutine(waitAndSpawn());
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
            enemy.GetComponent<EnemyMelee>().life += (counter - 1);
        }
    }

    IEnumerator waitAndSpawn()
    {
        nextWave = true;
        yield return new WaitForSeconds(5);
        player.GetComponent<playerAttack>().rollDice();
        yield return new WaitForSeconds(5);
        player.GetComponent<playerAttack>().PlaySound(nextWaveSound);
        counter++;
        kills = 0;
        enemies += 2;
        reqKills = enemies;
        if (counter <= 10)
            spawnWave(enemies);
        nextWave = false;
    }

    IEnumerator waitAndWin()
    {
        GameObject.Find("Music").GetComponent<AudioSource>().Stop();
        player.GetComponent<playerAttack>().PlaySound(winSound);
        textBox1.position = new Vector3(textBox1.position.x, 500f, textBox1.position.z);
        yield return new WaitForSeconds(4);
        SceneManager.LoadSceneAsync("menu");
    }
}
