using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRenderControl : MonoBehaviour
{
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateRenderVisibility();

        // Subscribe to scene changes so it updates automatically
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        UpdateRenderVisibility();
    }

    void UpdateRenderVisibility()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        // Example: only hide player in first-person scenes
        if (sceneName.Contains("HallWay") || sceneName.Contains("FP"))
        {
            sr.enabled = false;  // Hide sprite/model
        }
        else
        {
            sr.enabled = true;   // Show sprite/model
        }
    }
}
