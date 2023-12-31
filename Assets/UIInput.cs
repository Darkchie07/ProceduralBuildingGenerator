using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UIInput : MonoBehaviour
{
    public TMP_InputField axiom;
    public Transform parents;
    public GameObject inputBuild1;
    public GameObject inputBuild2;
    public GameObject inputPosition;
    public List<char> initialShape;
    public List<int> floorNum;
    public List<char> roofType;
    public List<float> _paramLength;
    public List<float> _paramWidth;
    public List<float> _paramHeight;
    public List<float> _paramInnerLength;
    public List<float> _paramInnerWidth;
    public List<float> _paramXPosition;
    public List<float> _paramYPosition;
    public List<float> _paramZPosition;
    public List<float> _paramXOffset;
    public List<float> _paramYOffset;
    private string[] result;

    public GameObject errorPanel;
    public GameObject inputPanel;

    // public FlexibleColorPicker fcpShape;
    // public FlexibleColorPicker fcpRoof;
    // public FlexibleColorPicker fcpDoor;
    // public FlexibleColorPicker fcpWindow;
    // public FlexibleColorPicker fcpStair;
    
    public FlexibleColorPicker[] colorObjects;
    
    public Material colorShape;
    public Material colorRoof;
    public Material colorDoor;
    public Material colorWindow;
    public Material colorStair;
    
    public List<float> RColor;
    public List<float> GColor;
    public List<float> BColor;
    public List<float> AColor;
    
    public GameObject oldObject;
    public CharacterMovement _CharacterMovement;

    public TMP_InputField prefabName;

    // Start is called before the first frame update
    private void Start()
    {
        // colorShape = new Material(Resources.Load<Material>("Material/ShapeMaterial"));
        // colorRoof = Resources.Load<Material>("Material/RoofMaterial");
        // colorDoor = Resources.Load<Material>("Material/DoorMaterial");
        // colorWindow = Resources.Load<Material>("Material/WindowMaterial");
        // colorStair = Resources.Load<Material>("Material/StairMaterial");
    }

    public void Next()
    {
        string inputString = axiom.text;
        char[] delimiters = { 'C', 'L', 'P', 'U', 'R' };
        result = SplitString(inputString, delimiters);
        SetInputField();
        inputPanel.SetActive(true);
    }
    //
    // public bool CheckShape()
    // {
    //     foreach (char character in initialShape)
    //     {
    //         if (!validShape.Contains(character))
    //         {
    //             return false; // Invalid character found
    //         }
    //     }
    //     return true;
    // }
    //
    // public bool CheckRoof()
    // {
    //     foreach (char character in roofType)
    //     {
    //         if (!validRoof.Contains(character))
    //         {
    //             return false; // Invalid character found
    //         }
    //     }
    //     return true;
    // }
    //
    // public void ParseAndGeneratePatterns(string userInput)
    // {
    //     int currentIndex = 0;
    //
    //     while (currentIndex < userInput.Length)
    //     {
    //         string currentPattern = ExtractPattern(userInput, ref currentIndex);
    //         if (currentPattern != null)
    //         {
    //             GeneratePattern(currentPattern);
    //         }
    //     }
    // }
    //
    // public string ExtractPattern(string userInput, ref int currentIndex)
    // {
    //     StringBuilder patternBuilder = new StringBuilder();
    //
    //     try
    //     {
    //         // Extract the first shape 'X' or 'Y'
    //         patternBuilder.Append(userInput[currentIndex]);
    //         currentIndex++;
    //
    //         // Extract the number part 'Y'
    //         while (currentIndex < userInput.Length && char.IsDigit(userInput[currentIndex]))
    //         {
    //             patternBuilder.Append(userInput[currentIndex]);
    //             currentIndex++;
    //         }
    //
    //         // Extract the second shape 'Z'
    //         patternBuilder.Append(userInput[currentIndex]);
    //         currentIndex++;
    //
    //         return patternBuilder.ToString();
    //     }
    //     catch (Exception e)
    //     {
    //         return null;
    //     }
    // }
    //
    // public void GeneratePattern(string pattern)
    // {
    //     char firstShape = pattern[0];
    //     initialShape.Add(firstShape);
    //
    //     // Extract the number 'Y' from the pattern
    //     int yIndex = 1;
    //     while (yIndex < pattern.Length && char.IsDigit(pattern[yIndex]))
    //     {
    //         yIndex++;
    //     }
    //
    //     if (!int.TryParse(pattern.Substring(1, yIndex - 1), out int duplicationCount))
    //     {
    //         return;
    //     }
    //     
    //     floorNum.Add(duplicationCount);
    //
    //     char topShape = pattern[yIndex];
    //     roofType.Add(topShape);
    //
    //     StringBuilder result = new StringBuilder();
    //
    //     for (int i = 0; i < duplicationCount; i++)
    //     {
    //         result.Append(firstShape);
    //     }
    //
    //     result.Append(topShape);
    // }

    string[] SplitString(string input, char[] delimiters)
    {
        List<string> result = new List<string>();
        string currentSegment = "";

        foreach (char c in input)
        {
            if (System.Array.IndexOf(delimiters, c) != -1)
            {
                // Keep the delimiter with the next character
                if (!string.IsNullOrEmpty(currentSegment))
                {
                    result.Add(currentSegment);
                }

                currentSegment = c.ToString();
            }
            else
            {
                currentSegment += c;
            }
        }

        if (!string.IsNullOrEmpty(currentSegment))
        {
            result.Add(currentSegment);
        }

        return result.ToArray();
    }
    public void SetInputField()
    {
        foreach (Transform child in parents.transform)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < result.Length; i++)
        {
            if (result[i][0] == 'C')
            {
                Instantiate(inputBuild1, parents.transform);
            }
            else if(result[i][0] == 'P')
            {
                Instantiate(inputPosition, parents.transform);
            }
            else
            {
                Instantiate(inputBuild2, parents.transform);
            }
        }
    }

    public void GetParameter()
    {
        _paramLength.Clear();
        _paramWidth.Clear();
        _paramHeight.Clear();
        _paramInnerLength.Clear();
        _paramInnerWidth.Clear();
        _paramXOffset.Clear();
        _paramYOffset.Clear();
        GameObject[] length = GameObject.FindGameObjectsWithTag("Length");
        GameObject[] width = GameObject.FindGameObjectsWithTag("Width");
        GameObject[] height = GameObject.FindGameObjectsWithTag("Height");
        GameObject[] innerLength = GameObject.FindGameObjectsWithTag("innerLength");
        GameObject[] innerWidth = GameObject.FindGameObjectsWithTag("innerWidth");
        GameObject[] Xposition = GameObject.FindGameObjectsWithTag("xposition");
        GameObject[] Yposition = GameObject.FindGameObjectsWithTag("yposition");
        GameObject[] Zposition = GameObject.FindGameObjectsWithTag("zposition");
        GameObject[] Xoffset = GameObject.FindGameObjectsWithTag("XOffset");
        GameObject[] Yoffset = GameObject.FindGameObjectsWithTag("YOffset");

        for (int i = 0; i < length.Length; i++)
        {
            _paramLength.Add(ParseFloatOrDefault(length[i].GetComponent<TMP_InputField>().text));
            _paramWidth.Add(ParseFloatOrDefault(width[i].GetComponent<TMP_InputField>().text));
            _paramHeight.Add(ParseFloatOrDefault(height[i].GetComponent<TMP_InputField>().text));
            _paramInnerLength.Add(ParseFloatOrDefault(innerLength[i].GetComponent<TMP_InputField>().text));
            _paramInnerWidth.Add(ParseFloatOrDefault(innerWidth[i].GetComponent<TMP_InputField>().text));
            _paramXOffset.Add(ParseFloatOrDefault(Xoffset[i].GetComponent<TMP_InputField>().text));
            _paramYOffset.Add(ParseFloatOrDefault(Yoffset[i].GetComponent<TMP_InputField>().text));
        }

        for (int i = 0; i < Xposition.Length; i++)
        {
            _paramXPosition.Add(ParseFloatOrDefault(Xposition[i].GetComponent<TMP_InputField>().text));
            _paramYPosition.Add(ParseFloatOrDefault(Yposition[i].GetComponent<TMP_InputField>().text));
            _paramZPosition.Add(ParseFloatOrDefault(Zposition[i].GetComponent<TMP_InputField>().text));
        }
        
        if (oldObject != null)
        {
            // Instantiate the new object
            GameObject emptyGameObject = new GameObject("House");
            
            // Destroy the old object
            Destroy(oldObject);

            // Assign the new object reference
            oldObject = emptyGameObject;
        }
        else
        {
            GameObject emptyGameObject = new GameObject("House");
            oldObject = emptyGameObject;
        }
        oldObject.transform.position = new Vector3(0f, 0f, 0f);
        oldObject.transform.rotation = Quaternion.identity;
        LSystem lSystem = oldObject.AddComponent<LSystem>();
        lSystem.SetParameter(initialShape, floorNum, roofType, _paramLength, _paramWidth, _paramHeight, _paramInnerLength, _paramInnerWidth, 
            _paramXPosition, _paramYPosition, _paramZPosition, _paramXOffset,  _paramYOffset, result,
            RColor, GColor, BColor, AColor);
        _CharacterMovement.enabled = true;

        // if (CheckValidParameter())
        // {
        //     // Instantiate an empty GameObject
        //     GameObject emptyGameObject = new GameObject("EmptyGameObject");
        //
        //     // You can also set the position, rotation, and parent if needed
        //     emptyGameObject.transform.position = new Vector3(0f, 0f, 0f);
        //     emptyGameObject.transform.rotation = Quaternion.identity;
        //     LSystem lSystem = emptyGameObject.AddComponent<LSystem>();
        //     lSystem.SetParameter(initialShape, floorNum, roofType, _paramLength, _paramWidth, _paramHeight, _paramInnerLength, _paramInnerWidth);
        // }
    }

    float ParseFloatOrDefault(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return 0f;
        }

        if (float.TryParse(input, out float result))
        {
            return result;
        }

        return 0f;
    }
    
    public bool CheckValidParameter()
    {
        for (int i = 0; i < _paramLength.Count; i++)
        {
            if (_paramLength[i] < _paramInnerLength[i] && _paramWidth[i] < _paramInnerWidth[i])
            {
                return false;
            }
        }
        return true;
    }

    public void SetMaterial()
    {
        foreach (FlexibleColorPicker colorObject in colorObjects)
        {
            RColor.Add(colorObject.color.r);
            GColor.Add(colorObject.color.g);
            BColor.Add(colorObject.color.b);
            AColor.Add(colorObject.color.a);
        }
        
        // RColor.Add(fcpShape.color.r);
        // GColor.Add(fcpShape.color.g);
        // BColor.Add(fcpShape.color.b);
        // RColor.Add(fcpRoof.color.r);
        // GColor.Add(fcpRoof.color.g);
        // BColor.Add(fcpRoof.color.b);
        // RColor.Add(fcpDoor.color.r);
        // GColor.Add(fcpDoor.color.g);
        // BColor.Add(fcpDoor.color.b);
        // RColor.Add(fcpWindow.color.r);
        // GColor.Add(fcpWindow.color.g);
        // BColor.Add(fcpWindow.color.b);
        // RColor.Add(fcpStair.color.r);
        // GColor.Add(fcpStair.color.g);
        // BColor.Add(fcpStair.color.b);
        
        // colorShape.color = fcpShape.color;
        // colorRoof.color = fcpRoof.color;
        // colorDoor.color = fcpDoor.color;
        // colorWindow.color = fcpWindow.color;
        // colorStair.color = fcpStair.color;
    }

    public void savePrefab()
    {
        string prefabPath = "Assets/Prefabs/" + prefabName.text + ".prefab";
        PrefabUtility.SaveAsPrefabAsset(oldObject, prefabPath);
    }
}
