using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{

    private GameObject particleEmitter;
    public int timeBetweenParticles = 1;
    private int particlesEmitted = 0;
    public GameObject ParticleObject;
    public float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        particleEmitter = new GameObject("Particle Emitter");
        particleEmitter.transform.position = new Vector3(0, 10, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        //want to have new particles spawn every second
        if (time > timeBetweenParticles)
        {
            
            GameObject newParticle = Instantiate(ParticleObject, Vector3.zero, transform.rotation);
            newParticle.name = "Particle_" + particlesEmitted;
            particlesEmitted += 1;
            //newParticle.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            //newParticle.transform.position = new Vector3(10, 5, 0);
            time = 0;
        }
        
    }
}
