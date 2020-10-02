using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleKiller : MonoBehaviour
{

    private ParticleSystem ps;

    // Start is called before the first frame update
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {

        if (ps)
        {
            if (!ps.IsAlive())
            {
                Destroy(gameObject);
            }
        }
    }
}

