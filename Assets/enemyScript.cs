using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    const float SPEED = 0.5f;

    int value = 0;

    // Start is called before the first frame update
    void Start()
    {
        TextMesh tm = GetComponentInChildren<TextMesh>();
        this.value = Random.Range(1,7);
        tm.text = this.value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos= transform.position;
        newPos.z = 0;
        newPos.y -= SPEED * Time.deltaTime;
        transform.position = newPos;
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(gameObject);
    }

}
