using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

using UnityEngine;

public class LSystemHouseGenerator : MonoBehaviour
{
    private string axiom = "F";
    private string currentString;
    private int iterations = 3; // Adjust the number of iterations

    private float wallLength = 5f;
    private float doorWidth = 1f;
    private float windowWidth = 1f;
    private float windowHeight = 1f;
    
    
    private Vector3 currentPosition;

    // Start is called before the first frame update
    void Start()
    {
        currentPosition = Vector3.zero;
        CreateXCube(4, 5);
        CreateZCube(2, 3);
    }

    IEnumerator GenerateLSystem()
    {
        currentString = axiom;

        for (int i = 0; i < iterations; i++)
        {
            currentString = ApplyRules(currentString);
            yield return null; // Wait for one frame
        }

        InterpretLSystem();
    }

    string ApplyRules(string input)
    {
        StringBuilder result = new StringBuilder();

        foreach (char c in input)
        {
            // Apply L-system rules
            switch (c)
            {
                case 'F':
                    result.Append("F+F+F+F-F-F-F-F+F");
                    break;
                default:
                    result.Append(c);
                    break;
            }
        }

        return result.ToString();
    }

    void InterpretLSystem()
    {
        foreach (char c in currentString)
        {
            // Interpret L-system symbols and create corresponding geometry
            switch (c)
            {
                case 'F':
                    // Move forward and instantiate a wall
                    transform.Translate(Vector3.forward * wallLength);
                    break;
                case '+':
                    // Rotate clockwise
                    transform.Rotate(Vector3.up * 90f);
                    break;
                case '-':
                    // Rotate counterclockwise
                    transform.Rotate(Vector3.up * -90f);
                    break;
            }
        }
    }
    
    void UpdatePosition(Vector3 lastPos)
    {
        Quaternion rotation = transform.rotation;
        Debug.Log(currentPosition);
        currentPosition = lastPos;
    }

    void CreateXCube(float _length = 0f, float _width = 0f, Material colorMaterial = null)
    {
        // Create a new cube GameObject
        GameObject cube = new GameObject("Cube");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material.color = Color.red;

        Quaternion rotation = transform.rotation;

        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z),
            new Vector3(currentPosition.x + _length, currentPosition.y + _width , currentPosition.z),
            new Vector3(currentPosition.x + _length, currentPosition.y, currentPosition.z),
        };

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
        {
            // Bottom face
            0, 1, 2,
            0, 2, 3,
        };
        
        UpdatePosition(new Vector3(currentPosition.x + _length, currentPosition.y, currentPosition.z));

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();
    }
    
    void CreateZCube(float _length = 0f, float _width = 0f, Material colorMaterial = null)
    {
        // Create a new cube GameObject
        GameObject cube = new GameObject("Cube");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material.color = Color.red;

        Quaternion rotation = transform.rotation;

        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z + _length),
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + _length),
        };

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
        {
            // Bottom face
            0, 1, 2,
            0, 2, 3,
        };
        
        UpdatePosition(new Vector3(currentPosition.x + _length, currentPosition.y, currentPosition.z));

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();
    }
    
    void CreateTriangles(float _length = 0f, float _width = 0f, Material colorMaterial = null)
    {
        // Create a new cube GameObject
        GameObject cube = new GameObject("Cube");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material.color = Color.red;

        Quaternion rotation = transform.rotation;

        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z + _length),
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + _length),
        };

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
        {
            // Bottom face
            0, 1, 2,
            0, 2, 3,
        };
        
        UpdatePosition(new Vector3(currentPosition.x + _length, currentPosition.y, currentPosition.z));

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();
    }
    
    void CreateTiltCube(float _length = 0f, float _width = 0f, Material colorMaterial = null)
    {
        // Create a new cube GameObject
        GameObject cube = new GameObject("Cube");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = cube.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = cube.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material.color = Color.red;

        Quaternion rotation = transform.rotation;

        CalculateHeight(_length, 25);

        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + _width , currentPosition.z + _length),
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z + _length),
        };

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
        {
            // Bottom face
            0, 1, 2,
            0, 2, 3,
        };
        
        UpdatePosition(new Vector3(currentPosition.x + _length, currentPosition.y, currentPosition.z));

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();
    }
    
    float CalculateHeight(float hypotenuse, float angleBaseDegrees)
    {
        // Convert angle from degrees to radians
        float angleBaseRadians = Mathf.Deg2Rad * angleBaseDegrees;

        // Use cosine function to calculate base length
        float baseLength = hypotenuse * Mathf.Cos(angleBaseRadians);

        // Use tangent function to calculate height
        float height = baseLength * Mathf.Tan(angleBaseRadians);

        return height;
    }
}
