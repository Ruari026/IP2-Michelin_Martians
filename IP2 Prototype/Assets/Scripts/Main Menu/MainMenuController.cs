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
        SceneManager.LoadScene("LayoutTest1");
    }
    //once called this quits the game after the sound is played
    IEnumerator DelayLoadQuit()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(GetComponent<AudioSource>().clip.length);
        Application.Quit();
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

    public void QuitGame()
    {
        StartCoroutine(DelayLoadQuit());
    }

}
