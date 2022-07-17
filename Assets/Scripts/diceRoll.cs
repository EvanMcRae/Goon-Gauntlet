using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diceRoll : MonoBehaviour
{
    private bool rolling = false;
    public Sprite[] diceSprites;
    public Image img;
    public float delay = 0.1f;
    private float time = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (rolling)
        {
            time += Time.deltaTime;
            if (time >= delay)
            {
                img.sprite = diceSprites[Random.Range(0, 6)];
                time = 0.0f;
            }
        }
    }
    
    public void startRoll()
    {
        rolling = true;
    }

    public void stopRoll(int dice)
    {
        rolling = false;
        img.sprite = diceSprites[dice];
        time = 0.0f;
    }
}
