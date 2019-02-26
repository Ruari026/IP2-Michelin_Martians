using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("General Scene Components")]
    public Vector2 cameraOrbitRange;
    public float moveTime;

    [Header("Main Menu Components")]
    public Transform mainMenuCameraPos;

    [Header("Settings Menu Components")]
    public Transform settingsMenuCameraPos;
    public Animator settingsAnimController;

    [Header("Game Exit Components")]
    public Transform exitCameraPos;

    void Update()
    {
        OrbitCamera();
    }

    /*
    ==================================================
    Starting The Game
    ==================================================
    */
    public void MoveToGame()
    {
        SceneManager.LoadScene("LayoutTest1");
    }

    /*
    ==================================================
    Moving To & From Settings
    ==================================================
    */
    public void MoveToSettings()
    {
        StartCoroutine(MoveCamera(settingsMenuCameraPos.position, settingsMenuCameraPos.rotation, moveTime));
        settingsAnimController.SetBool("Opened", true);
    }

    public void MoveFromSettings()
    {
        StartCoroutine(MoveCamera(mainMenuCameraPos.position, mainMenuCameraPos.rotation, moveTime));
        settingsAnimController.SetBool("Opened", false);
    }


    /*
    ==================================================
    Quitting The Game
    ==================================================
    */
    public void MoveToQuit()
    {
        StartCoroutine(MoveCamera(exitCameraPos.position, exitCameraPos.rotation, moveTime * 1.5f));
        this.GetComponent<Animator>().SetTrigger("OnQuit");
        StartCoroutine(DelayGameClose());
    }

    private IEnumerator DelayGameClose()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }

    /*
    ==================================================
    Scene Camera Orbiting
    ==================================================
    */
    private void OrbitCamera()
    {
        //Rotating The Camera Based On Mouse Position
        {
            Vector3 newRotation = Vector3.zero;
            Vector2 mousePos = Input.mousePosition;

            mousePos.x = (mousePos.x / Screen.width) * 2 - 1;
            mousePos.y = (mousePos.y / Screen.height) * 2 - 1;

            mousePos.x = Mathf.Clamp(mousePos.x, -1.0f, 1.0f);
            mousePos.y = Mathf.Clamp(mousePos.y, -1.0f, 1.0f);

            //Debug.Log(mousePos);

            newRotation.x += (mousePos.y * -cameraOrbitRange.x);
            newRotation.y += (mousePos.x * cameraOrbitRange.y);

            Camera.main.transform.eulerAngles = newRotation + this.transform.eulerAngles;
        }
    }

    /*
    ==================================================
    General Movement Methods
    ==================================================
    */
    private IEnumerator MoveCamera(Vector3 targetPosition, Quaternion targetRotation, float moveTime)
    {
        StartCoroutine(LerpToPosition(this.transform.position, targetPosition, moveTime));
        StartCoroutine(LerpToRotation(this.transform.rotation, targetRotation, moveTime));

        yield return new WaitForSeconds(moveTime);
    }

    private IEnumerator LerpToPosition(Vector3 currentPosition, Vector3 targetPosition, float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);
            
            float i = t;
            float sStart = (i * i);
            float sEnd = (1 - (1 - i) * (1 - i));

            i = Mathf.Lerp(sStart, sEnd, t);

            this.transform.position = Vector3.Lerp(currentPosition, targetPosition, i);

            if (t >= 1)
            {
                this.transform.position = targetPosition;
            }

            yield return null;
        }
    }

    private IEnumerator LerpToRotation(Quaternion currentRotation, Quaternion targetRotation, float maxTime)
    {
        float t = 0;

        while (t < 1)
        {
            t += (Time.deltaTime / maxTime);
            
            float i = t;
            float sStart = (i * i);
            float sEnd = (1 - (1 - i) * (1 - i));

            i = Mathf.Lerp(sStart, sEnd, t);

            this.transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, i);

            if (t >= 1)
            {
                this.transform.rotation = targetRotation;
            }

            yield return null;
        }
    }
}
