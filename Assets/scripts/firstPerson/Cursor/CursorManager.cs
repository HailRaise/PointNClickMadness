using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    private SpriteRenderer spriteRenderer;
    private bool isFPScene;

    [Header("Cursor Sprites")]
    [SerializeField] private Sprite defaultCursor;
    [SerializeField] private Sprite doorCursor;
    [SerializeField] private Sprite lookCursor;
    [SerializeField] private Sprite pickUpCursor;
    [SerializeField] private Sprite goBackCursor;
    [SerializeField] private Sprite dragCursor;
    [SerializeField] private Sprite walkCursor;
    [SerializeField] private Sprite dialogueCursor;
    

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name.Contains("FP") || newScene.name.Contains("FirstPerson") || newScene.name.Contains("HallWay"))
            EnableCursor(true);
        else
            EnableCursor(false);
    }

    private void EnableCursor(bool enable)
    {
        if (spriteRenderer != null) spriteRenderer.enabled = enable;

        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
            if (script != this) script.enabled = enable;

        isFPScene = enable;
    }

    // #### NEW: public API to set the cursor sprite
    public void SetCursor(string type)
    {
        if (!spriteRenderer) return;
        switch (type.ToLower())
        {
            case "door":       spriteRenderer.sprite = doorCursor; break;
            case "look":       spriteRenderer.sprite = lookCursor; break;
            case "pickup":     spriteRenderer.sprite = pickUpCursor; break;
            case "goback":     spriteRenderer.sprite = goBackCursor; break;
            case "drag":       spriteRenderer.sprite = dragCursor; break;
            case "walk":       spriteRenderer.sprite = walkCursor; break;
            case "dialogue":   spriteRenderer.sprite = dialogueCursor; break;
            default:           spriteRenderer.sprite = defaultCursor; break;
        }
    }
}
