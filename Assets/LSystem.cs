using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Color = System.Drawing.Color;

public class LSystem : MonoBehaviour
{
    public int iterations;
    public float length = 1.0f;

    public string axiom = "FTFTUF";
    private string currentString = "";
    private string nextString = "";
    private Vector3 currentPosition;

    public List<char> initialShape;
    public List<int> floorNum;
    public List<char> roofType;
    public List<float> ParamLength;
    public List<float> ParamWidth;
    public List<float> ParamHeight;
    public List<float> ParamInnerLength;
    public List<float> ParamInnerWidth;
    
    List<char> validShape = new List<char> { 'A', 'B', 'C', 'D' };
    List<char> validRoof = new List<char> { 'X', 'Y' };

    public GameObject prefabs;

    public Material colorMaterial;
    [SerializeField] private bool isStair = true;

    void Start()
    {
        currentPosition = Vector3.zero;
        CreateCubeMesh(4,3,5);
        CreateUBuilding(5,3,5,2,1);
        CreateLBuilding(5,3,5,2,1);
        CreateRCBuilding(5,3,5,2,1);
    }
    public void SetParameter(List<char> _initialShape, List<int> _floorNum, List<char> _roofType, List<float> _paramLength, List<float> _paramWidth, List<float> _paramHeight, List<float> _paramInnerLength, List<float> _paramInnerWidth)
    {
        initialShape = _initialShape;
        floorNum = _floorNum;
        roofType = _roofType;
        ParamLength = _paramLength;
        ParamWidth = _paramWidth;
        ParamHeight = _paramHeight;
        ParamInnerLength = _paramInnerLength;
        ParamInnerWidth = _paramInnerWidth;
    }

