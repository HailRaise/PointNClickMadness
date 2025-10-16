using UnityEngine;

public class PlayerController : MonoBehaviour
{
    void Awake()
    {
        // This ensures the player object is not destroyed when transitioning to a new scene
        DontDestroyOnLoad(gameObject);  
    }
}
