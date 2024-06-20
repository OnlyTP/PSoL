using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Button[] buttons;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log("Unlocked levels up to: " + unlockedLevel);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;  // Ensure all are initially set to false
        }
        for (int i = 0; i < unlockedLevel; i++)
        {
            if (i < buttons.Length)
            {
                buttons[i].interactable = true;  // Prevent accessing out of bounds index
            }
        }
    }


    public void StartLevel(int levelId)
    {
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();  // Find the PlayerStats component in the scene
        if (playerStats != null)
        {
            PlayerPrefs.SetInt("CurrentHealth", playerStats.maxHealth); // Reset health using the player's maxHealth
        }
        else
        {
            Debug.LogError("PlayerStats component not found in the scene.");
        }

        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
