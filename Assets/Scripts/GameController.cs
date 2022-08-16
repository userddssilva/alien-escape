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

    [SerializeField]
    private Image image2;
    [SerializeField]
    private Image image3;

    public GameObject finishedGame;

    public static GameController instance;
    void Start()
    {
        instance = this;
        image2.enabled = false;
        image3.enabled = false;
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "level_1") level1Start = Time.time;
        if (sceneName == "level_2") level2Start = Time.time;
    }

    public void ShowFinishedGame()
    {
        CountStars();
        Debug.Log(image2.enabled);
        Debug.Log(image3.enabled);
        finishedGame.SetActive(true);
    }

    public void CountStars()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        // GameObject playerUI = GameObject.FindGameObjectWithTag("PlayerUI");
        // Image[] stars = playerUI.GetComponents<Image>();
        // Debug.Log(stars);
        int starsCount = 1;
        
        if (sceneName == "level_1")
        {
            if (level1Turns <= 6) {
                starsCount += 1;
            }

            if (Time.time <= 16) {
                starsCount += 1;
            }
        }

        if (sceneName == "level_2")
        {
            if (level1Turns <= 10) {
                starsCount += 1;
            }

            if (Time.time <= 31) {
                starsCount += 1;
            }
        }

        if (starsCount == 2) {
            image2.enabled = true;
        }

        if (starsCount == 3) {
            image2.enabled = true;
            image3.enabled = true;
        }

        Debug.Log("stars:" + starsCount);
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
