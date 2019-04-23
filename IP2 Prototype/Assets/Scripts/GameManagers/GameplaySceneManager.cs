using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameplaySceneManager : MonoBehaviour
{
    [Header("Player Information")]
    public GameObject thePlayerController;

    [Header("Scene Starting Information")]
    public GameObject[] aliens;

    [Header("Level 1 Solutions")]
    public FoodObject[] eggSolutions;
    public GameObject eggTextUI;
    public FoodObject[] soupSolutions;
    public GameObject soupTextUI;
    public FoodObject[] fishFilletsSolutions;
    public GameObject fishFilletsTextUI;

    [Header("Level 2 Solutions")]
    public FoodObject[] steakSolutions;
    public GameObject steakTextUI;
    public FoodObject[] sushiSolutions;
    public GameObject sushiTextUI;
    public FoodObject[] pieSolutions;
    public GameObject pieTextUI;

    [Header("Level 3 Solutions")]
    public FoodObject[] surfAndTurfSolutions;
    public GameObject surfAndTurfTextUI;
    public FoodObject[] caviarSolutions;
    public GameObject caviarTextUI;

    public FoodObject sceneSolution;
    public InventorySlotController[] sceneStorageLocations;
    public FoodObject[] startingFoodObjects;
    
    [Header("Scene Solution Checking")]
    public InventorySlotController solutionPipeInventory;
    public GameObject sceneSolutionInfographic;
    private int alien;
    public GameObject[] solutionCorrectGraphic;
    public GameObject[] solutionWrongGraphic;

    [Header("Scene Timer")]
    private bool runTimer = false;
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
        RandomizeFoodLocations();

        currentTimerValue = maxTimerValue;

        StartCoroutine(SceneStartAnim());
    }

    public void SetSceneSolution()
    {
        //Choosing the alien to serve
        alien = Random.Range(0, aliens.Length);
        aliens[alien].SetActive(true);

        //Choosing the recipe to make
        string s = GameDataManager.GetRecipeName();
        //Level 1
        if (s == "Eggs")
        {
            sceneSolution = eggSolutions[alien];
            eggTextUI.SetActive(true);
        }
        else if (s == "Soup")
        {
            sceneSolution = soupSolutions[alien];
            soupTextUI.SetActive(true);
        }
        else if (s == "Fish Fillets")
        {
            sceneSolution = fishFilletsSolutions[alien];
            fishFilletsTextUI.SetActive(true);
        }
        //Level 2
        else if (s == "Steak")
        {
            sceneSolution = steakSolutions[alien];
            steakTextUI.SetActive(true);
        }
        else if (s == "Sushi")
        {
            sceneSolution = sushiSolutions[alien];
            sushiTextUI.SetActive(true);
        }
        else if (s == "Pie")
        {
            sceneSolution = pieSolutions[alien];
            pieTextUI.SetActive(true);
        }
        //Level 3
        else if (s == "Surf & Turf")
        {
            sceneSolution = surfAndTurfSolutions[alien];
            surfAndTurfTextUI.SetActive(true);
        }
        else if (s == "Caviar")
        {
            sceneSolution = caviarSolutions[alien];
            caviarTextUI.SetActive(true);
        }
    }
    
    public void RandomizeFoodLocations()
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

    private IEnumerator SceneStartAnim()
    {
        CameraController playerCC = thePlayerController.GetComponent<CameraController>();

        yield return new WaitForSeconds(3);

        playerCC.StartCoroutine(playerCC.MoveCamera(playerCC.defaultPosition.position, playerCC.defaultPosition.rotation, playerCC.zoomTime));

        yield return new WaitForSeconds(playerCC.zoomTime);

        playerCC.SetPlayerInputState(true);
        runTimer = true;
    }


    /*
    ======================================================================
    Running The Scene
    ======================================================================
    */
    private void Update()
    {
        //Level Timer Handling
        RunTimer();
        //Returning To The Main Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Main Menu");
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
        solutionCorrectGraphic[alien].SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        solutionPipeInventory.SetSlotInteractionState(false);
        solutionCorrectGraphic[alien].SetActive(false);

        endScreen.SetActive(true);
        float minutes = Mathf.FloorToInt(winTime / 60);
        float seconds = Mathf.FloorToInt(winTime - minutes * 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        endText.text = time;

        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator ShowPlayerFail()
    {
        solutionPipeInventory.SetSlotInteractionState(false);
        solutionWrongGraphic[alien].SetActive(true);
        sceneSolutionInfographic.SetActive(false);

        yield return new WaitForSeconds(2.5f);

        solutionPipeInventory.SetSlotInteractionState(true);
        solutionWrongGraphic[alien].SetActive(false);
        sceneSolutionInfographic.SetActive(true);
    }


    /*
    ======================================================================
    Handling The Scene Timer
    ======================================================================
    */
    private void RunTimer()
    {
        if (runTimer)
        {
            currentTimerValue -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(currentTimerValue / 60);
            float seconds = Mathf.FloorToInt(currentTimerValue - minutes * 60);

            string time = string.Format("{0:00}:{1:00}", minutes, seconds);
            sceneTimerText.text = time;
        }
    }
}
