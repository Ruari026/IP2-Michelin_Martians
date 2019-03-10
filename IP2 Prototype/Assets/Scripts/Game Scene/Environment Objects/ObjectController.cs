using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplianceTypes
{
    SCRAMBLER,
    GRINDER,
    STORAGE,
    FINALPIPE
};

public class ObjectController : MonoBehaviour
{
    public ApplianceTypes applianceType;
    
    public InventorySlotController foodSlot;

    private Animator theAnimController;
    public Transform selectionCameraPosition;

    public GameObject applianceDefaultUi;
    public GameObject appliancePuzzleUi;
    public PuzzleClass appliancePuzzle;

    /*
    ==================================================
    Information Required For Player Camera
    ==================================================
    */
    public Transform SelectedObject()
    {
        theAnimController = this.GetComponent<Animator>();
        if (theAnimController != null)
        {
            this.GetComponent<Animator>().SetBool("Opened", true);
        }

        return selectionCameraPosition;
    }
    

    /*
    ==================================================
    Affecting Animation State On The Object
    ==================================================
    */
    public void ResetObject()
    {
        if (theAnimController != null)
        {
            this.GetComponent<Animator>().SetBool("Opened", false);
        }
    }


    /*
    ==================================================
    Puzzle Event Handling
    ==================================================
    */
    public void ActivatePuzzle()
    {
        FoodObject foodToCheck = foodSlot.GetSlotContents();
        if (foodToCheck != null)
        {
            applianceDefaultUi.SetActive(false);
            appliancePuzzleUi.SetActive(true);
            foodSlot.SetSlotInteractionState(false);

            appliancePuzzle.GetRelativeSolution(foodSlot.slotObject);
        }
    }

    public void CheckPuzzle()
    {
        FoodObject foodToCheck = foodSlot.GetSlotContents();

        bool success = false;
        int transformation = 0;

        for (int i = 0; i < foodToCheck.applianceSolutions.Length; i++)
        {
            if (foodToCheck.applianceSolutions[i] == applianceType)
            {
                success = true;
                transformation = i;
            }
        }

        if (success && appliancePuzzle.CheckSolution())
        {
            EventSuccess(foodToCheck.applianceTransformations[transformation]);
        }
        else
        {
            EventFail(foodToCheck.failedTransformation);
        }

        applianceDefaultUi.SetActive(true);
        appliancePuzzleUi.SetActive(false);
        foodSlot.SetSlotInteractionState(true);
    }

    public void EventSuccess(FoodObject nextFoodObject)
    {
        foodSlot.AddSlotContents(nextFoodObject);
    }

    public void EventFail(FoodObject garbageFoodObject)
    {
        foodSlot.AddSlotContents(garbageFoodObject);
    }
}
