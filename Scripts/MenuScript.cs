using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum ScenesNames
{
    MainMenu,
    Info,
    LevelsMenu,
    Tutorial,
    Level,//then add number 'level1/level2......'
}

public class MenuScript : MonoBehaviour
{
    public static bool soundOnOff;
    public Button soundButton;
    public Button[] levelsButtons;

    private void Start()
    {
        SetSound();
        EnableLevelButtons();
    }

    #region sound
    public void SetSound()
    {
        if (soundButton != null)
        {
            soundOnOff = (PlayerPrefs.GetInt("soundOnOff", 1) == 1); // 1 => sound is on
            soundButton.image.sprite = Resources.Load<Sprite>("Images/Sound" + PlayerPrefs.GetInt("soundOnOff", 1));
        }
    }
    public void ChangeSoundOnOff()
    {
        soundOnOff = !soundOnOff;
        int num = soundOnOff ? 1 : 0;
        PlayerPrefs.SetInt("soundOnOff", num);
        soundButton.image.sprite = Resources.Load<Sprite>("Images/Sound" + num);
    }
    void PlaySound()
    {
        Time.timeScale = 1;
        GetComponentInParent<AudioSource>().Play();
    }
    #endregion

    #region buttons
    //Open MainMenu
    public void GoHome()
    {
        PlaySound();
        //move to home scene
        SceneManager.LoadScene(ScenesNames.MainMenu.ToString());
    }

    //Reload the current scene
    public void ResetLevel()
    {
        PlaySound();
        //load the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //play the next level or the highest level if starting from menu
    public void NextLevel()
    {
        PlaySound();
        //play the next scene

        if (GameManager.GetHighstLevel() == 0)
        {
            SceneManager.LoadScene(ScenesNames.Tutorial.ToString());
        }
        else if (GetCurrentSceneNumber() != 0)
        {
            SceneManager.LoadScene(ScenesNames.Level.ToString() + GetCurrentSceneNumber());
        }
        else
        {
            SceneManager.LoadScene(ScenesNames.Level.ToString() + GameManager.GetHighstLevel());
        }
    }

    public void PlayLevel(int levelNum)
    {
        if (levelNum <= GameManager.GetHighstLevel())
        {
            PlaySound();

            if (levelNum == 0)
            {
                SceneManager.LoadScene(ScenesNames.Tutorial.ToString());
            }
            else
            {
                SceneManager.LoadScene(ScenesNames.Level.ToString() + levelNum);
            }
        }
    }
    //Open the levels menu
    public void OpenLevels()
    {
        PlaySound();
        //open levels menu
        SceneManager.LoadScene(ScenesNames.LevelsMenu.ToString());

    }

    //opens the info page
    public void OpenInfo()
    {
        PlaySound();
        //open info scene
        SceneManager.LoadScene(ScenesNames.Info.ToString());
    }
    #endregion

    #region levelsMenu
    public void EnableLevelButtons()
    {
        if (levelsButtons != null)
        {
            if (levelsButtons.Length > 0)
            {
                int maxlevel = GameManager.GetHighstLevel() < 5 ? GameManager.GetHighstLevel() : 5;
                print("maxlevel = " + maxlevel);
                for (int i = 0; i <= maxlevel; i++)
                {
                    //levelsButtons[i].image.color = Color.white;
                    levelsButtons[i].interactable = true;
                }
            }
        }
    }
    #endregion

    //if in level we get the number of the level we are in (tutorial and everything else == 0)
    public static int GetCurrentSceneNumber()
    {
        return SceneManager.GetActiveScene().name.Contains("Level") ? int.Parse(SceneManager.GetActiveScene().name.Replace("Level", "")) : 0;
    }

}
