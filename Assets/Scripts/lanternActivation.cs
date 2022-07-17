using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternActivation : MonoBehaviour
{
    private Transform textBox;

    public Transform textBox1;
    public Transform textBox2;
    public Transform textBox3;
    public Transform textBox4;
    //public Transform anchor;
    private IEnumerator coru;
    // Start is called before the first frame update
    void Start()
    {
        textBox = textBox1;
        textBox.position = new Vector3(textBox.position.x, 5000f, textBox.position.z);
        coru = Wait();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bringDownText()
    {
        if(textBox.position.y > 900f)
        {
            textBox.position = new Vector3(textBox.position.x, 900f, textBox.position.z);
            StartCoroutine(Wait());
        }
    }

    IEnumerator Wait()
    {
        //print("waiting?");
        yield return new WaitForSeconds(4);
        textBox.position = new Vector3(textBox.position.x, 5000f, textBox.position.z);
        if(textBox == textBox1)
        {
            textBox = textBox2;
        }
        else if(textBox == textBox2)
        {
            textBox = textBox3;
        }
        else if(textBox == textBox3)
        {
            textBox = textBox4;
        }
        else if(textBox == textBox4)
        {
            textBox = textBox1;
        }
    }

}
