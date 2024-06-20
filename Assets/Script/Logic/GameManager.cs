using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;
using System.Collections;


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
            if (levelTime <= 0)
            {
                levelTime = 0;
                GameOver();
            }
        }

        HandleEscapeKeyPress();
    }

    private void HandleEscapeKeyPress()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SaveProgress();
            // Optionally add other logic for pausing the game or showing a pause menu
            Debug.Log("Game paused and progress saved.");
        }
    }

    private void SaveProgress()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        PlayerPrefs.SetInt("UnlockedLevel", Mathf.Max(PlayerPrefs.GetInt("UnlockedLevel", 1), currentLevelIndex));
        PlayerPrefs.Save();
        Debug.Log("Progress saved: Level " + currentLevelIndex + " is now unlocked.");
    }

    void UpdateTimerDisplay(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true); // Ensure this is correctly activating the Game Over UI
        Debug.Log("Game Over. Showing Game Over panel.");
        StartCoroutine(TransitionToMainMenu());
    }

    private IEnumerator TransitionToMainMenu()
    {
        yield return new WaitForSeconds(3); // Wait for 3 seconds to show the panel
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); // Ensure this scene name is correct
        Debug.Log("Transitioning to Main Menu.");
    }

}
