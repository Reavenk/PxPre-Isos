using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent( typeof(MeshFilter))]
[RequireComponent( typeof(MeshRenderer))]
public class Test : MonoBehaviour
{
    MarchingCubes marchingCubes = null;

    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Mesh mesh;

    private void Awake()
    {
        this.meshFilter = this.gameObject.GetComponent<MeshFilter>();
        this.meshRenderer = this.gameObject.GetComponent<MeshRenderer>();
        this.mesh = new Mesh();

        this.meshFilter.sharedMesh = this.mesh;
    }

    void Start()
    {
        this.marchingCubes = new MarchingCubes(16, 16, 16);
    }

    
    void Update()
    {
        this.marchingCubes.Clear();


        Vector3 cen = new Vector3(0.5f, 0.5f, 0.5f);
        float rad = 1.0f;

        foreach (MarchingCubes.Coord c in this.marchingCubes.CubeCoords)
        {
            float dstSqr = (c.v3 - cen).sqrMagnitude;
            c.density +=  (1.0f - Mathf.InverseLerp(0.25f, 0.5f, dstSqr)) * 3.0f;
        }

        this.marchingCubes.ExtractMesh(this.mesh);
        this.mesh.RecalculateNormals();
        this.mesh.RecalculateBounds();
    }
}
