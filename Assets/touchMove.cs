using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class touchMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void HandleTouch(Vector3 vector3)
    {
            vector3.z = 0;
            transform.position = vector3;
    }

    void Update () {
        // Handle native touch events
        foreach (Touch touch in Input.touches) {
            this.HandleTouch(Camera.main.ScreenToWorldPoint(touch.position));
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
}

}
