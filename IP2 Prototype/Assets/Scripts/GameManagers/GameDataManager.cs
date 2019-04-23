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

    public static void SetRecipeName(int levelNumber)
    {
        switch (levelNumber)
        {
            case (1):
                {
                    int r = Random.Range(0, 3);
                    if (r == 0)
                    {
                        GameDataManager.recipeName = "Eggs";
                    }
                    else if (r == 1)
                    {
                        GameDataManager.recipeName = "Soup";
                    }
                    else
                    {
                        GameDataManager.recipeName = "Fish Fillets";
                    }
                }
                break;

            case (2):
                {
                    int r = Random.Range(0, 2);
                    if (r == 0)
                    {
                        GameDataManager.recipeName = "Steak";
                    }
                    else
                    {
                        GameDataManager.recipeName = "Sushi";
                    }
                }
                break;

            case (3):
                {
                    GameDataManager.recipeName = "Surf & Turf";
                }
                break;
        }
    }

    public static string GetRecipeName()
    {
        return GameDataManager.recipeName;
    }
}
