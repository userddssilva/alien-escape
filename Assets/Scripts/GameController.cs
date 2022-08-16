using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public bool isGravityChanging = false;
    public bool isCollectedKey = false;
    public float lastAngle = 0f;

    public int level1Turns = 0;
    public int level2Turns = 0;
    
    public float level1Start = 0f;
    public float level2Start = 0f;

    public GameObject finishedGame;

    public static GameController instance;
    void Start()
    {
        instance = this;
    }

    public void ShowFinishedGame()
    {
        CountStars();
        finishedGame.SetActive(true);
    }

    public void CountStars()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        Image[] stars = playerUI.GetComponents<Image>();
        int starsCount = 1;

        if (sceneName == "level_1")
        {
            if (level1Turns <= 6) {
                starsCount += 1;
            }
        }

        if (starsCount == 2) {
            stars[1].enabled = true;
        }
    }

    public void CollectKey()
    {
        isCollectedKey = true;
    }

    public bool IsCollectedKey()
    {
        return isCollectedKey;
    }

    public bool IsGravityChanging()
    {
        return isGravityChanging;
    }

    public void ToggleGravityChanging()
    {
        isGravityChanging = !isGravityChanging;
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
