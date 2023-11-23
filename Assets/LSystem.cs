using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LSystem : MonoBehaviour
{
    public int iterations;
    public float length = 1.0f;

    private string axiom = "FT";
    private string currentString = "";
    private string nextString = "";
    private Vector3 currentPosition;

    public Material colorMaterial;

    void Start()
    {
        GenerateLSystem();
    }

    void GenerateLSystem()
    {
        currentString = axiom;

        currentPosition = transform.position;
        for (int i = 0; i < iterations; i++)
        {
            nextString = "";
            char[] currentChars = currentString.ToCharArray();
            for (int j = 0; j < currentChars.Length; j++)
            {
                char currentChar = currentChars[j];
                Debug.Log(currentChar == 'F');
                if (currentChar == 'F')
                {
                    CreateCubeMesh(1, 1, 1);
                }
                else
                {
                    CreatePyramid(1, 1, 1);
                }
            }
        }
    }
    
    void CreateCubeMesh(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        // Create a new cube GameObject
        GameObject cube = new GameObject("Cube");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;

        float tempLenght = _lenght / 2;
        float tempWidth = _width / 2;
        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
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

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
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

        currentPosition += rotation * new Vector3(length, 0, 0);

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();

        Debug.Log("Cube");
    }
    
    public void CreatePyramid(float _length = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        // Create a new pyramid GameObject
        GameObject pyramid = new GameObject("Pyramid");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = pyramid.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pyramid.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Vector3 currentPosition = transform.position;
        Quaternion rotation = transform.rotation;

        float tempLenght = _length / 2;
        float tempWidth = _width / 2;

        float centerX = tempLenght + (-tempLenght);
        float centerZ = tempWidth + (-tempWidth);
        // Define the vertices of the cube
        Vector3[] pyramidVertices = new Vector3[]
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
        int[] pyramidTriangles = new int[]
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

        currentPosition += rotation * new Vector3(length + _length, 0, 0);

        mesh.vertices = pyramidVertices;
        mesh.triangles = pyramidTriangles;
        // Optionally, calculate normals
        mesh.RecalculateNormals();
    }

    public void CreateUBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();

        Vector3[] vertices = new Vector3[currentString.Length * 20]; // Allocate enough space for vertices
        int[] triangles = new int[currentString.Length * 84]; // Allocate enough space for triangles
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
                // Define the vertices of the cube
                Vector3[] uVertices = new Vector3[]
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

                for (int j = 0; j < uVertices.Length; j++)
                {
                    vertices[vertexIndex] = currentPosition + rotation * uVertices[j];
                    vertexIndex++;
                }

                // Define the triangles to form the cube's faces
                int[] uTriangles = new int[]
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

                for (int j = 0; j < uTriangles.Length; j++)
                {
                    triangles[triangleIndex] = vertexIndex - uVertices.Length + uTriangles[j];
                    triangleIndex++;
                }
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public void CreateLBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.Clear();

        Vector3[] vertices = new Vector3[currentString.Length * 14]; // Allocate enough space for vertices
        int[] triangles = new int[currentString.Length * 60]; // Allocate enough space for triangles
        Vector3 currentPosition = transform.position;
        Quaternion rotation = transform.rotation;
        int vertexIndex = 0;
        int triangleIndex = 0;

        // Define the vertices of the cube
        for (int i = 0; i < currentString.Length; i++)
        {
            char currentChar = currentString[i];

            if (currentChar == 'F')
            {
                Vector3[] LVertices = new Vector3[]
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

                for (int j = 0; j < LVertices.Length; j++)
                {
                    vertices[vertexIndex] = currentPosition + rotation * LVertices[j];
                    vertexIndex++;
                }

                // Define the triangles to form the cube's faces
                int[] LTriangles = new int[]
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

                for (int j = 0; j < LTriangles.Length; j++)
                {
                    triangles[triangleIndex] = vertexIndex - LVertices.Length + LTriangles[j];
                    triangleIndex++;
                }

                currentPosition += rotation * new Vector3(0, 0, length);
            }
        }

        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

    public void CreateRCBuilding(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
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

        for (int i = 0; i < currentString.Length; i++)
        {
            char currentChar = currentString[i];

            if (currentChar == 'F')
            {
                // Define the vertices of the cube
                Vector3[] RCVertices = new Vector3[]
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

                for (int j = 0; j < RCVertices.Length; j++)
                {
                    vertices[vertexIndex] = currentPosition + rotation * RCVertices[j];
                    vertexIndex++;
                }

                // Define the triangles to form the cube's faces
                int[] RCTriangles = new int[]
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

                for (int j = 0; j < RCTriangles.Length; j++)
                {
                    triangles[triangleIndex] = vertexIndex - RCVertices.Length + RCTriangles[j];
                    triangleIndex++;
                }

                currentPosition += rotation * new Vector3(0, 0, length);
            }
        }
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }

}
