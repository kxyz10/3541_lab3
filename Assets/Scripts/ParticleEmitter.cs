using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{

    private GameObject particleEmitter;
    public int particlesPerTime = 1;
    public Particle particle;

    // Start is called before the first frame update
    void Start()
    {
        particleEmitter = new GameObject("Particle Emitter");
        particleEmitter.transform.position = new Vector3(0, 10, 0);
        
    }

    // Update is called once per frame
    void Update()
    {
        Particle newParticle = Instantiate(particle, new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10)), transform.rotation);
        //particle.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
    }
}
