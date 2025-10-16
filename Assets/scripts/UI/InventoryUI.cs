using UnityEngine;
using TMPro; // if you used TMP

public class InventoryUI : MonoBehaviour
{
    
    public GameObject panel;          // Your background panel
    public TMP_Text itemListText;     // The text object inside the panel
    private PlayerInventory playerInventory;
    

    void Start()
    {
        panel.SetActive(false);
        // Find player in scene (make sure player has the tag "Player")
        var player = GameObject.FindWithTag("Player");
        if (player != null)
            playerInventory = player.GetComponent<PlayerInventory>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("Tab pressed!");
            bool isActive = panel.activeSelf;
            panel.SetActive(!isActive);

            if (!isActive)
                RefreshInventory();
        }
    }

    void RefreshInventory()
    {
        if (playerInventory == null)
            return;

        if (playerInventory.items.Count == 0)
            itemListText.text = "Inventory is empty.";
        else
        {
            itemListText.text = "Inventory:\n";
            foreach (var item in playerInventory.items)
                itemListText.text += "• " + item + "\n";
        }
    }
}
