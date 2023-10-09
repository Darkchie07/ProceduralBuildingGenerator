using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    public Transform[] controlPoints;  // Define your control points in the Inspector.
    public int resolution = 10;  // Number of vertices along the curve.

    void Start()
    {
        Generate3DObject();
    }

    void Generate3DObject()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[resolution + 1];
        Vector3[] normals = new Vector3[resolution + 1];
        Vector2[] uv = new Vector2[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 vertex = CalculateBezierPoint(t);

            vertices[i] = vertex;
            normals[i] = Vector3.up; // Adjust normals as needed.
            uv[i] = new Vector2(t, 0); // Adjust UV coordinates as needed.
        }

        // Define triangles with the correct number of vertices
        int[] triangles = new int[resolution * 6]; // 6 triangles per segment

        for (int i = 0, ti = 0; i < resolution; i++, ti += 6)
        {
            triangles[ti] = i;
            triangles[ti + 1] = i + 1;
            triangles[ti + 2] = i + resolution + 2;

            triangles[ti + 3] = i + 1;
            triangles[ti + 4] = i + resolution + 1;
            triangles[ti + 5] = i + resolution + 2;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.normals = normals;
        mesh.triangles = triangles;
    }

    Vector3 CalculateBezierPoint(float t)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * controlPoints[0].position +
                    3 * uu * t * controlPoints[1].position +
                    3 * u * tt * controlPoints[2].position +
                    ttt * controlPoints[3].position;

        return p;
    }
}
