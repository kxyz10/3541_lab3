using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    float age;
    public float maxAge;
    float mass;
    private GameObject particle;
    private ParticleEmitter emitter;
    private Plane plane;
    // Start is called before the first frame update
    void Start()
    {
        particle = new GameObject("Particle");
        //particle.tag = "Particle";
        createCubeMesh(particle);
        randomSizeCube(particle);
        setCubeColor(particle, Color.red);
        age = 0;
        maxAge = 10;
        //gives particle a random starting position
        //position = particle.transform.position = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f));
        //particle.transform.position = new Vector3(0, 0, 0);
        particle.transform.position = new Vector3(0, 9, 0);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, -1, 0);
    }

    void setCubeColor(GameObject obj, Color color)
    {
        
        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", color); 
    }

    void createCubeMesh(GameObject particle)
    {
        MeshFilter filter = particle.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        //verticies
        Vector3[] vertices = new Vector3[]
        {
            //front
            new Vector3(-1,1,1),    //0: left top front
            new Vector3(1,1,1),     //1: right top front
            new Vector3(-1,-1,1),   //2: left bottom front
            new Vector3(1,-1,1),    //3: right bottom front

            //back
            new Vector3(1,1,-1),    //4: right top back
            new Vector3(-1,1,-1),   //5: left top back
            new Vector3(1,-1,-1),   //6: right bottom back
            new Vector3(-1,-1,-1),   //7: left bottom back
            
            //left
            new Vector3(-1,1,-1),   //8: left top back
            new Vector3(-1,1,1),    //9: left top front
            new Vector3(-1,-1,-1),  //10: left bottom back
            new Vector3(-1,-1,1),    //11: left bottom front

            //right
            new Vector3(1,1,1),     //12: right top front
            new Vector3(1,1,-1),    //13: right top back
            new Vector3(1,-1,1),    //14: right bottom front
            new Vector3(1,-1,-1),   //15: right bottom rback

            //top
            new Vector3(-1,1,-1),    //16: left top back
            new Vector3(1,1,-1),     //17: right top back
            new Vector3(-1,1,1),   //18: left top front
            new Vector3(1,1,1),    //19: right top front

            //bottom
            new Vector3(-1,-1,1),    //20: left bottom front
            new Vector3(1,-1,1),     //21: right bottom front
            new Vector3(-1,-1,-1),   //22: left bottom back
            new Vector3(1,-1,-1)     //23: right bottom back
            
        };

        //triangles
        int[] triangles = new int[]
        {
            //front
            0,2,3,
            3,1,0,

            //back
            4,6,7,
            7,5,4,

            //left
            8,10,11,
            11,9,8,

            //right
            12,14,15,
            15,13,12,

            //top
            16,18,19,
            19,17,16,

            //bottom
            20,22,23,
            23,21,20

        };

        //UVs
        Vector2[] uvs = new Vector2[]
        {
            //front
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),
            
            //bottom
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //left
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //right
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //top
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0),

            //bottom
            new Vector2(0,1),
            new Vector2(0,0),
            new Vector2(1,1),
            new Vector2(1,0)

        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.Optimize();

        //create mesh renderer and material
        MeshRenderer renderer = particle.AddComponent<MeshRenderer>();
        Material material = renderer.material;
    }

    void randomSizeCube(GameObject particle)
    {
        float randomNum = Random.Range(0.5f, 1.0f);
        Vector3 scale = new Vector3(randomNum, randomNum, randomNum);
        //Mesh mesh = particle.GetComponent<MeshFilter>().mesh;
        particle.transform.localScale -= scale;
    }

    // Update is called once per frame
    void Update()
    {
        //if(particle != null)
        //{
        //    ApplyForce(Time.deltaTime);
        //}
        
        age += 1 * Time.deltaTime;
        if(age >= maxAge)
        {
            Destroy(particle);
        }
        else if (age >= (maxAge*2) / 3)
        {
            setCubeColor(particle, Color.yellow);
        }
        else if(age >= maxAge / 3)
        {
            setCubeColor(particle, Color.blue);
        }

        //make sure we arent trying to reach a destroyed particle
        if (age<maxAge)
        {
            ApplyForce(Time.deltaTime);
        }

    }

    //applys forces to the particle
    void ApplyForce(float time)
    {
        //apply gravity
        Vector3 x;
        x = particle.transform.position;
        Vector3 newVelocity = velocity + acceleration * time;
        x += (velocity + newVelocity) / 2 * time;
        velocity = newVelocity;
        particle.transform.position = x;
        Collide(particle);
    }

    //returns a vector that is added velocity
    public void Collide(GameObject particle)
    {
        ArrayList particleList = GameObject.Find("ParticleEmitterObject").GetComponent<ParticleEmitter>().particleList;
        //for each particle see if im close and collide if so

        //see if we hit the ground
        if(particle.transform.position.y < 0)
        {
            //apply upward force
        }

    }

}
