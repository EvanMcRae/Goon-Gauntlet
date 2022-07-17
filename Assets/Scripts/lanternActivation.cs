using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternActivation : MonoBehaviour
{
    public Transform textBox;

    //public Transform anchor;
    private IEnumerator coru;
    // Start is called before the first frame update
    void Start()
    {
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
    }

}
