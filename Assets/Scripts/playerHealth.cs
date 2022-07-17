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
    public bool invincible = false;

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

        for (int i = 0; i < hearts.Length; i++)
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
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            takeDamage();
        }
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    public void takeDamage()
    {
        if (!invincible)
        {
            health -= 1;
            SimpleFlash[] flash = GetComponentsInChildren<SimpleFlash>();
            foreach (SimpleFlash f in flash)
            {
                f.Flash(1f, 2, false);
            }
            StartCoroutine(MakeInvincible(0.5f));
        }
    }

    IEnumerator MakeInvincible(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    void gainHealth()
    {
        health += 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("healthPack") && health < numberOfHearts)
        {
            gainHealth();
            collision.gameObject.GetComponent<healthPickup>().despawn();
        }

        if (collision.gameObject.CompareTag("lantern"))
        {
            //print("touched lantern");
            collision.gameObject.GetComponent<lanternActivation>().bringDownText();
        }
    }
}
