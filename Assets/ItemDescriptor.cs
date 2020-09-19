using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemDescriptor : MonoBehaviour
{

    private TextMeshProUGUI mytext;
    private float myDeathTimer;
    const float DEATH_TIME = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        mytext = GetComponent<TextMeshProUGUI>();
        myDeathTimer = DEATH_TIME;
    }

    // Update is called once per frame
    void Update()
    {
        myDeathTimer -= Time.deltaTime;
        mytext.faceColor = new Color(0f,0f,0f, myDeathTimer / DEATH_TIME);
    }
}