    void GenerateLSystem()
    {
        currentPosition = transform.position;
        for (int i = 0; i < initialShape.Count; i++)
        {
            float tempheight = ParamHeight[i];
            float tempDasar = 0;
            if (initialShape[i] == 'A')
            {
                for (int j = 0; j < floorNum[i]; j++)
                {
                    CreateCubeMesh(ParamLength[i], ParamWidth[i], tempheight, tempDasar);
                    tempheight += ParamHeight[i];
                    tempDasar += ParamHeight[i];
                }

                if (roofType[i] == 'X')
                {
                    CreatePyramid(ParamLength[i], ParamWidth[i], tempheight, tempDasar);
                }
                UpdatePosition(new Vector3(ParamLength[i], 0, currentPosition.z));
            }else if (initialShape[i] == 'B')
            {
                for (int j = 0; j < floorNum[i]; j++)
                {
                    CreateUBuilding(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                    tempheight += ParamHeight[i];
                    tempDasar += ParamHeight[i];
                }

                if (roofType[i] == 'X')
                {
                    Uroof1(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }else if(roofType[i] == 'Y')
                {
                    Uroof2(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }
                UpdatePosition(new Vector3(ParamLength[i], 0, currentPosition.z));
            }else if (initialShape[i] == 'C')
            {
                for (int j = 0; j < floorNum[i]; j++)
                {
                    CreateLBuilding(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                    tempheight += ParamHeight[i];
                    tempDasar += ParamHeight[i];
                }

                if (roofType[i] == 'X')
                {
                    LRoof1(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }else if(roofType[i] == 'Y')
                {
                    LRoof2(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }
                UpdatePosition(new Vector3(ParamLength[i], 0, currentPosition.z));
            }else if (initialShape[i] == 'D')
            {
                for (int j = 0; j < floorNum[i]; j++)
                {
                    CreateRCBuilding(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                    tempheight += ParamHeight[i];
                    tempDasar += ParamHeight[i];
                }

                if (roofType[i] == 'X')
                {
                    RCRoof1(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }else if(roofType[i] == 'Y')
                {
                    RCRoof2(ParamLength[i], ParamWidth[i], tempheight, ParamInnerLength[i], ParamInnerWidth[i], tempDasar);
                }
                UpdatePosition(new Vector3(ParamLength[i], 0, currentPosition.z));
            }
        }
        // currentPosition = transform.position;
        // for (int i = 0; i < iterations; i++)
        // {
        //     nextString = "";
        //     char[] currentChars = currentString.ToCharArray();
        //     for (int j = 0; j < currentChars.Length; j++)
        //     {
        //         char currentChar = currentChars[j];
        //         Debug.Log(currentChar == 'F');
        //         if (currentChar == 'F')
        //         {
        //             CreateCubeMesh(3, 1, 1);
        //         }
        //         else if (currentChar == 'T')
        //         {
        //             CreatePyramid(3, 2, 1);
        //         }
        //         else if (currentChar == 'U')
        //         {
        //             CreateUBuilding(3, 3, 1, 1, 1, 0);
        //         }else if (currentChar == 'L')
        //         {
        //             CreateLBuilding(3, 3, 1, 1, 1, 0);
        //         }else if (currentChar == 'R')
        //         {
        //             CreateRCBuilding(3, 3, 1, 1, 1, 0);
        //         }
        //         else if (currentChar == 'A')
        //         {
        //             RCRoof2(3, 3, 1, 1, 1, 0);
        //         }
        //     }
        // }
        // string prefabPath = "Assets/Prefabs/" + gameObject.name + ".prefab";
        // PrefabUtility.SaveAsPrefabAsset(gameObject, prefabPath);
        // Debug.Log("Prefab saved at: " + prefabPath);
    }

    void UpdatePosition(Vector3 lastPos)
    {
        Quaternion rotation = transform.rotation;
        Debug.Log(currentPosition);
        currentPosition += rotation * lastPos;
    }

    void CreateCubeMesh(float _length = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f, float _doorWidth = 0f)
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

        float tempLenght = currentPosition.x + _length;  
        float doorStart = (currentPosition.x + _length)/2;
        // Define the vertices of the cube
        Vector3[] cubeVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, _dasar, currentPosition.z),
            new Vector3(tempLenght, _dasar, currentPosition.z),
            new Vector3(tempLenght, _dasar, _width),
            new Vector3(currentPosition.x, _dasar, _width),

            // Back face
            new Vector3(currentPosition.x, _height, currentPosition.z),
            new Vector3(tempLenght, _height, currentPosition.z),
            new Vector3(tempLenght, _height, _width),
            new Vector3(currentPosition.x, _height, _width),
        };

        // Define triangles for the cube
        int[] cubeTriangles = new int[]
        {
            // Bottom face
            0, 1, 2,
            0, 2, 3,

            // Top face
            4, 6, 5,
            4, 7, 6,

            // Front face
            2, 5, 6,
            2, 1, 5,
            
            // Back face
            0, 7, 4,
            0, 3, 7,
            
            // Left face
            0, 5, 1,
            0, 4, 5,
            
            // Right face
            3, 6, 7,
            3, 2, 6,
        };

        Debug.Log(tempLenght);
        
        Vector3[] trimmedArray = TrimArray(cubeVertices, 4);
        AddProceduralDoor(cubeVertices[0], cubeVertices[1], _height, cube);
        AddProceduralWindow(trimmedArray, _height, cube, cubeVertices[0], cubeVertices[1]);

        mesh.vertices = cubeVertices;
        mesh.triangles = cubeTriangles;
        mesh.RecalculateNormals();
    }
    
    void AddProceduralDoor(Vector3 doorStart, Vector3 doorEnd, float _height, GameObject parent)
    {
        float doorHeight = 0;
        float maxDoorWidth = 3f;
        // Create a new door GameObject
        GameObject doorObject = new GameObject("Door");
        doorObject.transform.SetParent(parent.transform);

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = doorObject.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = doorObject.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material.color = UnityEngine.Color.black; // You can change this material as needed

        float t1 = 0.25f;
        float t3 = 0.75f;
        
        Vector3 q1 = InterpolateVectors(doorStart, doorEnd, t1);
        Vector3 q3 = InterpolateVectors(doorStart, doorEnd, t3);
        float h3 = _height * 0.75f;

        if (h3 < 5f)
        {
            doorHeight = h3;
        }
        else
        {
            doorHeight = 5;
        }
        
        Debug.Log(Vector3.Distance(q3, q1) > maxDoorWidth);
        Vector3 midpoint = (doorStart + doorEnd) / 2.0f;
        Vector3[] doorVertices = new Vector3[4];
        
        if(Vector3.Distance(q3, q1) < maxDoorWidth)
        {
            doorVertices = new Vector3[]
            {
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y, currentPosition.z - 0.001f),
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + doorHeight, currentPosition.z  - 0.001f),
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + doorHeight, currentPosition.z  - 0.001f),
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y, currentPosition.z  - 0.001f),
            };
        }
        else
        {
            doorVertices = new Vector3[]
            {
                new Vector3(q1.x, currentPosition.y, currentPosition.z - 0.001f),
                new Vector3(q1.x - (maxDoorWidth/2), currentPosition.y + doorHeight, currentPosition.z  - 0.001f),
                new Vector3(q3.x + (maxDoorWidth/2), currentPosition.y + doorHeight, currentPosition.z  - 0.001f),
                new Vector3(q3.x + (maxDoorWidth/2), currentPosition.y, currentPosition.z  - 0.001f),
            };
        }
        
        // Define the vertices for the door

        // Define triangles for the door
        int[] doorTriangles = new int[]
        {
            0, 1, 2,
            0, 2, 3,
        };

        if (isStair)
        {
            GameObject stairObject = new GameObject("Stair");
            stairObject.transform.SetParent(doorObject.transform);

            // Add MeshFilter and MeshRenderer components
            MeshFilter meshFilterStair = stairObject.AddComponent<MeshFilter>();
            MeshRenderer meshRendererStair = stairObject.AddComponent<MeshRenderer>();

            Mesh meshStair = new Mesh();
            meshFilterStair.mesh = meshStair;
            meshRendererStair.material.color = UnityEngine.Color.blue;
            
            Vector3[] stairVertices = new Vector3[]
            {
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y, currentPosition.z - 6f), //0
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y, currentPosition.z  - 6f), //1
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 0.75f, currentPosition.z - 6f), //2
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 0.75f, currentPosition.z  - 6f), //3
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 0.75f, currentPosition.z - 4f), //4
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 0.75f, currentPosition.z  - 4f), //5
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 1.5f, currentPosition.z - 4f), //6
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 1.5f, currentPosition.z  - 4f), //7
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 1.5f, currentPosition.z - 2f), //8
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 1.5f, currentPosition.z  - 2f), //9
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 2.25f, currentPosition.z - 2f), //10
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 2.25f, currentPosition.z  - 2f), //11
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y + 2.25f, currentPosition.z - 0.001f), //12
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y + 2.25f, currentPosition.z  - 0.001f), //13
                new Vector3(midpoint.x - (maxDoorWidth/2), currentPosition.y, currentPosition.z - 0.001f), //14
                new Vector3(midpoint.x + (maxDoorWidth/2), currentPosition.y, currentPosition.z  - 0.001f), //15
            };
            
            int[] stairTriangles = new int[]
            {
                0, 3, 1,
                0, 2, 3,
                
                2, 5, 3,
                2, 4, 5,
                
                4, 7, 5,
                4, 6, 7,
                
                6, 9, 7,
                6, 8, 9,
                
                8, 11, 9,
                8, 10, 11,
                
                10, 13, 11,
                10, 12, 13,
                
                0, 14, 2,
                2, 14, 4,
                4, 14, 6,
                6, 14, 8,
                8, 14, 10,
                10, 14, 12,
                
                1, 3, 15,
                3, 5, 15,
                5, 7, 15, 
                7, 9, 15,
                9, 11, 15,
                11, 13, 15,
            };
            
            meshStair.vertices = stairVertices;
            meshStair.triangles = stairTriangles;
            mesh.RecalculateNormals();
            Debug.Log(stairVertices[13]);
        }

        mesh.vertices = doorVertices;
        mesh.triangles = doorTriangles;
        mesh.RecalculateNormals();
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

        Quaternion rotation = transform.rotation;

        float tempLenght = currentPosition.x + _length;  
        float tempLengthX = _length / 2;
        float tempWidth = _width / 2;

        float centerX = tempLengthX + currentPosition.x;
        float centerZ = tempWidth + currentPosition.z;

        Vector3[] pyramidVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, _dasar, currentPosition.z),
            new Vector3(currentPosition.x, _dasar, _width),
            new Vector3(tempLenght, _dasar, _width),
            new Vector3(tempLenght, _dasar, currentPosition.z),

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

        Debug.Log(tempLenght);

        mesh.vertices = pyramidVertices;
        mesh.triangles = pyramidTriangles;
        // Optionally, calculate normals
        mesh.RecalculateNormals();
    }
    
    public void CRoof2(float _length = 0f, float _width = 0f, float _height = 0f, float _dasar = 0f)
    {
        // Create a new pyramid GameObject
        GameObject pyramid = new GameObject("Pyramid");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = pyramid.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = pyramid.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;

        float tempLenght = currentPosition.x + _length;  
        float tempLengthX = _length / 2;
        float tempWidth = _width / 2;

        float centerX = tempLengthX + currentPosition.x;
        float centerZ = tempWidth + currentPosition.z;

        Vector3[] pyramidVertices = new Vector3[]
        {
            // Front face
            new Vector3(currentPosition.x, _dasar, currentPosition.z),
            new Vector3(currentPosition.x, _dasar, _width),
            new Vector3(tempLenght, _dasar, _width),
            new Vector3(tempLenght, _dasar, currentPosition.z),

            // Back face
            new Vector3(currentPosition.x, _height, centerZ),
            new Vector3(tempLenght, _height, centerZ),
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

        Debug.Log(tempLenght);

        mesh.vertices = pyramidVertices;
        mesh.triangles = pyramidTriangles;
        // Optionally, calculate normals
        mesh.RecalculateNormals();
    }

    public void CreateUBuilding(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        // Create a new pyramid GameObject
        GameObject UBuilding = new GameObject("UBuilding");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = UBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = UBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;

        float tempInnerLength = (_length - _innerLength) / 2;

        // Define the vertices of the cube
        Vector3[] uVertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _innerWidth), //2 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _innerWidth), //3 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z), //4 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //5
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //6
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //7 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _width), //8
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _width), //9
        
            new Vector3(currentPosition.x, _height, currentPosition.z), //10 
            new Vector3(currentPosition.x + tempInnerLength, _height, currentPosition.z), //11 
            new Vector3(currentPosition.x + tempInnerLength, _height, currentPosition.z + _innerWidth), //12 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _height, currentPosition.z + _innerWidth), //13 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _height, currentPosition.z), //14 
            new Vector3(currentPosition.x + _length, _height, currentPosition.z), //15
            new Vector3(currentPosition.x + _length, _height, currentPosition.z + _width), //16
            new Vector3(currentPosition.x, _height, currentPosition.z + _width), //17 
            new Vector3(currentPosition.x + tempInnerLength, _height, currentPosition.z + _width), //18
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _height, currentPosition.z + _width), //19
        };

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

        mesh.vertices = uVertices;
        mesh.triangles = uTriangles;
        mesh.RecalculateNormals();
        
        Vector3[] trimmedArray = TrimArray(uVertices, 8);
        
        AddProceduralDoor(uVertices[2], uVertices[3], _height, UBuilding);
        AddProceduralWindow(trimmedArray, _height, UBuilding, uVertices[2], uVertices[3]);
    }
    
    public void AddProceduralWindow(Vector3[] positionPoint, float _height, GameObject parent, Vector3 doorStart = default, Vector3 doorEnd = default)
    {
        for (int i = 0; i < positionPoint.Length; i++)
        {
            Vector3 firstNumber = positionPoint[i];
            Vector3 secondNumber = positionPoint[(i + 1) % positionPoint.Length];

            if (doorStart != firstNumber && doorEnd != secondNumber)
            {
                // Create a new window GameObject
                GameObject windowObject = new GameObject("Window");

                // Add MeshFilter and MeshRenderer components to the window
                MeshFilter windowMeshFilter = windowObject.AddComponent<MeshFilter>();
                MeshRenderer windowMeshRenderer = windowObject.AddComponent<MeshRenderer>();

                Mesh windowMesh = new Mesh();
                windowMeshFilter.mesh = windowMesh;
                windowMeshRenderer.material.color = UnityEngine.Color.blue; // Use the material you want for the window


                float t1 = 0.25f;
                float t3 = 0.75f;


                Vector3 q1 = InterpolateVectors(firstNumber, secondNumber, t1);
                Vector3 q3 = InterpolateVectors(firstNumber, secondNumber, t3);

                Vector3[] windowVertices = new Vector3[]
                {
                    // Front face
                    new Vector3(q1.x, _height * 0.25f, q1.z),
                    new Vector3(q1.x, _height * 0.75f, q1.z),
                    new Vector3(q3.x, _height * 0.75f, q3.z),
                    new Vector3(q3.x, _height * 0.25f, q3.z),
                };
                // Define the triangles for the window
                int[] windowTriangles = new int[]
                {
                    0, 1, 2,
                    0, 2, 3,
                };

                Vector3 normal = Vector3
                    .Cross(windowVertices[1] - windowVertices[0], windowVertices[2] - windowVertices[0]).normalized;

                // Assume the camera is looking along the positive z-axis
                Vector3 viewDirection = Vector3.forward;

                // Assume the camera's right direction is along the positive x-axis
                Vector3 rightDirection = Vector3.right;

                // Calculate the dot products
                float dotProductRight = Vector3.Dot(normal, rightDirection);
                float dotProductView = Vector3.Dot(normal, viewDirection);

                if (dotProductRight > 0)
                {
                    windowObject.transform.position += new Vector3(0.001f, 0, 0);
                }
                else if (dotProductRight < 0)
                {
                    windowObject.transform.position -= new Vector3(0.001f, 0, 0);
                }

                if (dotProductView > 0)
                {
                    windowObject.transform.position += new Vector3(0, 0, 0.001f);
                }
                else if (dotProductView < 0)
                {
                    windowObject.transform.position -= new Vector3(0, 0, 0.001f);
                }

                windowMesh.vertices = windowVertices;
                windowMesh.triangles = windowTriangles;
                windowMesh.RecalculateNormals();

                // Make the window a child of the UBuilding
                windowObject.transform.parent = parent.transform;
            }
        }
    }
    
    Vector3 InterpolateVectors(Vector3 start, Vector3 end, float t)
    {
        return (1 - t) * start + t * end;
    }

    public void CreateLBuilding(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject LBuilding = new GameObject("LBuilding");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = LBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = LBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;
        
        Vector3[] LVertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //1
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //2 
            new Vector3(currentPosition.x + _innerLength, _dasar, _width), //3 -
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + (_width - _innerWidth)), //4 -
            new Vector3(currentPosition.x, _dasar, currentPosition.z + (_width - _innerWidth)), //5 

            new Vector3(currentPosition.x, _height, currentPosition.z), //6
            new Vector3(currentPosition.x + _length, _height, currentPosition.z), //7
            new Vector3(currentPosition.x + _length, _height, currentPosition.z + _width), //8
            new Vector3(currentPosition.x + _innerLength, _height, _width), //9 -
            new Vector3(currentPosition.x + _innerLength, _height, currentPosition.z + (_width - _innerWidth)), //10 -
            new Vector3(currentPosition.x, _height, currentPosition.z + (_width - _innerWidth)), //11
        };

        // Define the triangles to form the cube's faces
        int[] LTriangles = new int[]
        {
            1, 2, 0,
            3, 4, 2,
            4, 5 ,0,
            4, 8, 2,
            
            7, 6, 8,
            9, 8, 10,
            10, 6, 11,
            10, 8, 6,

            0, 7, 1,
            0, 6, 7,

            1, 8, 2,
            1, 7, 8,

            2, 8, 3,
            3, 8, 9,

            3, 9, 4,
            10, 4, 9,

            4, 11, 5,
            11, 4, 10,

            6, 0, 5,
            6, 5, 11,
        };
        
        Vector3[] trimmedArray = TrimArray(LVertices, 6);
        
        AddProceduralWindow(trimmedArray, _height, LBuilding, LVertices[0], LVertices[1]);
        AddProceduralDoor(LVertices[0], LVertices[1], _height, LBuilding);

        mesh.vertices = LVertices;
        mesh.triangles = LTriangles;
        mesh.RecalculateNormals();
    }

    public void CreateRCBuilding(float _length = 0f, float _width = 0f, float _height = 0f, float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject RCBuilding = new GameObject("RCBuilding");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = RCBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = RCBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;
        
        // Define the vertices of the cube
        Vector3[] RCVertices = new Vector3[]
        {
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z), //0
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //2
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //3 
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _innerWidth), //4
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + _innerWidth), //5
    
            new Vector3(currentPosition.x + _innerLength, _height, currentPosition.z), //0
            new Vector3(currentPosition.x + _length, _height, currentPosition.z), //1 
            new Vector3(currentPosition.x + _length, _height, currentPosition.z + _width), //2
            new Vector3(currentPosition.x, _height, currentPosition.z + _width), //3 
            new Vector3(currentPosition.x, _height, currentPosition.z + _innerWidth), //4
            new Vector3(currentPosition.x + _innerLength, _height, currentPosition.z + _innerWidth), //5
        };

        // Define the triangles to form the cube's faces
        int[] RCTriangles = new int[]
        {
            0, 5, 4,
            1, 2, 3,
            5, 0, 1,
            5, 3, 4,
            1, 3, 5,

            6, 10, 11,
            7, 9, 8,
            7, 6, 11,
            11, 10, 9,
            7, 11, 9,

            0, 4, 10,
            0, 10, 6,

            0, 7 , 1,
            0, 6, 7,

            1, 8, 2,
            1, 7, 8,

            2, 9, 3,
            2, 8, 9,

            3, 10, 4,
            3, 9, 10,
        };
                
        mesh.vertices = RCVertices;
        mesh.triangles = RCTriangles;
        mesh.RecalculateNormals();
        
        Vector3[] trimmedArray = TrimArray(RCVertices, 5);
        
        AddProceduralWindow(trimmedArray, _height, RCBuilding, RCVertices[0], RCVertices[1]);
        AddProceduralDoor(RCVertices[0], RCVertices[1], _height, RCBuilding);
    }

    public void Uroof1(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        // Create a new cube GameObject
        GameObject roof = new GameObject("Roof");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = roof.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = roof.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;
        
        Quaternion rotation = transform.rotation;

        float tempInnerLength = (_length - _innerLength) / 2;
        float tempInnerZ = (_width - _innerWidth) / 2;
        
        Vector3[] uRoof1Vertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _innerWidth), //2 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _innerWidth), //3 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z), //4 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //5
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //6
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //7 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _width), //8
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _width), //9
            
            new Vector3(currentPosition.x + (tempInnerLength / 2), _height, currentPosition.z), //10
            new Vector3(currentPosition.x + (tempInnerLength / 2), _height, currentPosition.z + _innerWidth + tempInnerZ), //11
            new Vector3((currentPosition.x + _length) - (_innerLength/2), _height, currentPosition.z + _innerWidth + tempInnerZ), //13
            new Vector3((currentPosition.x + _length) - (_innerLength/2), _height, currentPosition.z), //12
        };
        
        // Define the triangles to form the cube's faces
        int[] uRoof1Triangles = new int[]
        {
            0, 1, 8,
            0, 8, 7,
            4, 5, 6,
            4, 6, 9,
            2, 3, 9,
            2, 9, 8,

            0, 10, 1,
            
            0, 11, 10,
            0, 7, 11,
            
            1, 10, 11,
            1, 11, 2,
            
            7, 12, 11,
            7, 6, 12,
            
            2, 11, 3,
            11, 12, 3,
            
            4, 3, 12,
            4, 12, 13,
            
            5, 13, 12,
            5, 12, 6,
            
            5, 4, 13,
        };
        
        mesh.vertices = uRoof1Vertices;
        mesh.triangles = uRoof1Triangles;
        mesh.RecalculateNormals();
    }
    
    public void Uroof2(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        // Create a new cube GameObject
        GameObject roof = new GameObject("Roof");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = roof.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = roof.AddComponent<MeshRenderer>();

        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;
        
        Quaternion rotation = transform.rotation;

        float tempInnerLength = (_length - _innerLength) / 2;
        float tempInnerZ = (_width - _innerWidth) / 2;
        
        Vector3[] uRoof1Vertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _innerWidth), //2 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _innerWidth), //3 
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z), //4 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //5
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //6
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //7 
            new Vector3(currentPosition.x + tempInnerLength, _dasar, currentPosition.z + _width), //8
            new Vector3(currentPosition.x + tempInnerLength + _innerLength, _dasar, currentPosition.z + _width), //9
            
            new Vector3(currentPosition.x + (tempInnerLength / 2), _height, currentPosition.z + _innerWidth + tempInnerZ), //10
            new Vector3((currentPosition.x + _length) - (_innerLength/2), _height, currentPosition.z + _innerWidth + tempInnerZ), //11
        };
        
        // Define the triangles to form the cube's faces
        int[] uRoof1Triangles = new int[]
        {
            0, 1, 8,
            0, 8, 7,
            4, 5, 6,
            4, 6, 9,
            2, 3, 9,
            2, 9, 8,

            0, 10, 1,
            
            0, 7, 10,
            
            1, 10, 2,
            
            2, 10, 11,
            2, 11, 3,
            
            7, 11, 10,
            7, 6, 11,
            
            5, 11, 6,
            
            5, 4, 11,
            
            4, 3, 11,
        };
        
        mesh.vertices = uRoof1Vertices;
        mesh.triangles = uRoof1Triangles;
        mesh.RecalculateNormals();
    }
    
    public void LRoof1(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject LBuilding = new GameObject("LRoof");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = LBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = LBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        float tempInnerLength = (_length - _innerLength) / 2;
        Quaternion rotation = transform.rotation;
        
        Vector3[] LVertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x, _dasar, currentPosition.z + (_width - _innerWidth)), //1 
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + (_width - _innerWidth)), //2 -
            new Vector3(currentPosition.x + _innerLength, _dasar, _width), //3 -
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //4 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //5

            new Vector3(currentPosition.x, _height, currentPosition.z + (_width - _innerWidth)/2),  //6
            new Vector3(currentPosition.x + _innerLength + tempInnerLength, _height, currentPosition.z + (_width - _innerWidth)/2),  //7
            new Vector3(currentPosition.x + _innerLength + tempInnerLength, _height, currentPosition.z + _width),  //8
        };

        // Define the triangles to form the cube's faces
        int[] LTriangles = new int[]
        {
            1, 0, 2,
            3, 2, 4,
            4, 0 ,5,
            4, 2, 0,
            
            0, 6, 1,
            
            2, 7, 6,
            2, 6, 1,
            
            0, 6, 7,
            0, 7, 5,
            
            3, 7, 2,
            3, 8, 7,
            
            4, 5, 7,
            4, 7, 8,
            
            3, 4, 8,
        };

        mesh.vertices = LVertices;
        mesh.triangles = LTriangles;
        mesh.RecalculateNormals();
    }
    
    public void LRoof2(float _length = 0f, float _width = 0f, float _height = 0f,  float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject LBuilding = new GameObject("LRoof");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = LBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = LBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        float tempInnerLength = (_length - _innerLength) / 2;
        Quaternion rotation = transform.rotation;
        
        Vector3[] LVertices = new Vector3[]
        {
            new Vector3(currentPosition.x, _dasar, currentPosition.z), //0 
            new Vector3(currentPosition.x, _dasar, currentPosition.z + (_width - _innerWidth)), //1 
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + (_width - _innerWidth)), //2 -
            new Vector3(currentPosition.x + _innerLength, _dasar, _width), //3 -
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //4 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //5

            new Vector3(currentPosition.x + _innerLength + tempInnerLength, _height, currentPosition.z + (_width - _innerWidth)/2),  //6
        };

        // Define the triangles to form the cube's faces
        int[] LTriangles = new int[]
        {
            1, 0, 2,
            3, 2, 4,
            4, 0 ,5,
            4, 2, 0,
            
            0, 1, 6,
            
            2, 6, 1,
            
            3, 6, 2,
            
            4, 6, 3,
            
            5, 6, 4,
            
            0, 6, 5,
        };

        mesh.vertices = LVertices;
        mesh.triangles = LTriangles;
        mesh.RecalculateNormals();
    }
    
    public void RCRoof1(float _length = 0f, float _width = 0f, float _height = 0f, float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject RCBuilding = new GameObject("RCBuilding");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = RCBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = RCBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;
        
        // Define the vertices of the cube
        Vector3[] RCVertices = new Vector3[]
        {
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z), //0
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //2
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //3 
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _innerWidth), //4
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + _innerWidth), //5
    
            new Vector3(currentPosition.x + _innerLength/2, _height, currentPosition.z + _innerWidth/2), //6
            new Vector3(currentPosition.x + _innerLength, _height, currentPosition.z + _innerWidth), //7
            new Vector3(currentPosition.x + _length, _height, currentPosition.z + _width), //2
        };

        // Define the triangles to form the cube's faces
        int[] RCTriangles = new int[]
        {
            0, 5, 4,
            1, 2, 3,
            5, 0, 1,
            5, 3, 4,
            1, 3, 5,

            0, 4, 6,
            
            3, 7, 6,
            3, 6, 4,
            
            1, 6, 7,
            1, 0, 6,
            
            2, 8, 3,
            3, 8, 7,
            
            1, 8, 2,
            1, 7, 8,
        };
                
        mesh.vertices = RCVertices;
        mesh.triangles = RCTriangles;
        mesh.RecalculateNormals();
    }
    
    public void RCRoof2(float _length = 0f, float _width = 0f, float _height = 0f, float _innerLength = 0f, float _innerWidth = 0f, float _dasar = 0f)
    {
        GameObject RCBuilding = new GameObject("RCBuilding");

        // Add MeshFilter and MeshRenderer components
        MeshFilter meshFilter = RCBuilding.AddComponent<MeshFilter>();
        MeshRenderer meshRenderer = RCBuilding.AddComponent<MeshRenderer>();
        
        Mesh mesh = new Mesh();
        meshFilter.mesh = mesh;
        meshRenderer.material = colorMaterial;

        Quaternion rotation = transform.rotation;
        
        // Define the vertices of the cube
        Vector3[] RCVertices = new Vector3[]
        {
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z), //0
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z), //1 
            new Vector3(currentPosition.x + _length, _dasar, currentPosition.z + _width), //2
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _width), //3 
            new Vector3(currentPosition.x, _dasar, currentPosition.z + _innerWidth), //4
            new Vector3(currentPosition.x + _innerLength, _dasar, currentPosition.z + _innerWidth), //5
    
            new Vector3(currentPosition.x + _innerLength, _height, currentPosition.z + _innerWidth), //6
        };

        // Define the triangles to form the cube's faces
        int[] RCTriangles = new int[]
        {
            0, 5, 4,
            1, 2, 3,
            5, 0, 1,
            5, 3, 4,
            1, 3, 5,

            4, 6, 0,
            3, 6, 4,
            2, 6, 3,
            1, 6, 2,
            0, 6, 1,
        };
                
        mesh.vertices = RCVertices;
        mesh.triangles = RCTriangles;
        mesh.RecalculateNormals();
    }
    
    Vector3[] TrimArray(Vector3[] originalArray, int newSize)
    {
        if (newSize >= 0 && newSize <= originalArray.Length)
        {
            Vector3[] trimmedArray = new Vector3[newSize];
            System.Array.Copy(originalArray, trimmedArray, newSize);
            return trimmedArray;
        }
        else
        {
            Debug.LogWarning("Invalid newSize provided for trimming the array.");
            return originalArray; // Return the original array if newSize is invalid
        }
    }
}
