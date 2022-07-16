using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerHealth : MonoBehaviour
{
    public int health;

    public int numberOfHearts;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        hearts = GameObject.FindGameObjectsWithTag("Heart");
        //Array.Sort(hearts, CompareObNames);

        if (health > numberOfHearts)
        {
            health = numberOfHearts;
        }

        /*for (int i = 0; i < hearts.Length; i++)
        {

            if (i < health)
            {
                //if they have at least three health, then the other two must be full
                hearts[i].GetComponent<Image>().sprite = fullHeart;
            }
            else
            {
                //hearts more then the heath they have need to be empty
                hearts[i].GetComponent<Image>().sprite = emptyHeart;
            }

            if (i < numberOfHearts)
            {
                hearts[i].GetComponent<Image>().enabled = true;
            }
            else
            {
                hearts[i].GetComponent<Image>().enabled = false;
            }
        }*/
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    void takeDamage()
    {
        health -= 1;
    }

    void gainHealth()
    {
        health += 1;
    }

}
