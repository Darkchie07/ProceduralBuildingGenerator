using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    public int iterations = 3;
    public float length = 1.0f;

    private string axiom = "F";
    private string currentString = "";
    private string nextString = "";

    void Start()
    {
        GenerateLSystem();
    }

    void GenerateLSystem()
    {
        currentString = axiom;

        for (int i = 0; i < iterations; i++)
        {
            nextString = "";
            char[] currentChars = currentString.ToCharArray();

            for (int j = 0; j < currentChars.Length; j++)
            {
                char currentChar = currentChars[j];

                if (currentChar == 'F')
                {
                    nextString += "F";
                }
                else
                {
                    nextString += currentChar;
                }
            }

            currentString = nextString;
        }

        CreateCubeMesh();
    }
    
    void CreateCubeMesh()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();

        Vector3[] vertices = new Vector3[currentString.Length * 24]; // Allocate enough space for vertices
        int[] triangles = new int[currentString.Length * 36]; // Allocate enough space for triangles
        Vector3 currentPosition = transform.position;
        Quaternion rotation = transform.rotation;
        int vertexIndex = 0;
        int triangleIndex = 0;

        Debug.Log("Cube");
        for (int i = 0; i < currentString.Length; i++)
        {
            char currentChar = currentString[i];

            if (currentChar == 'F')
            {
                // Create vertices for a cube-like shape
                Vector3[] cubeVertices = new Vector3[]
                {
                    new Vector3(0, 0, 0),
                    new Vector3(length, 0, 0),
                    new Vector3(length, 0, length),
                    new Vector3(0, 0, length),
                    new Vector3(0, length, 0),
                    new Vector3(length, length, 0),
                    new Vector3(length, length, length),
                    new Vector3(0, length, length),
                };

                for (int j = 0; j < cubeVertices.Length; j++)
                {
                    vertices[vertexIndex] = currentPosition + rotation * cubeVertices[j];
                    vertexIndex++;
                }

                // Define triangles for the cube
                int[] cubeTriangles = new int[]
                {
                    0, 1, 2, 0, 2, 3, // Bottom face
                    4, 6, 5, 4, 7, 6, // Top face
                    0, 5, 1, 0, 4, 5, // Side faces
                    1, 6, 2, 1, 5, 6,
                    2, 7, 3, 2, 6, 7,
                    3, 4, 0, 3, 7, 4
                };

                for (int j = 0; j < cubeTriangles.Length; j++)
                {
                    triangles[triangleIndex] = vertexIndex - cubeVertices.Length + cubeTriangles[j];
                    triangleIndex++;
                }

                currentPosition += rotation * new Vector3(0, 0, length);
            }
            else if (currentChar == '+')
            {
                // Rotate to the right
                rotation *= Quaternion.Euler(0, 90, 0);
            }
            else if (currentChar == '-')
            {
                // Rotate to the left
                rotation *= Quaternion.Euler(0, -90, 0);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
    public void CreatePyramid(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();
        
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
}
