using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableDoor : MonoBehaviour
{
    public string sceneToLoad;

    // Этот метод может называться по-другому, например, OnMouseDown()
    public void LoadNextScene() 
    {
        // 1. СНАЧАЛА ПОЛНОСТЬЮ ОСВОБОЖДАЕМ КУРСОР
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Это самая важная строка!

        // 2. И ТОЛЬКО ПОТОМ ЗАГРУЖАЕМ СЦЕНУ
        SceneManager.LoadScene(sceneToLoad);
    }
}