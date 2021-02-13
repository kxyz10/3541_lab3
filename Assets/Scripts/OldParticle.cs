//Ignore this file, used for exploration

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class OldParticle : MonoBehaviour
{
    //Vector3 velocity;
    //Vector3 force;
    //private Vector3 acceleration = new Vector3(0, 0, 0);
    //int mass;
    //int currentlife;
    //int maxlife;
    //GameObject particle;
    //public int numberOfParticles = 10;


//    GameObject obj;
//    Vector3 velocity;
//    Vector3 force;
//    int mass;
//    int currentlife;
//    int maxlife;
//    IList<Particle> Particles = new List<Particle>();
//    public int numberOfParticles = 10;
//    private Vector3 acceleration = new Vector3(0, 0, 0);
    


//    // Start is called before the first frame update
//    void Start()
//    {
//        for(int i = 0; i < numberOfParticles; i++)
//        {
//            Particle part = new Particle();
//            part.obj.AddComponent(typeof(SphereCollider));
//            part.obj.AddComponent<MeshRenderer>();
//            part.obj.AddComponent<BoxCollider>();
//            part.obj.AddComponent<MeshRenderer>();
//            part.obj.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
//            part.obj.transform.localScale = new Vector3(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
//            Debug.Log("New Particle Made");
//        }
        
//    }

//    private void Move(Particle p)
//    {
//        Vector3 x;
//        x = p.obj.transform.position;
//        Vector3 newVelocity = velocity + acceleration * Time.deltaTime;
//        x = x + (velocity + newVelocity) / 2 * Time.deltaTime;
//        velocity = newVelocity;
//        p.obj.transform.position = x;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        foreach(Particle p in Particles)
//        {
//            Move(p);
//            ApplyForce(new Vector3(0, -1, 0));
//        }
//        foreach(Particle p in Particles)
//        {
//            //p.Update();
//            p.ResetForce();
//        }

//    }

//    void ApplyForce(Vector3 f)
//    {
//        force = force + f; 
//    }

//    void ResetForce()
//    {
//        force = Vector3.zero;
//    }
}
