using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class WaterManager : MonoBehaviour
{
    private MeshFilter meshFilter;
    public float waveHeight = 0.5f;
    public float waveFrequency = 0.5f;
    public float waveLength = 0.75f;
    //Position where the waves originate from
    public Vector3 waveOriginPosition = new Vector3(0.0f, 0.0f, 0.0f);

    public int[] tris;

    Mesh mesh;
    Vector3[] vertices;

    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
        CreateMeshLowPoly(meshFilter);
    }

    private void Update()
    {
        Vector3[] vertices = meshFilter.mesh.vertices;
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].y = WaveManager.instance.GetWaveHeight(transform.position.x + vertices[i].x);
        }

        meshFilter.mesh.vertices = vertices;
        //meshFilter.mesh.RecalculateNormals();
        mesh.SetTriangles(tris, 0);
    }

    MeshFilter CreateMeshLowPoly(MeshFilter mf)
    {
        mesh = mf.sharedMesh;

        //Get the original vertices of the gameobject's mesh
        Vector3[] originalVertices = mesh.vertices;

        //Get the list of triangle indices of the gameobject's mesh
        int[] triangles = mesh.triangles;

        //Create a vector array for new vertices 
        Vector3[] vertices = new Vector3[triangles.Length];

        //Assign vertices to create triangles out of the mesh
        for (int i = 0; i < triangles.Length; i++)
        {
            vertices[i] = originalVertices[triangles[i]];
            triangles[i] = i;
        }
        tris = triangles;
        //Update the gameobject's mesh with new vertices
        mesh.vertices = vertices;
        mesh.SetTriangles(triangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        this.vertices = mesh.vertices;

        return mf;
    }
}
