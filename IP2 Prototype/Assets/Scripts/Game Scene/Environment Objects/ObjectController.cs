using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplianceTypes
{
    BURNER,
    SHAKER,
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
    public virtual void ActivateObject()
    {
        FoodObject foodToCheck = foodSlot.GetSlotContents();
        if (foodToCheck != null)
        {
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

            if (success)
            {
                EventSuccess(foodToCheck.applianceTransformations[transformation]);
            }
            else
            {
                EventFail(foodToCheck.failedTransformation);
            }
        }
        else
        {
            Debug.Log("Error: Input Field Is Empty");
        }
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
