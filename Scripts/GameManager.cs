using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static int totalCoins = 0;
    static int highestLevel = 0;

    private void Awake()
    {
        highestLevel = PlayerPrefs.GetInt("highestLevel", 0);
        totalCoins = PlayerPrefs.GetInt("totalCoins", 0);
    }

    public static void AddToTotalCoins(int coinsNumber)
    {
        totalCoins = totalCoins + coinsNumber;
        PlayerPrefs.SetInt("totalCoins", totalCoins);
    }

    public static void LevelFinished(int levelNumber)
    {
        if (levelNumber + 1 > highestLevel)
        {
            highestLevel = levelNumber + 1;
            PlayerPrefs.SetInt("highestLevel", highestLevel);
            print("highest = " + highestLevel);
        }
    }

    public static int GetHighstLevel()
    {
        return highestLevel;
    }
}
