using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameController : MonoBehaviour
{

    // public int totalScore;
    // public Text scoreText;
    public bool isTurning = false;
    public bool isCollectedKey = false;
    public GameObject gameOver;

    public static GameController instance;
    void Start()
    {
        instance = this;
    }

    // public void UpdateScoreText()
    // {
    //     scoreText.text = totalScore.ToString();
    // }

    public void ShowGameOver()
    {
        gameOver.SetActive(true);
    }

    public void CollectKey()
    {
        isCollectedKey = true;
    }

    public bool IsCollectedKey()
    {
        return isCollectedKey;
    }

    public bool IsTurning()
    {
        return isTurning;
    }

    public void ToggleTurning(bool state)
    {
        isTurning = state;
    }

    public void RestartGame(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }
}
