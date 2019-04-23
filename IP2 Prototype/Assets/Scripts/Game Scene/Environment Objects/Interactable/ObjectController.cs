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
    COMBINER,

    //Other Interactable Items
    STORAGE,
    FINALPIPE
};

public class ObjectController : MonoBehaviour
{
    public ApplianceTypes applianceType;
    
    public InventorySlotController[] foodSlots = new InventorySlotController[1];

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
        bool canActivate = true;
        for (int i = 0; i < foodSlots.Length; i++)
        {
            if (foodSlots[i].GetSlotContents() == null)
            {
                canActivate = false;
            }
        }


        if (canActivate)
        {
            applianceDefaultUi.SetActive(false);
            appliancePuzzleUi.SetActive(true);
            foodSlots[0].SetSlotInteractionState(false);

            appliancePuzzle.GetRelativeSolution(foodSlots[0].slotObject);
        }
    }

    public void CheckPuzzle()
    {
        if (applianceType == ApplianceTypes.COMBINER)
        {
            bool success = false;

            FoodObject food1 = foodSlots[0].GetSlotContents();
            FoodObject food2 = foodSlots[1].GetSlotContents();
            FoodObject newFood = new FoodObject();

            for (int i = 0; i < food1.foodCombinations.Length; i++)
            {
                for (int j = 0; j < food2.foodCombinations.Length; j++)
                {
                    if (food1 == food2.foodCombinations[j] && food2 == food1.foodCombinations[i])
                    {
                        success = true;
                        newFood = food1.combinationTransformations[i];
                    }
                }
            }

            if (success && appliancePuzzle.CheckSolution())
            {
                TransformFoodItemSuccess(newFood);
            }
            else
            {
                TransformFoodItemFail(food1.failedTransformation);
            }

            applianceDefaultUi.SetActive(true);
            appliancePuzzleUi.SetActive(false);
            foodSlots[0].SetSlotInteractionState(true);
        }
        else
        {
            FoodObject foodToCheck = foodSlots[0].GetSlotContents();

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
                TransformFoodItemSuccess(foodToCheck.applianceTransformations[transformation]);
            }
            else
            {
                TransformFoodItemFail(foodToCheck.failedTransformation);
            }

            applianceDefaultUi.SetActive(true);
            appliancePuzzleUi.SetActive(false);
            foodSlots[0].SetSlotInteractionState(true);
        }
    }

    public void CheckPuzzle(bool manualSuccess)
    {
        FoodObject foodToCheck = foodSlots[0].GetSlotContents();
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
            TransformFoodItemSuccess(foodToCheck.applianceTransformations[transformation]);
        }
        else
        {
            TransformFoodItemFail(foodToCheck.failedTransformation);
        }

        applianceDefaultUi.SetActive(true);
        appliancePuzzleUi.SetActive(false);
        foodSlots[0].SetSlotInteractionState(true);
    }

    public void TransformFoodItemSuccess(FoodObject nextFoodObject)
    {
        for (int i = 0; i < foodSlots.Length; i++)
        {
            foodSlots[i].slotObject = null;
        }

        foodSlots[0].AddSlotContents(nextFoodObject);
    }

    public void TransformFoodItemFail(FoodObject garbageFoodObject)
    {
        for (int i = 0; i < foodSlots.Length; i++)
        {
            foodSlots[i].slotObject = null;
        }

        foodSlots[0].AddSlotContents(garbageFoodObject);
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
