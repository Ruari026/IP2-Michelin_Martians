using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject dataManager;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject g = GameObject.Find("GameDataManager(Clone)");
        if (g == null)
        {
            GameObject.Instantiate(dataManager);
        }
    }
    private void Update()
    {
        //Game Reset for playtesting
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                SceneManager.LoadScene("Main Menu");
            }
        }
    }

    //once called this loads the main menu after the sound is played
    IEnumerator DelayLoadMainMenu()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("MainMenu");
    }
    //once called this loads the settings menu after the sound is played
    IEnumerator DelayLoadSettings()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("Settings");
    }


    /*
    ====================================================================================================
    Scene Changing
    ====================================================================================================
    */
    public void ToMainGame()
    {
        StartCoroutine(DelayLoadMainGame());
    }
    //once called this loads the main game after the sound is played
    IEnumerator DelayLoadMainGame()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("Game Scene");
    }

    public void ToLevelSelect()
    {

    }

    public void ToMainMenu()
    {
        StartCoroutine(DelayLoadMainMenu());
    }

    public void ToSettings()
    {
        StartCoroutine(DelayLoadSettings());
    }

    public void QuitGame()
    {
        StartCoroutine(DelayLoadQuit());
    }
    //once called this quits the game after the sound is played
    IEnumerator DelayLoadQuit()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Application.Quit();
    }

}
