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
    public ArrayList particleList = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        particleEmitter = new GameObject("Particle Emitter");
        particleEmitter.transform.position = new Vector3(0, 10, 0);
        createPyramidMesh(particleEmitter);
        setPyramidColor(particleEmitter, Color.cyan);


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
            particleList.Add(newParticle);
            //newParticle.transform.position = new Vector3(Random.Range(0, 10), Random.Range(0, 10), Random.Range(0, 10));
            //newParticle.transform.position = new Vector3(10, 5, 0);
            time = 0;
        }
        //HandleAllParticleCollisions();
    }


    //void HandleAllParticleCollisions()
    //{
    //    for(int i = 0; i < Particles.Count; i++)
    //    {
    //        for(int j = i+1; j < Particles.Count; j++)
    //        {
    //            Particles[i].
    //        }
    //    }
    //}


    void createPyramidMesh(GameObject particleEmitter)
    {
        MeshFilter filter = particleEmitter.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        //verticies
        Vector3[] vertices = new Vector3[]
        {
            //front
            new Vector3(-1,1,1),    //0: left top front
            new Vector3(1,1,1),     //1: right top front
            new Vector3(0,-1,0),   //2: left bottom front
            new Vector3(0,-1,0),    //3: right bottom front

            //back
            new Vector3(1,1,-1),    //4: right top back
            new Vector3(-1,1,-1),   //5: left top back
            new Vector3(0,-1,0),   //6: right bottom back
            new Vector3(0,-1,0),   //7: left bottom back
            
            //left
            new Vector3(-1,1,-1),   //8: left top back
            new Vector3(-1,1,1),    //9: left top front
            new Vector3(0,-1,0),  //10: left bottom back
            new Vector3(0,-1,0),    //11: left bottom front

            //right
            new Vector3(1,1,1),     //12: right top front
            new Vector3(1,1,-1),    //13: right top back
            new Vector3(0,-1,0),    //14: right bottom front
            new Vector3(0,-1,0),   //15: right bottom rback

            //top
            new Vector3(-1,1,-1),    //16: left top back
            new Vector3(1,1,-1),     //17: right top back
            new Vector3(-1,1,1),   //18: left top front
            new Vector3(1,1,1),    //19: right top front

            //bottom
            new Vector3(0,-1,0),    //20: left bottom front
            new Vector3(0,-1,0),     //21: right bottom front
            new Vector3(0,-1,0),   //22: left bottom back
            new Vector3(0,-1,0)     //23: right bottom back

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
        MeshRenderer renderer = particleEmitter.AddComponent<MeshRenderer>();
        Material material = renderer.material;
    }

    void setPyramidColor(GameObject obj, Color color)
    {

        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }
}
