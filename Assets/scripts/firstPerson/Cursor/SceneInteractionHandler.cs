using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneInteractionHandler : MonoBehaviour
{
    private CursorManager cursor;
    private SceneTransitioner transitioner;

    void Start()
    {
        cursor = FindObjectOfType<CursorManager>();
        transitioner = FindObjectOfType<SceneTransitioner>();
    }

    void Update()
    {
         if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
        {
        cursor.SetCursor("default");
        return;
        }
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider == null)
        {
            cursor.SetCursor("default");
            return;
        }

        string name = hit.collider.name.ToLower();
        string tag = hit.collider.tag.ToLower();

        // --- hover logic ---
        if (name.Contains("door")) cursor.SetCursor("door");
        else if (name.Contains("window")) cursor.SetCursor("look");
        else if (name.Contains("notebook")) cursor.SetCursor("pickup");
        else if (name.Contains("goback")) cursor.SetCursor("goback");
        else if (name.Contains("stairs")) cursor.SetCursor("walk");
        else if (name.Contains("npc")) cursor.SetCursor("dialogue");
        else cursor.SetCursor("default");

        // --- click logic ---
        if (Input.GetMouseButtonDown(0))
            {
            if (name.Contains("kitchen"))
                transitioner.LoadScene("Kitchen", transitioner.walkSound);
            else if (name.Contains("window"))
                transitioner.LoadScene("FP_WindowLook", transitioner.windowSound);
            else if (name.Contains("doortoexit"))
                transitioner.LoadScene("FP_ExitHallway");

            else if (name.Contains("stairsdown"))
                transitioner.LoadScene("FP_HouseExit", transitioner.walkSound);

            else if (name.Contains("goback"))
                transitioner.GoBack();

            else if (name.Contains("oldman") || tag.Contains("npc"))
            {
                var npc = hit.collider.GetComponent<OldManDialogue>();
                if (npc != null) npc.StartDialogue();
            }
        }

        
    }
}
