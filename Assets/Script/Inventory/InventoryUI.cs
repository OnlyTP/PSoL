using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject itemsParent; 
    public GameObject title;       

    private bool isInventoryVisible = false;

    private void Start()
    {
        UpdateInventoryUIVisibility();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventoryUI();
        }
    }

    private void ToggleInventoryUI()
    {
        isInventoryVisible = !isInventoryVisible;
        UpdateInventoryUIVisibility();
    }

    private void UpdateInventoryUIVisibility()
    {
        itemsParent.SetActive(isInventoryVisible);
        title.SetActive(isInventoryVisible);
    }
}
