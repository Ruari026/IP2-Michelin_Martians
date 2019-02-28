using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleScript : MonoBehaviour
{
    public int puzzleNumber; //Which Puzzle it is
    public int puzzleID; //Which variation it is
    public GameObject[] puzzleObject; //the puzzle itself

    //Puzzle 1 Variables:
    int[] solution; //the solutions to the puzzles
    int[] nonSolution; //the nonsolutions
    public Slider[] p1Sliders; //sliders for puzzle 1

    // Start is called before the first frame update
    void Start()
    {

        if (puzzleNumber==1)
        {
            puzzleObject[0].SetActive(true);
            
            Puzzle1();
        }
    }


    // Update is called once per frame
    void Update()
    {
    }

    public void Puzzle1()
    {
        /*
if id = 1
solution = 1,5,6,8 full; 2,3,4,7,9 empty
if id = 2
solution = 2,5,7,9
if id = 3
sol = 1,3,4,6
if id = 4
sol= 3,5,7,8

*/
        if (puzzleID == 1)
        {
            solution = new int[4];
            solution[0] = 0;
            solution[1] = 4;
            solution[2] = 5;
            solution[3] = 7;

            nonSolution = new int[5];
            nonSolution[0] = 1;
            nonSolution[1] = 2;
            nonSolution[2] = 3;
            nonSolution[3] = 6;
            nonSolution[4] = 8;
        }
        else if (puzzleID == 2)
        {
            solution = new int[4];
            solution[0] = 1;
            solution[1] = 4;
            solution[2] = 6;
            solution[3] = 8;

            nonSolution = new int[5];
            nonSolution[0] = 0;
            nonSolution[1] = 2;
            nonSolution[2] = 3;
            nonSolution[3] = 5;
            nonSolution[4] = 7;
        }
        else if (puzzleID == 3)
        {
            solution = new int[4];
            solution[0] = 0;
            solution[1] = 2;
            solution[2] = 3;
            solution[3] = 5;

            nonSolution = new int[5];
            nonSolution[0] = 1;
            nonSolution[1] = 4;
            nonSolution[2] = 6;
            nonSolution[3] = 7;
            nonSolution[4] = 8;
        }
        else if (puzzleID == 4)
        {
            solution = new int[4];
            solution[0] = 2;
            solution[1] = 4;
            solution[2] = 6;
            solution[3] = 7;

            nonSolution = new int[5];
            nonSolution[0] = 0;
            nonSolution[1] = 1;
            nonSolution[2] = 3;
            nonSolution[3] = 5;
            nonSolution[4] = 8;
        }
    }

    public void CheckPuzzle1()
    {
        if (p1Sliders[nonSolution[0]].value == 0 && p1Sliders[nonSolution[1]].value == 0 && p1Sliders[nonSolution[2]].value == 0 && p1Sliders[nonSolution[3]].value == 0 && p1Sliders[nonSolution[4]].value == 0)
        {
            if (p1Sliders[solution[0]].value == 1 && p1Sliders[solution[1]].value == 1 && p1Sliders[solution[2]].value == 1 && p1Sliders[solution[3]].value == 1)
            {
                Debug.Log("YOU PASS");
            }
        }
    }


}
