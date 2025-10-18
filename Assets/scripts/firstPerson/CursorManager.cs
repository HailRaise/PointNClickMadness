using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    private static CursorManager instance;
    private SpriteRenderer spriteRenderer;
    private bool isFPScene;

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

        // Subscribe to scene change event
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // check if the new scene is a FP one
        if (newScene.name.Contains("FP") || newScene.name.Contains("FirstPerson") || newScene.name.Contains("HallWay"))
        {
            EnableCursor(true);
        }
        else
        {
            EnableCursor(false);
        }
    }

    private void EnableCursor(bool enable)
    {
        if (spriteRenderer != null)
            spriteRenderer.enabled = enable;

        // Enable/disable your logic scripts too
        foreach (MonoBehaviour script in GetComponents<MonoBehaviour>())
        {
            if (script != this)
                script.enabled = enable;
        }

        isFPScene = enable;
    }
}
