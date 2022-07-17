using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class playerHealth : MonoBehaviour
{
    public int health;

    public int numberOfHearts;

    public GameObject[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;
    public bool invincible = false;
    public bool dead = false;

    public Transform textBox;
    public AudioClip hurtSound, deadSound, pickupSound, lanternSound;
    
    // Start is called before the first frame update
    void Start()
    {
        textBox.position = new Vector3(textBox.position.x, 5000f, textBox.position.z);
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
    }

    int CompareObNames(GameObject x, GameObject y)
    {
        return x.name.CompareTo(y.name);
    }

    public void takeDamage()
    {
        if (!invincible && !dead)
        {
            health -= 1;
            SimpleFlash[] flash = GetComponentsInChildren<SimpleFlash>();
            foreach (SimpleFlash f in flash)
            {
                f.Flash(1f, 2, false);
            }
            StartCoroutine(MakeInvincible(0.5f));

            if(health <= 0)
            {
                StartCoroutine(Die());
                GetComponent<playerAttack>().PlaySound(deadSound);
            }
            else
            {
                GetComponent<playerAttack>().PlaySound(hurtSound);
            }
        }
    }

    IEnumerator MakeInvincible(float time)
    {
        invincible = true;
        yield return new WaitForSeconds(time);
        invincible = false;
    }

    IEnumerator Die()
    {
        dead = true;
        GetComponent<Rigidbody2D>().isKinematic = true;
        GameObject.Find("music").GetComponent<AudioSource>().Stop();
        textBox.position = new Vector3(textBox.position.x, 700f, textBox.position.z);
        yield return new WaitForSeconds(4);
        //GameObject.Destroy(gameObject);
        //yield return new WaitForSeconds(4);
        SceneManager.LoadScene("menu");
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
            GetComponent<playerAttack>().PlaySound(pickupSound);
        }

        if (collision.gameObject.CompareTag("lantern"))
        {
            //print("touched lantern");
            collision.gameObject.GetComponent<lanternActivation>().bringDownText();
            GetComponent<playerAttack>().PlaySound(lanternSound);
        }
    }
}
