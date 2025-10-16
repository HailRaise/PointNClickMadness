using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickableDoor : MonoBehaviour
{
    public string sceneToLoad;

    // Этот метод мы вызовем по нажатию кнопки
    public void LoadNextScene()
    {
        // Прячем курсор перед переходом в 2D сцену
        Cursor.visible = false; 
        SceneManager.LoadScene(sceneToLoad);
    }
}