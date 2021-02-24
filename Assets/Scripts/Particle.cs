using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    Vector3 position;
    Vector3 velocity;
    Vector3 acceleration;
    public float age;
    public float maxAge;
    public float size;
    public float mass;
    Mesh mesh;
    private GameObject particle;
    Vector3[] vertices;
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
        vertices = new Vector3[]
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
            
            //back
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
        //colides with plane
        if (particle.transform.position.y - 3 <= plane.transform.position.y && velocity.y < 0 && particle.transform.position.x < 10 && particle.transform.position.x > -10 && particle.transform.position.z < 10 && particle.transform.position.z > -10)
        {
            //apply forceupward 
            velocity.y = velocity.y * -0.5f / mass;
        }
        //keep from falling through plane
        else if (particle.transform.position.y < plane.transform.position.y)
        {
            position.y = plane.transform.position.y + size;
        }

        // colides with other particle
        for (int i = 0; i < particleList.Count; i++)
        {
            GameObject obj = (GameObject)particleList[i];
            Particle particle2 = obj.GetComponent<Particle>();
            if (!particle2.Equals(particle))
            {
                //float distance = Mathf.Sqrt(Mathf.Pow(particle.transform.position.x - obj.transform.position.x, 2) + Mathf.Pow(particle.transform.position.y - obj.transform.position.y, 2) + Mathf.Pow(particle.transform.position.z - obj.transform.position.z, 2));
                //if (distance <= 2)
                if(Vector3.Distance(particle.transform.position, particle2.transform.position) <= 2.1f)
                {
                    //Vector3 velocity2 = particle2.velocity;
                    Vector3 save = velocity;

                    if (particle.transform.position.x > particle2.transform.position.x)
                    {
                        velocity = new Vector3(velocity.y, velocity.y, velocity.z);
                        particle2.velocity = new Vector3(-particle2.velocity.y, particle2.velocity.y, particle2.velocity.z);
                    }
                    else
                    {
                        velocity = new Vector3(-velocity.y, velocity.y, velocity.z);
                        particle2.velocity = new Vector3(particle2.velocity.y, particle2.velocity.y, particle2.velocity.z);
                    }

                    if (particle.transform.position.y > particle2.transform.position.y)
                    {
                        velocity = new Vector3(velocity.x, -2*velocity.y, velocity.z);
                        particle2.velocity = new Vector3(particle2.velocity.x, 2*particle2.velocity.y, particle2.velocity.z);
                    }
                    else
                    {
                        velocity = new Vector3(velocity.x, 2*velocity.y, velocity.z);
                        particle2.velocity = new Vector3(particle2.velocity.x, -2*particle2.velocity.y, particle2.velocity.z);
                    }

                    if (particle.transform.position.z > particle2.transform.position.z)
                    {
                        velocity = new Vector3(velocity.x, velocity.y, velocity.y);
                        particle2.velocity = new Vector3(particle2.velocity.x, particle2.velocity.y, -particle2.velocity.y);
                    }
                    else
                    {
                        velocity = new Vector3(velocity.x, velocity.y, -velocity.y);
                        particle2.velocity = new Vector3(particle2.velocity.x, particle2.velocity.y, particle2.velocity.y) ;
                    }
                }
            }
        }

    }

}

