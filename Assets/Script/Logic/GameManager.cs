using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public float levelTime = 600; // 10 minutes in seconds
    public TextMeshProUGUI timerText; // Reference to the TextMeshPro UI component
    public GameObject gameOverPanel;  // Reference to the Game Over Panel
    public ItemManager itemManager; // Reference to the Item Manager

    private void Start()
    {
        gameOverPanel.SetActive(false); // Hide the game over panel initially
        itemManager = GetComponent<ItemManager>(); // Ensure ItemManager is attached to the same GameObject
    }

    private void Update()
    {
        if (levelTime > 0)
        {
            levelTime -= Time.deltaTime;
            UpdateTimerDisplay(levelTime);
            itemManager.UpdateItemDecay(levelTime); // Update decay state of items based on the current time
            if (levelTime <= 0)
            {
                levelTime = 0;
                GameOver();
            }
        }
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        //Debug.Log("Game Over Started");
        gameOverPanel.SetActive(true); // Show the game over panel
        //Debug.Log("Game Over Panel Shown");
        Invoke("LoadMainMenu", 3f); // Schedule LoadMainMenu to be called after 3 seconds
        //Debug.Log("Invoke LoadMainMenu scheduled");
    }

    public void LoadMainMenu()
    {
        //Debug.Log("Attempting to load Main Menu.");
        try
        {
            SceneManager.LoadScene("MainMenu");
            //Debug.Log("Main Menu should now be loading.");
        }
        catch (Exception ex)
        {
            Debug.LogError("Failed to load Main Menu: " + ex.Message);
        }
    }
}
