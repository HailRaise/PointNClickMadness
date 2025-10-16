using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawnManager : MonoBehaviour
{
    void OnEnable()
    {
        // Run this whenever a scene is loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Find a spawn point by tag or name
        GameObject spawnPoint = GameObject.FindWithTag("PlayerSpawn");
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.transform.position;
            transform.rotation = spawnPoint.transform.rotation;
            Debug.Log($"Player spawned at {scene.name} spawn point.");
        }
        else
        {
            Debug.LogWarning($"No spawn point found in {scene.name}!");
        }
    }
}
