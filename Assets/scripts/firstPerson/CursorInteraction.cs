using UnityEngine;

public class CursorInteraction : MonoBehaviour
{
    public Sprite defaultCursor;  // Default cursor
    public Sprite doorCursor;     // Cursor to show when hovering over the doorframe

    private SpriteRenderer cursorRenderer;  // Reference to the cursor's SpriteRenderer

    void Start()
    {
        cursorRenderer = GetComponent<SpriteRenderer>();  // Get the reference to the cursor
    }

    void Update()
    {
        RaycastHit2D hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Raycast from the mouse position

        // Perform the raycast in 2D world space
        hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null)
        {
            // Change cursor if the mouse is over the doorframe (tagged as "DoorObject")
            if (hit.collider.CompareTag("DoorObjectKitchen"))
            {
                cursorRenderer.sprite = doorCursor;  // Change the cursor to the "door" cursor
            }
            else
            {
                cursorRenderer.sprite = defaultCursor;  // Default cursor if not hovering over a door
            }
        }
        else
        {
            cursorRenderer.sprite = defaultCursor;  // Default cursor if nothing is hit
        }

        // Handle mouse click
        if (Input.GetMouseButtonDown(0)) // Left click
        {
            if (hit.collider != null && hit.collider.CompareTag("DoorObjectKitchen"))
            {
                Debug.Log("Clicked on the Door!");
                // Here, you can implement what happens when the player clicks on the door
                // For example, transition to another scene or trigger an animation
            }
        }
    }
}
