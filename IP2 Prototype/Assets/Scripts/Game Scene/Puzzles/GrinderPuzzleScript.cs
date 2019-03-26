using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderPuzzleScript : PuzzleClass
{
    public override void GetRelativeSolution(FoodObject transformingFood)
    {

    }

    public override bool CheckSolution()
    {
        Debug.Log("Base Class");
        return false;
    }

    public override void ResetPuzzle()
    {

    }
}
