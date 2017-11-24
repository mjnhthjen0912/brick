using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour {

    public ParticleSystem[] allParticle;

    void Start()
    {
        allParticle = GetComponentsInChildren<ParticleSystem>();
    }

    public void Play()
    {
        foreach (ParticleSystem pars in allParticle)
        {
            pars.Stop();
            pars.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    
}
