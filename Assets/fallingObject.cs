using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

abstract public class fallingObject : MonoBehaviour
{

    public float speed = 0f;
    protected gameManager gm;

    // Start is called before the first frame update
    protected void Start()
    {
        gm = gameManager.Instance;
    }

    // Update is called once per frame
    protected void Update()
    {
        Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y -= speed * Time.deltaTime;
        transform.position = newPos;
    }

}
