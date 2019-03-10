using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    //once called this loads the main game after the sound is played
    IEnumerator DelayLoadMainGame()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        SceneManager.LoadScene("Game Scene");
    }
    //once called this quits the game after the sound is played
    IEnumerator DelayLoadQuit()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Application.Quit();
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

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void ToMainGame()
    {
        StartCoroutine(DelayLoadMainGame());
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

}
