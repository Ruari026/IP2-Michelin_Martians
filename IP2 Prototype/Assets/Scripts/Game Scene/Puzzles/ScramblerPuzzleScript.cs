using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScramblerPuzzleScript : PuzzleClass
{
    //Puzzle Elements
    public Slider[] puzzleSliders;

    //Grinder Puzzle Has 4 Solutions
    [Header("If The Ingredient Has Horns")]
    public List<FoodObject> solution1Ingredients;
    public bool[] solution1;

    [Header("Else, If The Ingredient Is Mostly Green")]
    public List<FoodObject> solution2Ingredients;
    public bool[] solution2;

    [Header("Else, If The Ingredient Has Red Spots")]
    public List<FoodObject> solution3Ingredients;
    public bool[] solution3;
    
    [Header("Else, If None Of The Above Are True")]
    public bool[] solution4;

    public bool[] currentSolution;

    public override void GetRelativeSolution(FoodObject transformingFood)
    {
        if (solution1Ingredients.Contains(transformingFood))
        {
            Debug.Log("Using Solution 1");
            currentSolution = solution1;
        }
        else if(solution2Ingredients.Contains(transformingFood))
        {
            Debug.Log("Using Solution 2");
            currentSolution = solution2;
        }
        else if(solution3Ingredients.Contains(transformingFood))
        {
            Debug.Log("Using Solution 3");
            currentSolution = solution3;
        }
        else
        {
            Debug.Log("Using Solution 4");
            currentSolution = solution4;
        }
    }

    public override bool CheckSolution()
    {
        //Getting What The Player Has Put On The UI
        string p = "";
        string s = "";
        for (int i = 0; i < 8; i++)
        {
            if (puzzleSliders[i].value == 1)
            {
                p += "0";
            }
            else
            {
                p += ".";
            }

            if (currentSolution[i])
            {
                s += "0";
            }
            else
            {
                s += ".";
            }
        }
        Debug.Log("Player Solution: " + p);
        Debug.Log("Puzzle Solution: " + s);

        if (p == s)
        {
            Debug.Log("Correct Solution");
            return true;
        }
        else
        {
            Debug.Log("Wrong Solution");
            return false;
        }
    }

    public void ResetPuzzle()
    {
        for (int i = 0; i < puzzleSliders.Length; i++)
        {
            puzzleSliders[i].value = 0;
        }
    }
}
