using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplianceTypes
{
    BLAZER,
    FREEZER,
    FIRER,
    CHOPPER,
    STORAGE,
    FINALPIPE
};

public class ObjectController : MonoBehaviour
{
    public ApplianceTypes applianceType;

    public InventorySlotController[] objectSlots;
    public InventorySlotController outputSlot;

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
        FoodObject outputSlotObject = outputSlot.GetSlotContents();
        if (outputSlotObject == null)
        {
            FoodObject foodToCheck = objectSlots[0].GetSlotContents();
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
        else
        {
            Debug.Log("Error: Output Field Is Full");
        }
    }

    public void EventSuccess(FoodObject nextFoodObject)
    {
        objectSlots[0].ClearSlotContents();
        outputSlot.AddSlotContents(nextFoodObject);
    }

    public void EventFail(FoodObject garbageFoodObject)
    {
        objectSlots[0].ClearSlotContents();
        outputSlot.AddSlotContents(garbageFoodObject);
    }
}
