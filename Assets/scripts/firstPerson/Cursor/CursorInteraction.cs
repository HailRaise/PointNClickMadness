using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CursorInteraction : MonoBehaviour
{
    public Sprite defaultCursor;   // Default cursor sprite
    public Sprite dialogueCursor;
    public Sprite pickUpCursor;
    public Sprite doorCursor;      // Cursor sprite when hovering over a door
    private SpriteRenderer cursorRenderer;
    public Sprite lookCursor;
    public Sprite dragCursor;
    public Sprite walkCursor;
    public Sprite goBackCursor;

    [Header("Scene transitions")]
    public string KitchenScene = "Kitchen"; // Scene to load
    public string DoorExitHallway = "ExitHallway";
    public string FP_HouseExit = "FP_HouseExit";
    public string FP_LookWindow = "FP_WindowLook";
    // You can add more scene names later for other doors

    void Start()
    {
        cursorRenderer = GetComponent<SpriteRenderer>();
            // Store the name of the previous scene before loading a new one
        string currentScene = SceneManager.GetActiveScene().name;
        if (PlayerPrefs.HasKey("CurrentScene"))
            PlayerPrefs.SetString("PreviousScene", PlayerPrefs.GetString("CurrentScene"));
        PlayerPrefs.SetString("CurrentScene", currentScene);
    }

    void Update()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
            return;
            

        // --- Hover logic ---
        if (hit.collider != null)
        {
            string name = hit.collider.name.ToLower();
            string tag = hit.collider.tag.ToLower();

            if (name.Contains("door") || tag.Contains("door"))
                cursorRenderer.sprite = walkCursor;
            else if (name.Contains("curtain") || tag.Contains("curtain"))
                cursorRenderer.sprite = dragCursor;
            else if (name.Contains("notebook") || tag.Contains("notebook") || name.Contains("item"))
                cursorRenderer.sprite = pickUpCursor;
            else if (name.Contains("window") || tag.Contains("window"))
                cursorRenderer.sprite = lookCursor;
            else if (name.Contains("oldman") || tag.Contains("npc"))
                cursorRenderer.sprite = dialogueCursor;
            else if (name.Contains("stairs") || tag.Contains("stairs"))
            {
                cursorRenderer.sprite = walkCursor;
            }
            else if (name.Contains("goback") || tag.Contains("goback"))
            {
                cursorRenderer.sprite = goBackCursor;
            }

            else
                cursorRenderer.sprite = defaultCursor;
        }
        else
        {
            cursorRenderer.sprite = defaultCursor;
        }

        // --- Click logic ---
        if (Input.GetMouseButtonDown(0))
            {if (hit.collider == null)
        {
            Debug.Log("No collider hit!");
            return;
        }

            Debug.Log($"Clicked on: | Tag: {hit.collider.tag}");
            if (hit.collider == null)
                return; // <-- prevents null reference

            if (hit.collider.CompareTag("DoorObjectKitchen"))
            {
                Debug.Log("Clicked on the Door! Moving to Kitchen...");
                SceneManager.LoadScene(KitchenScene);
            }
            else if (hit.collider.CompareTag("DoorToExit"))
            {
                SceneManager.LoadScene(DoorExitHallway);
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
            else if (hit.collider.CompareTag("StairsDown"))
            {
                SceneManager.LoadScene(FP_HouseExit);
            }
            else if (hit.collider.CompareTag("npcOldMan"))
            {
                Debug.Log("Clicked OldMan");
                var npc = hit.collider.GetComponent<OldManDialogue>();
                if (npc != null)
                    npc.StartDialogue();
            }
            else if (hit.collider.CompareTag("GoBack"))
            {
                // Go to previous scene
                if (PlayerPrefs.HasKey("PreviousScene"))
                {
                    string prevScene = PlayerPrefs.GetString("PreviousScene");
                    Debug.Log("Going back to " + prevScene);
                    SceneManager.LoadScene(prevScene);
                }
                else
                {
                    Debug.Log("No previous scene found!");
                }
            }
            else if (hit.collider.CompareTag("WindowHouseLook"))
            {
                SceneManager.LoadScene(FP_LookWindow);
            }
        }
    }
}