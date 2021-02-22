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
    public float size;
    public float mass;
    Mesh mesh;
    private GameObject particle;
    // Start is called before the first frame update
    void Start()
    {
        particle = new GameObject("Particle");
        //particle.tag = "Particle";
        createCubeMesh();
        randomSizeCube();
        randomSizeCube();
        setCubeColor(Color.red);
        age = 0;
        maxAge = 10;
        //gives particle a random starting position and rotation
        float x = Random.Range(-2f, 2f);
        float y = Random.Range(-2f, 2f);
        float z = Random.Range(-2f, 2f);
        float w = Random.Range(-2f, 2f);
        particle.transform.position = new Vector3((x + y) / 2, 9, (z + w) / 2);
        particle.transform.rotation = new Quaternion(x, y, z, w);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, -100, 0);
    }

    void setCubeColor(Color color)
    {

        particle.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }

    void createCubeMesh()
    {
        MeshFilter filter = particle.AddComponent<MeshFilter>();
        mesh = filter.mesh;
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

    void randomSizeCube()
    {
        float randomNum = Random.Range(0.5f, 1.0f);
        Vector3 scale = new Vector3(randomNum, randomNum, randomNum);
        particle.transform.localScale -= scale;
        size = scale.x;
        mass = scale.x;
    }

    // Update is called once per frame
    void Update()
    {
        age += 1 * Time.deltaTime;
        if (age >= maxAge)
        {
            Destroy(particle);
        }
        else if (age >= (maxAge * 2) / 3)
        {
            setCubeColor(Color.yellow);
        }
        else if (age >= maxAge / 3)
        {
            setCubeColor(Color.blue);
        }

        //make sure we arent trying to reach a destroyed particle
        if (age < maxAge)
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
        Collide();
    }

    //returns a vector that is added velocity
    public void Collide()
    {
        ArrayList particleList = GameObject.Find("ParticleEmitterObject").GetComponent<ParticleEmitter>().particleList;
        GameObject plane = GameObject.Find("PlaneObject").GetComponent<Plane>().plane;

        // colides with other particle
        for (int i = 0; i < particleList.Count; i++)
        {
            GameObject particle2 = (GameObject)particleList[i];
            if (!particle2.Equals(particle))
            {
                if (particle2.transform.position.x <= particle.transform.position.x + size && particle2.transform.position.x >= particle.transform.position.x - size
                    && particle2.transform.position.y <= particle.transform.position.y + size && particle2.transform.position.x >= particle.transform.position.x - size
                    && particle2.transform.position.z <= particle.transform.position.z + size && particle2.transform.position.z >= particle.transform.position.z - size)
                {
                    //              Vector3 direction = new Vector3(position.x - particle2.transform.position.x, position.y - particle2.transform.position.y, position.z - particle2.transform.position.z); 
                    //              velocity = new Vector3(velocity.x * (0.9f), velocity.y * (0.9f), velocity.z * (0.9f));
                }
            }
        }

        //colides with plane
        if (particle.transform.position.y - size <= plane.transform.position.y && velocity.y < 0)
        {
            //apply forceupward 
            velocity.y = velocity.y * (-0.8f / mass);
        }
        else if (particle.transform.position.y < plane.transform.position.y)
        {
            position.y = plane.transform.position.y + size;
        }


    }

}

