using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRenderControl : MonoBehaviour
{
    private SpriteRenderer sr;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void OnEnable()
    {
        // Subscribe when enabled
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe when disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        UpdateRenderVisibility(); // also run at startup
    }

    // Called after a scene is fully loaded and ready
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        UpdateRenderVisibility();
    }

    void UpdateRenderVisibility()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Hide player sprite in first-person scenes
        if (sceneName.Contains("HallWay") || sceneName.Contains("FP"))
            sr.enabled = false;
        else
            sr.enabled = true;
    }
}
