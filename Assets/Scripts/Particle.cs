using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    int age;
    int maxAge;
    int mass;
    // Start is called before the first frame update
    void Start()
    {
        GameObject particle = new GameObject("Particle");
        particle.AddComponent(typeof(SphereCollider));
        particle.AddComponent<Rigidbody>();
        particle.AddComponent<MeshRenderer>();
        particle.AddComponent<BoxCollider>();
        particle.AddComponent<MeshRenderer>();
        //particle.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        particle.transform.position = new Vector3(0, 0, 0);
        //particle.transform.localScale = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        particle.transform.localScale = new Vector3(1.0f, 1.0f);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        ApplyForce(Time.deltaTime);
    }

    //applys forces to the particle
    void ApplyForce(float time)
    {
        //apply gravity
        Vector3 x;
        x = transform.position;
        Vector3 newVelocity = velocity + acceleration * time;
        x = x + (velocity + newVelocity) / 2 * time;
        velocity = newVelocity;
        transform.position = x;

        //check for other forces
        Vector3 collsions = Collision();

    }

    //returns a vector that is added velocity
    Vector3 Collision()
    {
        return (new Vector3(0, 0, 0));
    }

}
