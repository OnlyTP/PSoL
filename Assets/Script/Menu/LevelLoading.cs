using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

    public Button[] buttons;

    private void Awake()
    {
        int unlockedlevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }
        for (int i = 0; i< unlockedlevel; i++)
        {
            buttons[i].interactable = true;
        }
    }

    public void StartLevel(int levelId)
    {
        string levelName = "Level" + levelId;
        SceneManager.LoadScene(levelName);
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  // Change "MainMenu" to your main menu scene's exact name
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
