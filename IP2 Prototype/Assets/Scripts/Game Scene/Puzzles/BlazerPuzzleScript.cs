using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BlazerPuzzleScript : PuzzleClass, IPointerDownHandler, IPointerUpHandler
{
    //overall timer for the device, the maximum that time goes up to anda boolean that checks if puzzle started
    float overallTimer = 0f;
    bool activated;
    int minTime;
    int maxTime;
    public Text overallTimerText;
    public Slider overallTimerSlider;
    public Image timerSliderFill;

    //used to identify which LED is used
    int i = 0;
    [Header("List of LED lights")]
    public List<GameObject> LED;
    [Header("1 for low, 2 for med, 3 for high")]
    public int temperature = 0;

    //button timer, check to see timer start and check to see if button held
    private float timer = 0.0f;
    bool hold;
    bool startTimer;
    int score = 0;

    //solutions
    [Header("If low temp required.")]
    [Header("True - Held, False - Press")]
    public List<FoodObject> solution1Ingredients;
    public bool[] solution1;
    [Header("If medium temp required.")]
    public List<FoodObject> solution2Ingredients;
    public bool[] solution2;
    [Header("If high temp required.")]
    public List<FoodObject> solution3Ingredients;
    public bool[] solution3;
    public bool[] currentSolution;
    private bool[] playerSolution;

    public override void GetRelativeSolution(FoodObject transformingFood)
    {
        if (solution1Ingredients.Contains(transformingFood))
        {
            temperature = 1;
            Debug.Log("Using Solution 1");
            currentSolution = solution1;
            activated = true;
            minTime = 15;
            maxTime = 30;
            overallTimerSlider.maxValue = 70;
        }
        else if (solution2Ingredients.Contains(transformingFood))
        {
            temperature = 2;
            Debug.Log("Using Solution 2");
            currentSolution = solution2;
            activated = true;
            minTime = 30;
            maxTime = 50;
            overallTimerSlider.maxValue = 70;
        }
        else if (solution3Ingredients.Contains(transformingFood))
        {
            temperature = 3;
            Debug.Log("Using Solution 3");
            currentSolution = solution3;
            activated = true;
            minTime = 50;
            maxTime = 70;
            overallTimerSlider.maxValue = 70;
        }
    }

    public override bool CheckSolution()
    {
        if (hold == currentSolution[i])
        {
            LED[i].GetComponent<Image>().color = new Color32(0, 0, 255, 255);
            
            if (overallTimer < maxTime && overallTimer > minTime)
            {
                score++;
                Debug.Log(score);
                LED[i].GetComponent<Image>().color = new Color32(0, 255, 0, 255);
            }
            i++;
            timer = 0f;
            startTimer = false;
            Debug.Log("Correct");
        }
        else
        {
            LED[i].GetComponent<Image>().color = new Color32(255, 0, 0, 255);
            i++;
            
            timer = 0f;
            startTimer = false;
            Debug.Log("Wrong");
        }

        if (i == 4)
        {
            if (overallTimer < maxTime && overallTimer > minTime && score == 4)
            {
                activated = false;
                parentController.CheckPuzzle(true);
                return true;
            }
            else
            {
                activated = false;
                parentController.CheckPuzzle(false);
                return true;
            }

        }

        return false;
    }

    public override void ResetPuzzle()
    {
        i = 0;
        score = 0;
        startTimer = false;
        overallTimer = 0;
        for (int j = 0; j < 4; j++)
        {
            LED[j].GetComponent<Image>().color = new Color32(154, 154, 154, 255);
        }
    }


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

    // Update is called once per frame
    void Update()
    {
        if (buttonPressed)  //button is pressed down, start the timer
        {
            Debug.Log("Button Pressed, timer Start");
            startTimer = true;
            timer += Time.deltaTime;
        }

        if (!buttonPressed && startTimer) //if the timer has been started and the button is no longer held down - check how long held for
        {
            if (timer >= 0.5f)
            {
                hold = true;
                CheckSolution();
            }
            else if (timer < 0.5f)
            {
                hold = false;
                CheckSolution();
            }
        }

        if (activated)
        {
            overallTimer += Time.deltaTime * 5;
            overallTimerText.text = string.Format("{00}", (int)overallTimer);
            overallTimerSlider.value = overallTimer;
            if (overallTimer > 15 && overallTimer < 30)
            {
                timerSliderFill.color = Color.blue;
            }
            else if (overallTimer > 30 && overallTimer < 50)
            {
                timerSliderFill.color = Color.yellow;
            }
            else if (overallTimer > 50 && overallTimer < 70)
            {
                timerSliderFill.color = Color.red;
            }
            else if ((int)overallTimer == 70)
            {
                overallTimer = 0;
                timerSliderFill.color = Color.green;
            }
            else if ((int)overallTimer >=0 && (int)overallTimer <15)
            {
                timerSliderFill.color = Color.green;
            }
        }
    }
}
