// using UnityEngine;

// public class ScenePersist : MonoBehaviour
// {
//     // 1. Static reference to hold the one and only instance of this object
//     private static ScenePersist instance;

//     void Awake()
//     {
//         // 2. Check if the instance already exists
//         if (instance != null)
//         {
//             // If an instance already exists, this is a duplicate. Destroy it.
//             Destroy(gameObject);
//         }
//         else
//         {
//             // If no instance exists, this is the original. Set the static reference 
//             // to this object and ensure it persists across scenes.
//             instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//     }
// }