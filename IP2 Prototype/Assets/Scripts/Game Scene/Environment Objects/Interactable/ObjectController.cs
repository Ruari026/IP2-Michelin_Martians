using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplianceTypes
{
    //Appliances
    SCRAMBLER,
    GRINDER,
    BLAZER,
    BUSTER,

    //Other Interactable Items
    STORAGE,
    FINALPIPE
};

public class ObjectController : MonoBehaviour
{
    public ApplianceTypes applianceType;
    
    public InventorySlotController foodSlot;

    public Animator theAnimController;
    public Transform selectionCameraPosition;

    public GameObject applianceDefaultUi;
    public GameObject appliancePuzzleUi;
    public PuzzleClass appliancePuzzle;

    public GameObject objectHighlight;
    public Material highlightMaterial;
    public float objectHighlightWidth = 0.005f;


    private void Start()
    {
        SetHighlightVisibility(false);
    }

    private void Update()
    {
        SetHighlightVisibility(false);
    }


    /*
    ==================================================
    Information Required For Player Camera
    ==================================================
    */
    public Transform SelectedObject()
    {
        if (theAnimController != null)
        {
            theAnimController.SetBool("Opened", true);
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
            theAnimController.SetBool("Opened", false);
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

    public void CheckPuzzle(bool manualSuccess)
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

        if (manualSuccess && success)
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


    /*
    ==================================================
    Handling Highlight Visibility
    ==================================================
    */
    public void SetHighlightVisibility(bool isVisible)
    {
        if (objectHighlight != null)
        {
            if (isVisible)
            {
                objectHighlight.SetActive(true);
            }
            else
            {
                objectHighlight.SetActive(false);
            }
        }
    }
}
