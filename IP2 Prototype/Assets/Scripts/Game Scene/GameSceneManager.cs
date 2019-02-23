using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public FoodObject sceneSolution;

    [Header("Scene Solution Checking")]
    public InventorySlotController solutionPipeInventory;
    public GameObject sceneSolutionInfographic;
    public GameObject solutionCorrectGraphic;
    public GameObject solutionWrongGraphic;

    public FoodObject GetSceneSolution()
    {
        return this.sceneSolution;
    }


    /*
    ======================================================================
    Handling when the player checks if they have finished the level
    ======================================================================
    */
    public void CheckPlayerSolution()
    {
        FoodObject playerSolution = solutionPipeInventory.GetSlotContents();

        if (playerSolution == null)
        {
            Debug.Log("Error: There is no fooditem to check");
        }
        else
        {
            if (playerSolution == sceneSolution)
            {
                Debug.Log("Player Has Completed The Level");
                StartCoroutine(ShowPlayerSuccess());
            }
            else
            {
                Debug.Log("Player Has Submitted A Wrong Item");
                StartCoroutine(ShowPlayerFail());
            }
        }
    }

    IEnumerator ShowPlayerSuccess()
    {
        solutionCorrectGraphic.SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(3);

        solutionCorrectGraphic.SetActive(false);
        sceneSolutionInfographic.SetActive(true);
    }

    IEnumerator ShowPlayerFail()
    {
        solutionWrongGraphic.SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(3);

        solutionWrongGraphic.SetActive(false);
        sceneSolutionInfographic.SetActive(true);
    }
}
