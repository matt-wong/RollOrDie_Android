using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changingColorBGParticles : MonoBehaviour
{

    ParticleSystem[] bgParticles;
    SpriteRenderer[] bgPanels;

    private static Color BG_BLUE = new Color(0.568f, 0.686f, 0.792f);
    private static Color BG_PURPLE = new Color(0.874f, 0.807f, 0.890f);
    private static Color BG_RED = new Color(0.901f, 0.776f, 0.756f);
    private static Color BG_ORANGE = new Color(0.921f, 0.831f, 0.756f);
    private static Color BG_YELLOW = new Color(0.909f, 0.921f, 0.756f);
    private static Color BG_GREEN = new Color(0.756f, 0.921f, 0.8f);

    private Color[] colorSequence = {BG_BLUE, BG_PURPLE, BG_RED, BG_ORANGE, BG_YELLOW, BG_GREEN};
    private int colorCurrentIndex;
    // Start is called before the first frame update
    void Start()
    {
        bgParticles = transform.GetComponentsInChildren<ParticleSystem>();
        bgPanels = transform.Find("BackgroundPanel").GetComponents<SpriteRenderer>();
        colorCurrentIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {

        Color lerpColor = Color.Lerp(bgParticles[0].startColor, colorSequence[this.colorCurrentIndex], Time.deltaTime * 15f);

        foreach(ParticleSystem ps in bgParticles){
            ps.startColor = lerpColor;
        }

        foreach(SpriteRenderer panel in bgPanels){
            panel.color = new Color(lerpColor.r, lerpColor.g, lerpColor.b, panel.color.a);
        }

        if (lerpColor == colorSequence[this.colorCurrentIndex]){
            colorCurrentIndex += 1;
            colorCurrentIndex = colorCurrentIndex % colorSequence.Length; //loop
        }
    }
}
