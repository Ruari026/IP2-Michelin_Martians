using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSceneManager : MonoBehaviour
{
    [Header("Scene Starting Information")]
    public GameObject[] aliens;
    public FoodObject[] solutions;
    public FoodObject sceneSolution;
    public InventorySlotController[] sceneStorageLocations;
    public FoodObject[] startingFoodObjects;
    
    [Header("Scene Solution Checking")]
    public InventorySlotController solutionPipeInventory;
    public GameObject sceneSolutionInfographic;
    public GameObject solutionCorrectGraphic;
    public GameObject solutionWrongGraphic;


    [Header("Scene Timer")]
    public Text sceneTimerText;
    public float maxTimerValue;
    public float currentTimerValue;
    public GameObject endScreen;
    public Text endText;
    /*
    ======================================================================
    Handling Setting Up The Game Scene
    ======================================================================
    */
    void Start()
    {
        SetSceneSolution();
        RandomizeFoodLoctions();

        currentTimerValue = maxTimerValue;
    }

    private void Update()
    {
        RunTimer();
    }

    public void SetSceneSolution()
    {
        int i = Random.Range(0, aliens.Length);
        aliens[i].SetActive(true);
        sceneSolution = solutions[i];
    }
    
    public void RandomizeFoodLoctions()
    {
        for (int i = 0; i < startingFoodObjects.Length; i++)
        {
            bool placed = false;

            while (!placed)
            {
                int spot = Random.Range(0, sceneStorageLocations.Length);

                if (sceneStorageLocations[spot].GetSlotContents() == null)
                {
                    sceneStorageLocations[spot].AddSlotContents(startingFoodObjects[i]);
                    placed = true;
                }
            }
        }
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
                StartCoroutine(ShowPlayerSuccess(currentTimerValue));
            }
            else
            {
                Debug.Log("Player Has Submitted A Wrong Item");
                StartCoroutine(ShowPlayerFail());
            }
        }
    }

    IEnumerator ShowPlayerSuccess(float winTime)
    {
        solutionPipeInventory.SetSlotInteractionState(false);
        solutionCorrectGraphic.SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        solutionPipeInventory.SetSlotInteractionState(false);
        solutionCorrectGraphic.SetActive(false);

        endScreen.SetActive(true);
        float minutes = Mathf.FloorToInt(winTime / 60);
        float seconds = Mathf.FloorToInt(winTime - minutes * 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        endText.text = time;
    }

    IEnumerator ShowPlayerFail()
    {
        solutionPipeInventory.SetSlotInteractionState(false);
        solutionWrongGraphic.SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        solutionPipeInventory.SetSlotInteractionState(true);
        solutionWrongGraphic.SetActive(false);
        sceneSolutionInfographic.SetActive(true);
    }


    /*
    ======================================================================
    Handling The Scene Timer
    ======================================================================
    */
    private void RunTimer()
    {
        currentTimerValue -= Time.deltaTime;

        float minutes = Mathf.FloorToInt(currentTimerValue / 60);
        float seconds = Mathf.FloorToInt(currentTimerValue - minutes * 60);

        string time = string.Format("{0:00}:{1:00}", minutes, seconds);

        sceneTimerText.text = time;
    }
}
