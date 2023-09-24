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

    public void CreateCube(float _lenght = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        
        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
        
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
    }
}
