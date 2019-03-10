using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotController : MonoBehaviour
{
    public FoodObject slotObject;
    public Image slotIcon;
    private PlayerInventoryManager playerInventoryInstance;

    // Start is called before the first frame update
    void Start()
    {
        playerInventoryInstance = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventoryManager>();
        if (playerInventoryInstance == null)
        {
            Debug.Log("Error: Player Requires A PlayerInventoryManager Component");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Rendering Stored Item
        if (slotObject != null)
        {
            slotIcon.enabled = true;
            slotIcon.sprite = slotObject.foodIcon;
        }
        else
        {
            slotIcon.enabled = false;
        }
    }

    /*
    ==================================================
    Player Interation With The Slot
    ==================================================
    */
    public void SlotClicked()
    {
        if (playerInventoryInstance.PlayerHasItem())
        {
            if (this.slotObject != null)
            {
                //Player Is Trying To Drop The Item Into A Taken Slot
                Debug.Log("Error: Slot Taken");
            }
            else
            {
                //Player Is Dropping An Item Into An Empty Slot
                Debug.Log("Inventory Slot Clicked, Player Is Dropping Item");

                slotObject = playerInventoryInstance.GetHeldObject();
                playerInventoryInstance.ClearHeldObject();
            }
        }
        else
        {
            if (this.slotObject != null)
            {
                //Player Is Picking Up An Item
                Debug.Log("Inventory Slot Clicked, Player Is Picking Up Item");
                playerInventoryInstance.SetHeldObject(this.slotObject);
                this.ClearSlotContents();
            }
            else
            {
                //Player Is Trying To Pick Up An Item From An Empty Slot
                Debug.Log("Error: Slot Empty");
            }
        }
    }

    public void SetSlotInteractionState(bool newState)
    {
        this.gameObject.GetComponent<Button>().interactable = newState;
    }


    /*
    ==================================================
    Controlling Slot Contents
    ==================================================
    */
    public void AddSlotContents(FoodObject foodToAdd)
    {
        this.slotObject = foodToAdd;
    }

    public FoodObject GetSlotContents()
    {
        return this.slotObject;
    }

    public void ClearSlotContents()
    {
        this.slotObject = null;
    }
}