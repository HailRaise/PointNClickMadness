using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorInteraction : MonoBehaviour
{
    public Sprite defaultCursor;   // Default cursor sprite
    public Sprite pickUpCursor;
    public Sprite doorCursor;      // Cursor sprite when hovering over a door
    private SpriteRenderer cursorRenderer;
    public Sprite dragCursor; 

    [Header("Scene transitions")]
    public string KitchenScene = "Kitchen"; // Scene to load
    // You can add more scene names later for other doors

    void Start()
    {
        cursorRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        // --- Hover logic ---
        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("DoorObjectKitchen"))
            {
                cursorRenderer.sprite = doorCursor;
            }
            else if (hit.collider.CompareTag("Curtain"))
            {
                cursorRenderer.sprite = dragCursor; // or special cursor
            }
            else if (hit.collider.CompareTag("Notebook"))
            {
                cursorRenderer.sprite = pickUpCursor;
            }
            else
            {
                cursorRenderer.sprite = defaultCursor;
            }
        }
        else
        {
            cursorRenderer.sprite = defaultCursor;
        }

        // --- Click logic ---
        if (Input.GetMouseButtonDown(0))
        {
            if (hit.collider == null)
                return; // <-- prevents null reference

            if (hit.collider.CompareTag("DoorObjectKitchen"))
            {
                Debug.Log("Clicked on the Door! Moving to Kitchen...");
                SceneManager.LoadScene(KitchenScene);
            }
            else if (hit.collider.CompareTag("Curtain"))
            {
                var curtain = hit.collider.GetComponent<CurtainInteraction>();
                if (curtain != null)
                    curtain.Toggle();
            }
            else if (hit.collider.CompareTag("Notebook"))
            {
                  Debug.Log("NotebookPickedUp");

                // Find player and add the item
                GameObject player = GameObject.FindWithTag("Player");
                if (player != null)
                {
                    var inventory = player.GetComponent<PlayerInventory>();
                    if (inventory != null)
                    {
                        inventory.AddItem("Notebook");
                    }
                }

                Destroy(hit.collider.gameObject);
            }
        }
    }
}