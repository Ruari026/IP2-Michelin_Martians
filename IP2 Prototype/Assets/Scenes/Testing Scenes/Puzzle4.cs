using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle4 : MonoBehaviour
{
    public GameObject[] panels;
    public Color[] colourSet = { Color.red, Color.blue, Color.green, Color.yellow };
    [Header("Solution 1")]
    public Color[] sol1 = { Color.red, Color.red, Color.blue  };
    [Header("Solution 2")]
    public Color[] sol2 = { Color.green, Color.blue, Color.yellow, Color.green };
    [Header("Solution 3")]
    public Color[] sol3 = { Color.blue, Color.red, Color.blue, Color.yellow };
    [Header("Solution 4")]
    public Color[] sol4 = { Color.blue, Color.green, Color.yellow, Color.red };
    public int score = 0;

    public Text[] colourTexts;
    public Text endText;

    public Color[] currentSol;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            panels[i].GetComponent<Image>().color = colourSet[0];
            colourTexts[i].text = "R";
        }

        currentSol = sol2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpChange(int buttonNumber)
    {

        if(panels[buttonNumber].GetComponent<Image>().color == colourSet[0])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[1];
            colourTexts[buttonNumber].text = "B";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[1])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[2];
            colourTexts[buttonNumber].text = "G";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[2])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[3];
            colourTexts[buttonNumber].text = "Y";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[3])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[0];
            colourTexts[buttonNumber].text = "R";
        }
    }

    public void DownChange(int buttonNumber)
    {

        if (panels[buttonNumber].GetComponent<Image>().color == colourSet[0])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[3];
            colourTexts[buttonNumber].text = "Y";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[3])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[2];
            colourTexts[buttonNumber].text = "G";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[2])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[1];
            colourTexts[buttonNumber].text = "B";
        }
        else if (panels[buttonNumber].GetComponent<Image>().color == colourSet[1])
        {
            panels[buttonNumber].GetComponent<Image>().color = colourSet[0];
            colourTexts[buttonNumber].text = "R";
        }
    }

    public void CheckSol()
    {
        for (int i = 0; i < currentSol.Length; i++)
        {
            if(panels[i].GetComponent<Image>().color == currentSol[i])
            {
                Debug.Log("YAY");
                score++;
            }
        }
        if(score == currentSol.Length)
        {
            Debug.Log("Win");
            endText.text = "Correct";
            score = 0;
        }
        else
        {
            Debug.Log("Lose");
            endText.text = "Incorrect";
            score = 0;
        }
    }

}
