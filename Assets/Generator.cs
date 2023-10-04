using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Generator : MonoBehaviour
{
    /*[Header("Set the size")]
    public float length;
    public float width;
    public float height;
    public int jumlahLantai;*/
    private Mesh mesh;
    
    public int numSegments = 32;
    public int numRings = 16;
    public float radius = 1f;
    
    public Transform[] controlPoints;  // Define your control points in the Inspector.
    public int resolution = 10; 

    private void Start()
    {
        Generate3DObject();
    }

    public void CreateCube(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();

        float tempLenght = _lenght / 2;
        float tempWidth = _width / 2;
        // Define the vertices of the cube
        Vector3[] vertices = new Vector3[]
        {
            // Front face
            new Vector3(-(tempLenght), _dasar, -(tempWidth)),
            new Vector3(-(tempLenght), _dasar, tempWidth),
            new Vector3(tempLenght, _dasar, tempWidth),
            new Vector3(tempLenght, _dasar, -(tempWidth)),

            // Back face
            new Vector3(-(tempLenght), _height, -(tempWidth)),
            new Vector3(-(tempLenght), _height, tempWidth),
            new Vector3(tempLenght, _height, tempWidth),
            new Vector3(tempLenght, _height, -(tempWidth)),
        };

        // Define the triangles to form the cube's faces
        int[] triangles = new int[]
        {
            // Bottom face
            0, 2, 1,
            0, 3, 2,

            // Top face
            4, 5, 6,
            4, 6, 7,

            // Front face
            2, 6, 5,
            2, 5, 1,
            
            // Back face
            0, 4, 7,
            0, 7, 3,
            
            // Left face
            0, 1, 5,
            0, 5, 4,
            
            // Right face
            3, 7, 6,
            3, 6, 2,
        };

        // Define UV coordinates (the size matches the number of vertices)
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y); // Simple UV mapping based on vertex positions
        }

        // Assign vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Optionally, calculate normals
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    
    public void CreatePyramid(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        float tempLenght = _lenght / 2;
        float tempWidth = _width / 2;

        float centerX = tempLenght + (-tempLenght);
        float centerZ = tempWidth + (-tempWidth);
        // Define the vertices of the cube
        Vector3[] vertices = new Vector3[]
        {
            // Front face
            new Vector3(-(tempLenght), _dasar, -(tempWidth)),
            new Vector3(-(tempLenght), _dasar, tempWidth),
            new Vector3(tempLenght, _dasar, tempWidth),
            new Vector3(tempLenght, _dasar, -(tempWidth)),

            // Back face
            new Vector3(centerX, _height, centerZ),
        };

        // Define the triangles to form the cube's faces
        int[] triangles = new int[]
        {
            // Bottom face
            0, 2, 1,
            0, 3, 2,

            // Front face
            0, 1, 4,
            
            // Back face
            1, 2, 4,
            
            // Left face
            2, 3, 4,
            
            // Right face
            3, 0, 4,
        };

        // Define UV coordinates (the size matches the number of vertices)
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y); // Simple UV mapping based on vertex positions
        }

        // Assign vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Optionally, calculate normals
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    
    public void CreateUBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // Define the vertices of the cube
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(2, 0, 2), //0 
            new Vector3(1, 0, 2), //1 
            new Vector3(1, 0, 0), //2 
            new Vector3(-1, 0, 0), //3 
            new Vector3(-1, 0, 2), //4 
            new Vector3(-2, 0, 2), //5
            new Vector3(-2, 0, -2), //6
            new Vector3(2, 0, -2), //7 
            new Vector3(1, 0, -2), //8
            new Vector3(-1, 0, -2), //9
            
            new Vector3(2, 2, 2), //10
            new Vector3(1, 2, 2), //11 
            new Vector3(1, 2, 0), //12 
            new Vector3(-1, 2, 0), //13
            new Vector3(-1, 2, 2), //14
            new Vector3(-2, 2, 2), //15 
            new Vector3(-2, 2, -2), //16
            new Vector3(2, 2, -2), //17
            new Vector3(1, 2, -2), //18
            new Vector3(-1, 2, -2), //19
        };

        // Define the triangles to form the cube's faces
        int[] triangles = new int[]
        {
            0, 1, 8,
            0, 8, 7,
            4, 5, 6,
            4, 6, 9,
            2, 3, 9,
            2, 9, 8,
            
            10, 18, 11,
            10, 17, 18,
            14, 16, 15,
            14, 19, 16,
            12, 19, 13,
            12, 18, 19,
            
            0, 11, 1,
            0, 10, 11,
            
            1, 12, 2,
            1, 11, 12,
            
            3, 14, 4,
            3, 13, 14,
            
            2, 13, 3,
            2, 12, 13,
            
            4, 15, 5,
            4, 14, 15,
            
            0, 7, 17,
            0, 17, 10,
            
            5, 16, 6,
            5, 15, 16,
            
            6, 17, 7,
            6, 16, 17,
        };

        // Define UV coordinates (the size matches the number of vertices)
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y); // Simple UV mapping based on vertex positions
        }

        // Assign vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Optionally, calculate normals
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    
    public void CreateLBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // Define the vertices of the cube
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(-2, 0, 2), //0 
            new Vector3(0, 0, 2), //1 
            new Vector3(0, 0, 0), //2 -
            new Vector3(2, 0, 0), //3 -
            new Vector3(2, 0, -2), //4 
            new Vector3(-2, 0, -2), //5
            new Vector3(-2, 0, 0), //6 

            new Vector3(-2, 2, 2), //7 
            new Vector3(0, 2, 2), //8 
            new Vector3(0, 2, 0), //9 -
            new Vector3(2, 2, 0), //10 -
            new Vector3(2, 2, -2), //11 
            new Vector3(-2, 2, -2), //12
            new Vector3(-2, 2, 0), //13
        };

        // Define the triangles to form the cube's faces
        int[] triangles = new int[]
        {
            6, 1, 0,
            2, 1, 6,
            
            4, 3, 6,
            5, 4, 6,
            
            8, 13, 7,
            8, 9, 13,
            
            10, 11, 13,
            11, 12, 13,
            
            0, 1, 8,
            0, 8, 7,
            
            1, 2, 9,
            1, 9, 8,
            
            2, 3, 9,
            3, 10, 9,
            
            3, 4, 10,
            11, 10, 4,
            
            4, 5, 12,
            12, 11, 4,
            
            0, 7, 5,
            7, 12, 5,
        };

        // Define UV coordinates (the size matches the number of vertices)
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y); // Simple UV mapping based on vertex positions
        }

        // Assign vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Optionally, calculate normals
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }
    
    public void CreateRCBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;

        // Define the vertices of the cube
        Vector3[] vertices = new Vector3[]
        {
            new Vector3(0, 0, 2), //0
            new Vector3(2, 0, 2), //1 
            new Vector3(2, 0, -2), //2
            new Vector3(-2, 0, -2), //3 
            new Vector3(-2, 0, 0), //4
            new Vector3(0, 0, 0), //5
            
            new Vector3(0, 2, 2), //6
            new Vector3(2, 2, 2), //7 
            new Vector3(2, 2, -2), //8
            new Vector3(-2, 2, -2), //9 
            new Vector3(-2, 2, 0), //10
            new Vector3(0, 2, 0), //11
        };

        // Define the triangles to form the cube's faces
        int[] triangles = new int[]
        {
            0, 4, 5,
            1, 3, 2,
            0, 5, 1,
            5, 4, 3,
            
            6, 11, 10,
            7, 8, 9,
            6, 7, 11,
            11, 9, 10,
            
            0, 10, 4,
            0, 6, 10,
            
            0, 1 , 7,
            0, 7, 6,
            
            1, 2, 8,
            1, 8, 7,
            
            2, 3, 9,
            2, 9, 8,
            
            3, 4, 10,
            3, 10, 9,
        };

        // Define UV coordinates (the size matches the number of vertices)
        Vector2[] uv = new Vector2[vertices.Length];

        for (int i = 0; i < vertices.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y); // Simple UV mapping based on vertex positions
        }

        // Assign vertices, triangles, and UV coordinates to the mesh
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uv;

        // Optionally, calculate normals
        mesh.RecalculateNormals();
        meshCollider.sharedMesh = mesh;
    }

    public void CreateBall()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();

        Vector3[] vertices = new Vector3[(numSegments + 1) * (numRings + 1)];
        Vector2[] uv = new Vector2[vertices.Length];
        Vector3[] normals = new Vector3[vertices.Length];

        int[] triangles = new int[numSegments * numRings * 6];

        float phiStep = Mathf.PI * 2.0f / numSegments;
        float thetaStep = Mathf.PI / numRings;

        for (int ring = 0; ring <= numRings; ring++)
        {
            for (int segment = 0; segment <= numSegments; segment++)
            {
                float phi = segment * phiStep;
                float theta = ring * thetaStep;

                int index = segment + ring * (numSegments + 1);

                float x = radius * Mathf.Sin(theta) * Mathf.Cos(phi);
                float y = radius * Mathf.Cos(theta);
                float z = radius * Mathf.Sin(theta) * Mathf.Sin(phi);

                vertices[index] = new Vector3(x, y, z);
                uv[index] = new Vector2((float)segment / numSegments, (float)ring / numRings);
                normals[index] = new Vector3(x, y, z).normalized;
            }
        }

        int numTriangles = 0;
        for (int ring = 0; ring < numRings; ring++)
        {
            for (int segment = 0; segment < numSegments; segment++)
            {
                int current = segment + ring * (numSegments + 1);
                int next = current + numSegments + 1;

                triangles[numTriangles++] = current;
                triangles[numTriangles++] = next;
                triangles[numTriangles++] = current + 1;

                triangles[numTriangles++] = next;
                triangles[numTriangles++] = next + 1;
                triangles[numTriangles++] = current + 1;
            }
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.normals = normals;
        mesh.triangles = triangles;
        meshCollider.sharedMesh = mesh;
    }
    
    void Generate3DObject()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        Vector3[] vertices = new Vector3[resolution + 1];
        Vector2[] uv = new Vector2[resolution + 1];

        for (int i = 0; i <= resolution; i++)
        {
            float t = i / (float)resolution;
            Vector3 vertex = CalculateBezierPoint(t);

            vertices[i] = vertex;
            uv[i] = new Vector2(t, 0); // Adjust UV coordinates as needed.
        }

        int[] triangles = new int[resolution * 6];

        for (int i = 0, ti = 0; i < resolution; i++, ti += 6)
        {
            triangles[ti] = i;
            triangles[ti + 1] = i + resolution + 1;
            triangles[ti + 2] = i + 1;

            triangles[ti + 3] = i + 1;
            triangles[ti + 4] = i + resolution + 1;
            triangles[ti + 5] = i + resolution + 2;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        // Recalculate normals for the mesh to ensure correct shading
        mesh.RecalculateNormals();
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
