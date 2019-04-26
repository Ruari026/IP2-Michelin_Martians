using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BusterPuzzleScript : PuzzleClass
{
    public GameObject[] panels;
    public Color[] colourSet = { Color.red, Color.blue, Color.green, Color.yellow };
    [Header("If Parameter 1")]
    public Color[] sol1 = { Color.red, Color.red, Color.blue };
    [Header("If Parameter 2")]
    public Color[] sol2 = { Color.green, Color.blue, Color.yellow, Color.green };
    [Header("If Parameter 3")]
    public Color[] sol3 = { Color.blue, Color.red, Color.blue, Color.yellow };
    [Header("If Parameter 4")]
    public Color[] sol4 = { Color.blue, Color.green, Color.yellow, Color.red };
    public int score = 0;

    public Text[] colourTexts;
    public Text endText;

    public Color[] currentSol;
    
    
    /*
    ====================================================================================================
    Inherited PuzzleClass Methods
    ====================================================================================================
    */
    public override void GetRelativeSolution(FoodObject transformingFood)
    {
        Debug.Log(transformingFood.foodToughness);
        switch (transformingFood.foodToughness)
        {
            case (FoodToughness.SOFT):
                {
                    currentSol = sol1;
                }
                break;

            case (FoodToughness.WEAK):
                {
                    currentSol = sol2;
                }
                break;

            case (FoodToughness.TOUGH):
                {
                    currentSol = sol3;
                }
                break;

            case (FoodToughness.HARD):
                {
                    currentSol = sol4;
                }
                break;
        }
    }

    public override bool CheckSolution()
    {
        for (int i = 0; i < currentSol.Length; i++)
        {
            if (panels[i].GetComponent<Image>().color == currentSol[i])
            {
                Debug.Log("YAY");
                score++;
            }
        }

        if (score == currentSol.Length)
        {
            Debug.Log("Win");
            endText.text = "Correct";
            score = 0;
            return true;
        }
        else
        {
            Debug.Log("Lose");
            endText.text = "Incorrect";
            score = 0;
            return false;
        }
    }

    public override void ResetPuzzle()
    {
        for (int i = 0; i < 4; i++)
        {
            panels[i].GetComponent<Image>().color = colourSet[0];
            colourTexts[i].text = "R";
        }
        
    }


    /*
    ====================================================================================================
    Puzzle Unique Methods
    ====================================================================================================
    */
    public void UpChange(int buttonNumber)
    {
        if (panels[buttonNumber].GetComponent<Image>().color == colourSet[0])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[1];
            colourTexts[buttonNumber].text = "B";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[1])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[2];
            colourTexts[buttonNumber].text = "G";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[2])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[3];
            colourTexts[buttonNumber].text = "Y";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[3])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[0];
            colourTexts[buttonNumber].text = "R";
        }
    }

    public void DownChange(int buttonNumber)
    {
        if (panels[buttonNumber].GetComponent<Image>().color == colourSet[0])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[3];
            colourTexts[buttonNumber].text = "Y";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[3])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[2];
            colourTexts[buttonNumber].text = "G";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[2])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[1];
            colourTexts[buttonNumber].text = "B";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[1])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[0];
            colourTexts[buttonNumber].text = "R";
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
