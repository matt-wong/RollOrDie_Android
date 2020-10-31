using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsMaker : MonoBehaviour
{
    public ParticleSystem HeartParticles;
    public AudioClip[] HeartDeathSounds;

    public void HeartEffect(Vector3 point){
        //Heart Effects to show user that they used a heart
        ParticleSystem ps = Instantiate(HeartParticles, point, Quaternion.Euler(0f, 0f, 40));
        ps.Play();
        AudioSource.PlayClipAtPoint(this.HeartDeathSounds[UnityEngine.Random.Range(0, this.HeartDeathSounds.Length)], point); 

    }
}
