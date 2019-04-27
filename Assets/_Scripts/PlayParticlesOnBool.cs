using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticlesOnBool : MonoBehaviour
{
    ParticleSystem ps;
    public UnityAtoms.BoolVariable ShouldPlay;
    
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    public void PlayParticles(){
        // print("PlayParticles " + ShouldPlay.Value);
        if (ShouldPlay.Value) {
            ps.Play();
        } else {
            ps.Stop();
        }
    }
}
