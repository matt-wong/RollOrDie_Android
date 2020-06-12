using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{

public int value = 0;

    // Start is called before the first frame update
    void Start()
    {
        this.value = Random.Range(1,7);
        
        TextMesh tm = GetComponentInChildren<TextMesh>();
        this.value = Random.Range(1,7);
        tm.text = this.value.ToString();
    }

    // Update is called once per frame
    void HandleTouch(Vector3 vector3)
    {

    if (vector3.x > 0f){
        transform.position  = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }else{
        transform.position  = new Vector3(transform.position.x - 1, transform.position.y, 0);
    }
    
    Roll();
    }

    void Roll(){
        this.value = Random.Range(1,7);
    }

    void Update () {

        // Handle native touch events
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began){
            this.HandleTouch(Camera.main.ScreenToWorldPoint(touch.position));
            }
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            }
            // if (Input.GetMouseButton(0) ) {
            //     HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Moved);
            // }
            // if (Input.GetMouseButtonUp(0) ) {
            //     HandleTouch(10, Camera.main.ScreenToWorldPoint(Input.mousePosition), TouchPhase.Ended);
            // }
        }

        TextMesh tm = GetComponentInChildren<TextMesh>();
        tm.text = this.value.ToString();

}

}
