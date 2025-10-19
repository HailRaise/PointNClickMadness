using UnityEngine;
using UnityEngine.SceneManagement; 
public class CursorClickInteraction : MonoBehaviour
{
    public string KitchenScene = "Kitchen";
    public Transform player;  // Reference to the player's GameObject
    public Vector3 kitchenPosition;  // The position of the kitchen in the scene

    void Update()
    {
        RaycastHit2D hit;
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Convert screen position to world position

        // Perform raycast in 2D world space
        hit = Physics2D.Raycast(mousePos, Vector2.zero); // Raycast at the mouse position in the 2D world

        // Detect mouse click
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (hit.collider != null && hit.collider.CompareTag("DoorObjectKitchen"))
            {
                Debug.Log("Clicked on the Door! Moving to Kitchen...");
                // Move the player to the kitchen position (you can set this in the Inspector)
                SceneManager.LoadScene(KitchenScene);
            }
        }
    }
}
