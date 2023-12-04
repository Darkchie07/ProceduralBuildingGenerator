using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class UIInput : MonoBehaviour
{
    public TMP_InputField axiom;
    public GameObject inputBuild1;
    public GameObject inputBuild2;
    public List<char> initialShape;
    public List<int> floorNum;
    public List<char> roofType;
    
    List<char> validShape = new List<char> { 'A', 'B', 'C', 'D' };
    List<char> validRoof = new List<char> { 'X', 'Y' };
    // Start is called before the first frame update
    void Start()
    {
        ParseAndGeneratePatterns(axiom.text);
        if (initialShape.Count == floorNum.Count && floorNum.Count == roofType.Count && CheckShape() && CheckRoof())
        {
            //
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            GeneratePattern(currentPattern);
        }
    }
    
    public string ExtractPattern(string userInput, ref int currentIndex)
    {
        StringBuilder patternBuilder = new StringBuilder();

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
         
    }
}
