using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public TMP_InputField axiom;
    public Transform parents;
    public GameObject inputBuild1;
    public GameObject inputBuild2;
    public List<char> initialShape;
    public List<int> floorNum;
    public List<char> roofType;
    public List<float> _paramLength;
    public List<float> _paramWidth;
    public List<float> _paramHeight;
    public List<float> _paramInnerLength;
    public List<float> _paramInnerWidth;

    public GameObject errorPanel;
    public GameObject inputPanel;
    
    List<char> validShape = new List<char> { 'A', 'B', 'C', 'D' };
    List<char> validRoof = new List<char> { 'X', 'Y' };
    // Start is called before the first frame update
    public void Next()
    {
        ParseAndGeneratePatterns(axiom.text);
        if (initialShape.Count == floorNum.Count && floorNum.Count == roofType.Count && CheckShape() && CheckRoof())
        {
            inputPanel.SetActive(true);
            SetInputField();
        }
        else
        {
            errorPanel.SetActive(true);
        }
    }
    
    public bool CheckShape()
    {
        foreach (char character in initialShape)
        {
            if (!validShape.Contains(character))
            {
                return false; // Invalid character found
            }
        }
        return true;
    }
    
    public bool CheckRoof()
    {
        foreach (char character in roofType)
        {
            if (!validRoof.Contains(character))
            {
                return false; // Invalid character found
            }
        }
        return true;
    }
    
    public void ParseAndGeneratePatterns(string userInput)
    {
        int currentIndex = 0;

        while (currentIndex < userInput.Length)
        {
            string currentPattern = ExtractPattern(userInput, ref currentIndex);
            if (currentPattern != null)
            {
                GeneratePattern(currentPattern);
            }
        }
    }
    
    public string ExtractPattern(string userInput, ref int currentIndex)
    {
        StringBuilder patternBuilder = new StringBuilder();

        try
        {
            // Extract the first shape 'X' or 'Y'
            patternBuilder.Append(userInput[currentIndex]);
            currentIndex++;

            // Extract the number part 'Y'
            while (currentIndex < userInput.Length && char.IsDigit(userInput[currentIndex]))
            {
                patternBuilder.Append(userInput[currentIndex]);
                currentIndex++;
            }

            // Extract the second shape 'Z'
            patternBuilder.Append(userInput[currentIndex]);
            currentIndex++;

            return patternBuilder.ToString();
        }
        catch (Exception e)
        {
            return null;
        }
    }

    public void GeneratePattern(string pattern)
    {
        char firstShape = pattern[0];
        initialShape.Add(firstShape);

        // Extract the number 'Y' from the pattern
        int yIndex = 1;
        while (yIndex < pattern.Length && char.IsDigit(pattern[yIndex]))
        {
            yIndex++;
        }

        if (!int.TryParse(pattern.Substring(1, yIndex - 1), out int duplicationCount))
        {
            return;
        }
        
        floorNum.Add(duplicationCount);

        char topShape = pattern[yIndex];
        roofType.Add(topShape);

        StringBuilder result = new StringBuilder();

        for (int i = 0; i < duplicationCount; i++)
        {
            result.Append(firstShape);
        }

        result.Append(topShape);
    }

    public void SetInputField()
    {
        for (int i = 0; i < initialShape.Count; i++)
        {
            if (initialShape[i] == 'A')
            {
                Instantiate(inputBuild1, parents.transform);
            }
            else
            {
                Instantiate(inputBuild2, parents.transform);
            }
        }
    }

    public void GetParameter()
    {
        GameObject[] length = GameObject.FindGameObjectsWithTag("Length");
        GameObject[] width = GameObject.FindGameObjectsWithTag("Width");
        GameObject[] height = GameObject.FindGameObjectsWithTag("Height");
        GameObject[] innerLength = GameObject.FindGameObjectsWithTag("innerLength");
        GameObject[] innerWidth = GameObject.FindGameObjectsWithTag("innerWidth");

        for (int i = 0; i < initialShape.Count; i++)
        {
            _paramLength.Add(float.Parse(length[i].GetComponent<TMP_InputField>().text));
            _paramWidth.Add(float.Parse(width[i].GetComponent<TMP_InputField>().text));
            _paramHeight.Add(float.Parse(height[i].GetComponent<TMP_InputField>().text));
            _paramInnerLength.Add(float.Parse(innerLength[i].GetComponent<TMP_InputField>().text));
            _paramInnerWidth.Add(float.Parse(innerWidth[i].GetComponent<TMP_InputField>().text));
        }
    }
}
