using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class sceneCurtain : MonoBehaviour
{

    Image myImage;
    // Start is called before the first frame update
    void Start()
    {
        myImage = gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myImage.color.a > 0.05){
            this.myImage.color = Color.Lerp( this.myImage.color, Color.clear, Time.deltaTime * 2);
        }else{
            Destroy(gameObject);
        }
    }
}
