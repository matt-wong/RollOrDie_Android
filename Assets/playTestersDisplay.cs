using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playTestersDisplay : MonoBehaviour
{

    Text myText;
    int nameIdx = 0;
    string[] names = {"Alicia", "Rocket Rob", "Rob 2", "my Mom, I think...", "Paullison (2)", "Scott", "Geoff with a G", "Alicia's Brothers", "Kevin", "Other Matt"};

    // Start is called before the first frame update
    void Start()
    {
        myText = gameObject.GetComponent<Text>();
        InvokeRepeating("ChangeName", 0f, 2f);
    }

    void ChangeName(){
        if (myText){
            myText.text = names[nameIdx % names.Length];
            nameIdx += 1;
        }
    }
}
