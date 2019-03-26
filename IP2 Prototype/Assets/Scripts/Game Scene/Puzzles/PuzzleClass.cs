using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleClass : MonoBehaviour
{
    public ObjectController parentController;

    public virtual void GetRelativeSolution(FoodObject transformingFood)
    {

    }

    public virtual bool CheckSolution()
    {
        Debug.Log("Base Class");
        return false;
    }

    public virtual void ResetPuzzle()
    {

    }
}
