using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadEndScreen : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // Reset player stats and items before ending the game
            CarryOver.ResetData();

            // Load the end screen scene
            SceneManager.LoadScene("End");
        }
    }
}
