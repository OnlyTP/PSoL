using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            int sceneIndex = SceneManager.GetActiveScene().buildIndex;
            Debug.Log("Current level index: " + sceneIndex);
            int nextSceneIndex = sceneIndex + 1;

            // Check if the next level index exceeds the stored 'UnlockedLevel'
            int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
            if (nextSceneIndex > unlockedLevel)
            {
                PlayerPrefs.SetInt("UnlockedLevel", nextSceneIndex);
                PlayerPrefs.Save();  // Make sure to save PlayerPrefs changes
                Debug.Log("New level unlocked: " + nextSceneIndex);
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
    }

}
