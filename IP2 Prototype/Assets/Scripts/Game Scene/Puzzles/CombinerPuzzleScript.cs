using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombinerPuzzleScript : PuzzleClass
{
    //PART 1
    [Header("Part 1 Solutions; Red = 1, Blue = 2, Green = 3, Yellow = 4")]
    public Color[] pattern1 = { Color.red, Color.red, Color.red, Color.red };
    public string part1Answer;
    //PART 2
    [Header("Part 2 Solutions; Red = B, Blue = A, Green = L, Yellow = E")]
    public Color[] pattern2 = { Color.blue, Color.green, Color.green, Color.blue };
    public string part2Answer;
    [Header("The LED lights on the scene")]
    public GameObject[] LED;
    [Header("The text objects the players 'Input' appears")]
    public Text input1, input2;

    public GameObject[] puzzlePanels;

    public bool waiting;
    public bool startPattern;
    int part1RandomPattern;
    int part2RandomPattern;
    int i = 0;


    /*
    ====================================================================================================
    Inherited PuzzleClass Methods
    ====================================================================================================
    */
    public override void GetRelativeSolution(FoodObject transformingFood)
    {
        if (part2RandomPattern == 0)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.blue, Color.green, Color.green, Color.red };
            part2Answer = "ALLB";
        }
        else if (part2RandomPattern == 1)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.red, Color.blue, Color.green, Color.green };
            part2Answer = "BALL";
        }
        else if (part2RandomPattern == 2)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.green, Color.blue, Color.red, Color.yellow };
            part2Answer = "LABE";
        }
        else if (part2RandomPattern == 3)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.green, Color.yellow, Color.blue, Color.red };
            part2Answer = "LEAB";
        }
        else if (part2RandomPattern == 4)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.yellow, Color.green, Color.red, Color.blue };
            part2Answer = "ELBA";
        }
        else if (part2RandomPattern == 5)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.blue, Color.red, Color.green, Color.yellow };
            part2Answer = "ABLE";
        }
        else if (part2RandomPattern == 6)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.red, Color.yellow, Color.blue, Color.green };
            part2Answer = "BEAL";
        }
        else if (part2RandomPattern == 7)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.red, Color.yellow, Color.green, Color.green };
            part2Answer = "BELL";
        }
        else if (part2RandomPattern == 8)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.blue, Color.green, Color.red, Color.yellow };
            part2Answer = "ALBE";
        }
        else if (part2RandomPattern == 9)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.blue, Color.yellow, Color.red, Color.yellow };
            part2Answer = "AEBE";
        }
        else if (part2RandomPattern == 10)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.blue, Color.green, Color.red, Color.blue };
            part2Answer = "ALBA";
        }
        else if (part2RandomPattern == 11)
        {
            pattern2 = null;
            pattern2 = new Color[] { Color.green, Color.blue, Color.blue, Color.red };
            part2Answer = "LAAB";
        }

        if (part1RandomPattern == 0)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.red, Color.red, Color.red, Color.red };
            part1Answer = "4";
        }
        else if (part1RandomPattern == 1)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.blue, Color.green, Color.green, Color.blue };
            part1Answer = "10";
        }
        else if (part1RandomPattern == 2)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.blue, Color.red, Color.green, Color.yellow };
            part1Answer = "10";
        }
        else if (part1RandomPattern == 3)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.blue, Color.blue, Color.green, Color.blue };
            part1Answer = "9";
        }
        else if (part1RandomPattern == 4)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.blue, Color.blue, Color.blue, Color.blue };
            part1Answer = "8";
        }
        else if (part1RandomPattern == 5)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.red, Color.green, Color.red, Color.blue };
            part1Answer = "7";
        }
        else if (part1RandomPattern == 6)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.green, Color.green, Color.green, Color.blue };
            part1Answer = "11";
        }
        else if (part1RandomPattern == 7)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.green, Color.green, Color.green, Color.green };
            part1Answer = "12";
        }
        else if (part1RandomPattern == 8)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.blue, Color.red, Color.green, Color.blue };
            part1Answer = "8";
        }
        else if (part1RandomPattern == 9)
        {
            pattern1 = null;
            pattern1 = new Color[] { Color.yellow, Color.yellow, Color.yellow, Color.yellow };
            part1Answer = "16";
        }
        startPattern = true;
    }

    public override bool CheckSolution()
    {
        //Checking Part 2 Answer
        if (input2.text == part2Answer)
        {
            LED[3].GetComponent<Image>().color = Color.green;
        }
        else
        {
            LED[3].GetComponent<Image>().color = Color.red;
        }

        //Checking Overall Answer
        if (LED[1].GetComponent<Image>().color != Color.white && LED[3].GetComponent<Image>().color != Color.white)
        {
            if (LED[1].GetComponent<Image>().color == Color.green && LED[3].GetComponent<Image>().color == Color.green)
            {
                Debug.Log("Puzzle Correct");
                return (true);
            }
            else if (LED[1].GetComponent<Image>().color == Color.red || LED[3].GetComponent<Image>().color == Color.red)
            {
                Debug.Log("Puzzle Wrong");
                return false;
            }
        }

        Debug.Log("Error");
        return false;
    }

    public override void ResetPuzzle()
    {
        input1.text = "";
        input2.text = "";

        //randomizes the pattern to use
        part1RandomPattern = Random.Range(0, 9);
        part2RandomPattern = Random.Range(0, 11);

        puzzlePanels[0].SetActive(true);
        puzzlePanels[1].SetActive(false);

        waiting = false;
        startPattern = false;
    }


    /*
    ====================================================================================================
    Unique Puzzle Methods
    ====================================================================================================
    */
    // Update is called once per frame
    void Update()
    {
        if (startPattern == true)
        {
            PlayPattern();
        }
    }

    public void PlayPattern()
    {
        if (!waiting)
        {
            if (i > 3)
            {
                LED[0].GetComponent<Image>().color = Color.black;
                LED[2].GetComponent<Image>().color = Color.black;
                StartCoroutine(Wait());
                i = 0;
            }
            else
            {
                StartCoroutine(Wait());
                LED[0].GetComponent<Image>().color = pattern1[i];
                LED[2].GetComponent<Image>().color = pattern2[i];
                i++;
            }
        }
    }

    IEnumerator Wait()
    {
        waiting = true;

        if (i > 3)
        {
            yield return new WaitForSeconds(2);
        }
        else
        {
            yield return new WaitForSeconds(1);
            LED[0].GetComponent<Image>().color = Color.white;
            LED[2].GetComponent<Image>().color = Color.white;
            yield return new WaitForSeconds(0.04f);
        }
        waiting = false;
    }

    public void SubmitPart1()
    {
        if (input1.text == part1Answer)
        {
            LED[1].GetComponent<Image>().color = Color.green;
        }
        else
        {
            LED[1].GetComponent<Image>().color = Color.red;
        }
        puzzlePanels[0].SetActive(false);
        puzzlePanels[1].SetActive(true);
    }

    public void ClearPart1()
    {
        if (LED[1].GetComponent<Image>().color == Color.white)
            input1.text = "";
    }

    public void SubmitPart2()
    {
        CheckSolution();
    }

    public void ClearPart2()
    {
        if (LED[3].GetComponent<Image>().color == Color.white)
            input2.text = "";
    }

    public void ButtonNumberType(int number)
    {
        if (LED[1].GetComponent<Image>().color == Color.white)
        {
            if (input1.text.Length < 2)
                input1.text += number;
        }
    }

    public void ButtonTextType(string text)
    {
        if (LED[3].GetComponent<Image>().color == Color.white)
            input2.text = text;
    }
}
