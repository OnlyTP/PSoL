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
            Debug.Log(sceneIndex);
            SceneManager.LoadScene(sceneIndex + 1); 
            
        }
    }
}
