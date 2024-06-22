using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Tutorial : MonoBehaviour
{
    public GameObject Panel;
    public Button yourButton;

    void Start()
    {
 
        if (yourButton != null)
        {
            yourButton.onClick.AddListener(TogglePanel);
        }
    }

    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePanel();
        }
    }

    public void TogglePanel()
    {
        if (Panel != null)
        {

            Panel.SetActive(!Panel.activeSelf);
        }
    }
}
