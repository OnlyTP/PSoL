using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject btn1;
    public GameObject btn2;
    public GameObject btn3;
    public GameObject levelSelectPanel;




    // Function to be called when the "Play" button is pressed
    public void PlayGame()
    {
        CarryOver.ResetData();
        SceneManager.LoadScene("Level1");
    }

   public void OpenLevelSelect()
{
        Debug.Log("Opening Level Select Panel");
        btn1.SetActive(false);
        btn2.SetActive(false);
        btn3.SetActive(false);

        levelSelectPanel.SetActive(true);
        Debug.Log("Level Select Panel activated.");
        
}




    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }

  

}
