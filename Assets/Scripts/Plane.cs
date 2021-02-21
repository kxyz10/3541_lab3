using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    GameObject particle;
    private GameObject plane;
    // Start is called before the first frame update
    void Start()
    {
        plane = new GameObject("Plane");
        createPlaneMesh(plane);
        setPlaneColor(plane, Color.black);
    }

    // Update is called once per frame
    void Update()
    {
        //particle = GameObject.FindGameObjectWithTag("Particle").GetComponent<particle>();
        //Vector3 particlePos = particle.transform.position;
        //if (particlePos.y <= this.transform.position.y)
        //{
        //    particle.Collide(plane);
        //}
    }

    void setPlaneColor(GameObject obj, Color color)
    {

        obj.GetComponent<MeshRenderer>().material.SetColor("_Color", color);
    }


    void createPlaneMesh(GameObject plane)
    {
        MeshFilter filter = plane.AddComponent<MeshFilter>();
        Mesh mesh = filter.mesh;
        mesh.Clear();

        //verticies
        Vector3[] vertices = new Vector3[]
        {
            //bottom left
            new Vector3(-10, 1, -10),

            //bottom right
            new Vector3(10, 1, -10),

            //top right
            new Vector3(-10, 1, 10),

            //top left
            new Vector3(10, 1,10)
        };

        //triangles
        int[] triangles = new int[]
        {
            0, 2, 3,
            3, 1, 0
        };

        //UVs
        Vector2[] uvs = new Vector2[]
        {
            new Vector2 (0, 0),
            new Vector2 (0, 1),
            new Vector2(1, 1),
            new Vector2 (1, 0)
        };



        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;

        mesh.RecalculateNormals();
        mesh.Optimize();

        //create mesh renderer and material
        MeshRenderer renderer = plane.AddComponent<MeshRenderer>();
        Material material = renderer.material;
    }
}
