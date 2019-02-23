using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryManager : MonoBehaviour
{
    public FoodObject heldObject;
    public GameObject heldIcon;

    // Update is called once per frame
    void Update()
    {
        RenderHeldObject();
    }


    /*
    ==================================================
    Getting Player's Held Item Information
    ==================================================
    */
    public bool PlayerHasItem()
    {
        if (heldObject != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    /*
    ==================================================
    Altering Player's Held Item Information
    ==================================================
    */
    public void SetHeldObject(FoodObject newObject)
    {
        this.heldObject = newObject;
    }

    public FoodObject GetHeldObject()
    {
        return this.heldObject;
    }

    public void ClearHeldObject()
    {
        this.heldObject = null;
    }


    /*
    ==================================================
    Rendering Player's Held Item
    ==================================================
    */
    private void RenderHeldObject()
    {
        if (this.heldObject != null)
        {
            heldIcon.SetActive(true);
            heldIcon.GetComponent<Image>().sprite = heldObject.foodIcon;
            heldIcon.transform.position = Input.mousePosition;
        }
        else
        {
            heldIcon.SetActive(false);
        }
    }
}