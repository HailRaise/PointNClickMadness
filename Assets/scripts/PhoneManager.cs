using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject smsAppScreen;

    // Этот метод можно вызвать в начале игры, если нужно
    void Start()
    {
        // Показываем домашний экран и прячем экран смс
        GoToHomeScreen();
    }

    // Метод для открытия приложения СМС
    public void OpenSmsApp()
    {
        homeScreen.SetActive(false);
        smsAppScreen.SetActive(true);
    }

    // Метод для возврата на домашний экран
    public void GoToHomeScreen()
    {
        homeScreen.SetActive(true);
        smsAppScreen.SetActive(false);
    }
}