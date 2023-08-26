using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public bool isGameActive;
    public TextMeshProUGUI gameOverText;
    public Button RestartButton;
    public Button PauseButton;
    public GameObject titleScreen;
    public bool isPaused = false;
    public bool isClicked = false;
    private int score;

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game over!\r\n You total score: " + score;
        isGameActive = false;
        //RestartButton.gameObject.SetActive(true);
        //PauseButton.gameObject.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Pause()
    {
        if (isClicked == false && isPaused == false)
        {
            isPaused = true;
            Time.timeScale = 0f;
            isClicked = true;
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            isClicked = false;
        }
    }
}
