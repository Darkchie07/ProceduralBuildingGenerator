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
        string inputString = "FF(l, w)D+FX(a, l, x)W-FY(a, l, x)FFDFFX(a, l, x)W-FY(a, l, x)B";

        // Use custom split method to split the input string
        string[] components = SplitComponents(inputString);

        // Output the separated components
        foreach (string component in components)
        {
            Debug.Log(component);
        }
    }

    string[] SplitComponents(string input)
    {
        List<string> components = new List<string>();
        string currentComponent = "";
        int parenthesisCount = 0;

        for (int i = 0; i < input.Length; i++)
        {
            char c = input[i];

            if (c == '(')
            {
                parenthesisCount++;
            }
            else if (c == ')')
            {
                parenthesisCount--;
            }

            if ((c == '+' || c == '-') && parenthesisCount == 0)
            {
                components.Add(currentComponent.Trim());
                currentComponent = c.ToString();
            }
            else if (c == 'F')
            {
                if (currentComponent.Length > 0)
                {
                    components.Add(currentComponent.Trim());
                    currentComponent = "F";
                }
                else
                {
                    currentComponent += c;
                }
            }
            else
            {
                currentComponent += c;
            }
        }

        if (!string.IsNullOrEmpty(currentComponent))
        {
            components.Add(currentComponent.Trim());
        }

        return components.ToArray();
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

        float tempAdjacent = CalculateAdjacent(_width, 45);
        float tempHeight = CalculateHeight(_width, 45);

        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, currentPosition.y, currentPosition.z),
            new Vector3(currentPosition.x, currentPosition.y + tempHeight, currentPosition.z + tempAdjacent),
            new Vector3(currentPosition.x + _length, currentPosition.y + tempHeight , currentPosition.z + tempAdjacent),
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
    
    float CalculateAdjacent(float hypotenuse, float angleBaseDegrees)
    {
        // Convert angle from degrees to radians
        float angleBaseRadians = Mathf.Deg2Rad * angleBaseDegrees;

        // Use cosine function to calculate adjacent length
        float adjacentLength = hypotenuse * Mathf.Cos(angleBaseRadians);

        return adjacentLength;
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
