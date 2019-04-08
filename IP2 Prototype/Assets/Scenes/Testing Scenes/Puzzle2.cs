using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle2 : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{ 
    //used to check if a button is held down
    public bool buttonPressed;
    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
    }

    public GameObject activePuzzle;
    public GameObject inactivePuzzle;

    //overall timer for the device, the maximum that time goes up to anda boolean that checks if puzzle started
    float overallTimer = 0f;
    bool activated;
    int maxTime;
    public Text overallTimerText;
    public Slider overallTimerSlider;

    //used to identify which LED is used
    int i = 0;
    [Header("List of LED lights")]
    public List<GameObject> LED;
    [Header("1 for low, 2 for med, 3 for high")]
    public int temperature;

    //button timer, check to see timer start and check to see if button held
    private float timer = 0.0f;
    bool hold;
    bool startTimer;

    //solutions
    [Header("If low temp required.")]
    [Header("True - Held, False - Press")]
    public bool[] solution1;
    [Header("If medium temp required.")]
    public bool[] solution2;
    [Header("If high temp required.")]
    public bool[] solution3;

    public bool[] currentSolution;
    int score = 0;

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)  //button is pressed down, start the timer
        {
            Debug.Log("Button Pressed, timer Start");
            Debug.Log(timer);
            startTimer = true;
            timer += Time.deltaTime;
        }

        if (!buttonPressed &&  startTimer) //if the timer has been started and the button is no longer held down - check how long held for
        {
            if (timer >= 1f)
            {
                hold = true;
                PressComplete();
            }
            else if (timer < 1f)
            {
                hold = false;
                PressComplete();
            }
        }

        if(activated)
        {
            overallTimer += Time.deltaTime;
            overallTimerText.text = string.Format ("{00}", (int)overallTimer);
            overallTimerSlider.value = overallTimer;
             if((int)overallTimer == maxTime)
             {
               Debug.Log("Fail, create Charcoal");
               activated = false;
             }
        }



    }

    public void ActivatePuzzle() //start the puzzle, sets the solution and starts the overall timer
    {
        activePuzzle.SetActive(true);
        inactivePuzzle.SetActive(false);

        if (temperature == 1)
        {
            Debug.Log("Using Solution 1");
            currentSolution = solution1;
            activated = true;
            maxTime = 10;
            overallTimerSlider.maxValue = maxTime;
        }
        else if (temperature == 2)
        {
            Debug.Log("Using Solution 2");
            currentSolution = solution2;
            activated = true;
            maxTime = 15;
            overallTimerSlider.maxValue = maxTime;
        }
        else if (temperature == 3)
        {
            Debug.Log("Using Solution 3");
            currentSolution = solution3;
            activated = true;
            maxTime = 20;
            overallTimerSlider.maxValue = maxTime;
        }
    }

    public void PressComplete()//checks if the solution on the given result are the same and sets appropriate colors
    {
        if (hold == currentSolution[i])
        {
            LED[i].GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            i++;
            score++;
            timer = 0f;
            startTimer = false;
            Debug.Log("Correct");

            if (score==4)
            {
                Debug.Log("Pass, create food");
                activated = false;
            }
        }
        else
        {
            LED[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            i++;
            timer = 0f;
            startTimer = false;
            Debug.Log("Wrong");
        }
    }

    public void Retry()
    {
        i = 0;
        score = 0;
        startTimer = false;
        overallTimer = 0;
        for(int j = 0; j<4;j++)
        {
            LED[j].GetComponent<Image>().color = new Color32(154, 154, 154, 255);
        }
    }
}
