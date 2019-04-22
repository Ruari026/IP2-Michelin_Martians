using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    private static string recipeName;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void SetRecipeName(int levelNumber)
    {
        switch (levelNumber)
        {
            case (1):
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        recipeName = "Eggs";
                    }
                    else
                    {
                        recipeName = "Soup";
                    }
                }
                break;

            case (2):
                {

                }
                break;

            case (3):
                {

                }
                break;
        }
    }

    public static string GetRecipeName()
    {
        return GameDataManager.recipeName;
    }
}
