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

Debug.Log("VECTOR X" + vector3.x.ToString());

    if (vector3.x > (Screen.width * 2/3)){ //Right Third of Screen
        transform.position  = new Vector3(transform.position.x + 1, transform.position.y, 0);
    }else if (vector3.x < (Screen.width * 1/3)){ // Left thrid of Screen
        transform.position  = new Vector3(transform.position.x - 1, transform.position.y, 0);
    }else{
        Roll();
    }
    
    }

    void Roll(){
        this.value = Random.Range(1,7);
    }

    void Update () {

        // Handle native touch events
        foreach (Touch touch in Input.touches) {
            if (touch.phase == TouchPhase.Began){
            this.HandleTouch(touch.position);
            }
        }

        // Simulate touch events from mouse events
        if (Input.touchCount == 0) {
            if (Input.GetMouseButtonDown(0) ) {
                HandleTouch(Input.mousePosition);
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
