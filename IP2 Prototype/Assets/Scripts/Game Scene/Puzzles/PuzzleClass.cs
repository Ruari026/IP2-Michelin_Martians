using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleClass : MonoBehaviour
{
    public virtual void GetRelativeSolution(FoodObject transformingFood)
    {

    }

    public virtual bool CheckSolution()
    {
        Debug.Log("Base Class");
        return false;
    }
}
