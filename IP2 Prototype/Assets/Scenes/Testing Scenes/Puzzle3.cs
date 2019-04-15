using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Puzzle3 : MonoBehaviour
{ 
    int rotateAmount = 0;
    int i = 0;
    int score = 0;
    float oldRotation;
    float newRotation;
    bool turningR;
    bool turningL;
    bool rotating;
    bool mouseCheck;
    Vector3 mouseStartPos;
    Vector3 mouseEndPos;

    //identify which object is to be turned
    [Header("Object That Is Turned, Drag mouse left/right")]
    public GameObject turnableObject;
    [Header("Solution 1")]
    public int[] sol1 = {2, 0, 4, 15};
    [Header("(AMMO; >,<,>,<), true = right, false = left")]
    public bool[] sol1Directions = { true, false, true,false };
    [Header("Solution 2")]
    public int[] sol2 = { 5, 4, 14, 10 };
    [Header("(WMMW; >,<,>,<), true = right, false = left")]
    public bool[] sol2Directions = { true, false, true, false };
    [Header("Solution 3")]
    public int[] sol3 = { 12, 5, 3, 9 };
    [Header("(WWUV; <,>,<,>), true = right, false = left")]
    public bool[] sol3Directions = { false, true, false, true };
    [Header("Solution 4")]
    public int[] sol4 = { 6, 15, 1, 11 };
    [Header("(OOVA; >,<,>,<), true = right, false = left")]
    public bool[] sol4Directions = { true, false, true, false };

    public int[] currentSolution;
    public bool[] currentDirectionSolution;
    public int[] playerSolution;
    public int playerPrevSolution = 0;
    public bool[] playerDirectionSolution;

    public GameObject pivotPoint;


    bool mouseR;
    bool mouseL;


    // Start is called before the first frame update
    void Start()
    {
        currentSolution = sol1;
        currentDirectionSolution = sol1Directions;
    }


    // Update is called once per frame
    void Update()
    {
        
        if (rotating == true)
        {
            float angle = Quaternion.FromToRotation(mouseStartPos - pivotPoint.transform.position, Input.mousePosition - pivotPoint.transform.position).eulerAngles.z;

            turnableObject.transform.localEulerAngles = new Vector3(0, 0, angle);
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Touching");
            rotating = true;
            oldRotation = turnableObject.transform.eulerAngles.z;
            mouseStartPos = Input.mousePosition;
        }

        if(Input.GetMouseButtonUp(0))
        {
            rotating = false;
            newRotation = turnableObject.transform.eulerAngles.z;
            mouseEndPos = Input.mousePosition;
            mouseCheck = true;
            

            //for snap feature
            float turnAmount = turnableObject.transform.eulerAngles.z; //the current angle when click let go
            for (int i = 0; i < 17; i++) //to check each of the 16 "segments"
            {

                float rAmount = (22.5f / 2) + 22.5f * i; //how far to the right the angle can be before snap
                float lAmount = rAmount - 22.5f; //how far to left before snap
                if (turnAmount <= rAmount && turnAmount > lAmount) //checks if it's between two angles of the 16 "segments"
                {
                    //rotates to the correct "segment" as a 'Snap'
                    turnableObject.transform.eulerAngles = new Vector3(0, 0, i * 22.5f);
                    if (i == 16)
                        i = 0;
                    rotateAmount = i;
                    break;
                }
            }
            CheckAnswer();
        }
        //used to check the direction that the mouse is moving therefore the direction of rotation

/*
        if(mouseCheck == true)
        {
            if (mouseEndPos.x < mouseStartPos.x)
            {
                Debug.Log("Left Drag, Right Movement");
                turningL = false;
                turningR = true;
                mouseCheck = false;
                CheckAnswer();
            }
            else if (mouseEndPos.x > mouseStartPos.x)
            {
                Debug.Log("Right Drag, Left Movement");
                turningR = false;
                turningL = true;
                mouseCheck = false;
                CheckAnswer();
            }
        }
        */
    }

    public void CheckAnswer()
    {
        playerSolution[i] = rotateAmount;
        if(playerSolution[i] < playerPrevSolution)
        {
            turningL = false;
            playerPrevSolution = playerSolution[i];
            playerDirectionSolution[i] = false;
        }
        else if(playerSolution[i] > playerPrevSolution)
        {
            turningR = false;
            playerDirectionSolution[i] = true;
        }

        Debug.Log(playerSolution[i]);
        if (i < 4)
        {
            if (playerSolution[i] == currentSolution[i] && playerDirectionSolution[i] == currentDirectionSolution[i])
            {
                Debug.Log("Yay");
                i++;
                score++;
            }
            else if (playerSolution[i] != currentSolution[i] || playerDirectionSolution[i] != currentDirectionSolution[i])
            {
                Debug.Log("Nay");
                i++;
            }
        }
        if(i == 4)
        {
            if (score == 4)
            {
                Debug.Log("Win");
            }
            else
            {
                Debug.Log("Lose");
            }
        }
    }
}