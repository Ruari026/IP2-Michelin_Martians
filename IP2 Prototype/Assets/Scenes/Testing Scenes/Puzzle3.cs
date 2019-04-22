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
    bool rotating;
    bool mouseCheck;
    bool buttonSwtichCheck;
    Vector3 mouseStartPos;
    Vector3 mouseEndPos;

    //identify which object is to be turned
    [Header("Object That Is Turned, Drag mouse left/right")]
    public GameObject turnableObject;
    [Header("Solution 1")]
    public int[] sol1 = {2, 0, 4, 15};
    [Header("1 - positive rotation, 2 - negative roation, 0 - at zero")]
    public int[] sol1Directions = { 1, 0, 1, 2 };
    [Header("Solution 2")]
    public int[] sol2 = { 5, 4, 14, 10 };
    [Header("1 - positive rotation, 2 - negative roation, 0 - at zero")]
    public int[] sol2Directions = { 1, 1, 1, 1 };
    [Header("Solution 3")]
    public int[] sol3 = { 12, 5, 3, 9 };
    [Header("1 - positive rotation, 2 - negative roation, 0 - at zero")]
    public int[] sol3Directions = { 2, 1, 1, 1};
    [Header("Solution 4")]
    public int[] sol4 = { 6, 15, 1, 11 };
    [Header("1 - positive rotation, 2 - negative roation, 0 - at zero")]
    public int[] sol4Directions = { 1, 2, 1, 2 };

    public Text[] inputResults;

    public int[] currentSolution;
    public int[] currentDirectionSolution;
    public int[] playerSolution;
    public int playerPrevSolution = 0;
    public int[] playerDirectionSolution;

    public GameObject pivotPoint;

    // Start is called before the first frame update
    void Start()
    {
        currentSolution = sol1;
        currentDirectionSolution = sol1Directions;
    }

    bool atZero = true; //at centre
    bool poz; //positive direction
    bool neg; //negative direction
    bool active = true;
    float angle;
    Vector3 nextAngle;
    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            //first, continuous check of directions - shows if in the positive 360 or negative
            if (turnableObject.transform.eulerAngles.z < 22.5f || turnableObject.transform.eulerAngles.z > 338.5f)
            {
                atZero = true;
                poz = false;
                neg = false;
            }
            if (atZero == true && (turnableObject.transform.eulerAngles.z >= 22.5f && turnableObject.transform.eulerAngles.z <= 180f))
            {
                poz = false;
                neg = true;
                atZero = false;
            }
            if (atZero == true && (turnableObject.transform.eulerAngles.z <= 337.5f && turnableObject.transform.eulerAngles.z >= 180f))
            {
                poz = true;
                neg = false;
                atZero = false;
            }

            if (rotating == true)
            {
                    //creates an angle to rotate
                    angle = Quaternion.FromToRotation((mouseStartPos - pivotPoint.transform.position), Input.mousePosition - pivotPoint.transform.position).eulerAngles.z;
                    //rotates the angle as appropriate
                    turnableObject.transform.localEulerAngles = new Vector3(0, 0, angle) + nextAngle;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Touching");
                rotating = true;
                oldRotation = turnableObject.transform.eulerAngles.z;
                mouseStartPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
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

                        rotateAmount = 16 - i;
                        if (rotateAmount == 16)
                            rotateAmount = 0;

                        break;
                    }
                }
                //get new angle to add to rotation, prevents snap to zero.
                nextAngle.z = turnableObject.transform.eulerAngles.z;
                //final check for directions
                if (turnableObject.transform.eulerAngles.z < 22.4f || turnableObject.transform.eulerAngles.z > 337.6f)
                {
                    atZero = true;
                    poz = false;
                    neg = false;
                }
                if (atZero == true && (turnableObject.transform.eulerAngles.z >= 22.4f && turnableObject.transform.eulerAngles.z <= 180f))
                {
                    poz = false;
                    neg = true;
                    atZero = false;
                }
                if (atZero == true && (turnableObject.transform.eulerAngles.z <= 337.6f && turnableObject.transform.eulerAngles.z >= 180f))
                {
                    poz = true;
                    neg = false;
                    atZero = false;
                }
                CheckAnswer();
            }
        }
        
    }

    public void CheckAnswer()
    {
            playerSolution[i] = rotateAmount;

            playerPrevSolution = playerSolution[i];

            if (poz)
                playerDirectionSolution[i] = 1;
            else if (neg)
                playerDirectionSolution[i] = 2;
            else
                playerDirectionSolution[i] = 0;


        Debug.Log(playerSolution[i]);
        if (i < 4)
        {
            if (playerSolution[i] == currentSolution[i] && playerDirectionSolution[i] == currentDirectionSolution[i])
            {
                Debug.Log("Yay");
                inputResults[i].text = playerSolution[i].ToString();
                i++;
                score++;
            }
            else if (playerSolution[i] != currentSolution[i] || playerDirectionSolution[i] != currentDirectionSolution[i])
            {
                Debug.Log("Nay");
                inputResults[i].text = playerSolution[i].ToString();
                i++;
            }
        }
        if(i == 4)
        {
            if (score == 4)
            {
                Debug.Log("Win");
                active = false;
                if (playerSolution[3] >= 10)
                    buttonSwtichCheck = true;
                else if (playerSolution[3] < 10)
                    buttonSwtichCheck = false;
            }
            else
            {
                Debug.Log("Lose");
                active = false;
                if (playerSolution[3] >= 10)
                    buttonSwtichCheck = true;
                else if (playerSolution[3] < 10)
                    buttonSwtichCheck = false;
            }
        }
    }

    public void ButtonPress()
    {
        if(buttonSwtichCheck == false && active == false)
        {
            Debug.Log("Task Complete");
        }
    }

    public void SwitchPress()
    {
        if (buttonSwtichCheck == true && active == false)
        {
            Debug.Log("Task Complete");
        }
    }
}