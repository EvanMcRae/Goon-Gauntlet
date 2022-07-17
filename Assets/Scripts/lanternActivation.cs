using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanternActivation : MonoBehaviour
{
    public Transform textBox;

    public Transform anchor;
    // Start is called before the first frame update
    void Start()
    {
        textBox.position = new Vector3(textBox.position.x, 5000f, textBox.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bringDownText()
    {
        textBox.position = new Vector3(textBox.position.x, anchor.position.y, textBox.position.z);
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
    }

}
