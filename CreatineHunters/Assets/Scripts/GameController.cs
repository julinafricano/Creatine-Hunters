using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public Text healthText;

    public int score;
    public Text scoreText;

    public GameObject pauseObj;
    public GameObject gameOverObj;


    private bool isPaused;
    public static GameController instance;
    // Start is called before the first frame update
    void Awake()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }
    public void UpdateScore(int value)
    {
        score += value;
        scoreText.text = score.ToString();
    }
    public void UpdateLives(int value)
    {
        healthText.text = "x" + value.ToString();
    } 

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
            pauseObj.SetActive(isPaused);
        }
        if (isPaused)
        {
            Time.timeScale = 0f;

        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void GameOver()
    {
        gameOverObj.SetActive(true);
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
