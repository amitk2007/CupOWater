using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject publicWinCanvas;
    public GameObject publicLoseCanvas;
    public TextMeshProUGUI publicCoinsText;
    public TextMeshProUGUI publicEndCoinsText;

    static GameObject winCanvas;
    static GameObject loseCanvas;
    static TextMeshProUGUI coinsText;
    static TextMeshProUGUI endCoinsText;


    // Start is called before the first frame update
    void Start()
    {
        winCanvas = publicWinCanvas;
        loseCanvas = publicLoseCanvas;
        coinsText = publicCoinsText;
        endCoinsText = publicEndCoinsText;
    }

    public static void UpdateCoinText()
    {
        coinsText.text = "Coins: " + PlayerController.coinAmount;
    }

    public static void LevelFinished(bool success)
    {
        if (success == true)
        {
            //WON
            winCanvas.SetActive(true);
            endCoinsText.text = "you got " + PlayerController.coinAmount.ToString() + " coins";

            //update the global variables
            GameManager.LevelFinished(MenuScript.GetCurrentSceneNumber());
            GameManager.AddToTotalCoins(PlayerController.coinAmount);
        }
        if (success == false)
        {
            //died
            loseCanvas.SetActive(true);
            //endCoinsText.text = "";
        }
        Time.timeScale = 0;
    }
}
