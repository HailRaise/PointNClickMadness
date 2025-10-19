using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitioner : MonoBehaviour
{
    [Header("Transition Settings")]
    [Header("Extra Transition Sounds")]
    public AudioClip walkSound;
    public AudioClip doorOpenSoundHouse;
    public AudioClip windowSound;

    public AudioClip transitionSound;
    private AudioSource src;
    private static bool comingBack = false;

    void Awake()
    {
        // set up an AudioSource automatically
        src = gameObject.AddComponent<AudioSource>();
        src.playOnAwake = false;
    }

    void Start()
    {
    string current = SceneManager.GetActiveScene().name;

    if (!comingBack)
    {
        if (PlayerPrefs.HasKey("CurrentScene"))
            PlayerPrefs.SetString("PreviousScene", PlayerPrefs.GetString("CurrentScene"));

        PlayerPrefs.SetString("CurrentScene", current);
    }

    comingBack = false; // reset for normal travel
}

private string GetReturnScene(string current)
{
    switch (current)
    {
        case "FP_WindowLook":
            return "FP_ExitHallway";
        // add more one-off rules if needed later:
        // case "FP_AtticLook": return "Hallway";
        default:
            return PlayerPrefs.HasKey("PreviousScene")
                ? PlayerPrefs.GetString("PreviousScene")
                : "Hallway";
    }
}

    public void LoadScene(string scene, AudioClip clip = null)
    {
        StartCoroutine(PlayThenLoad(scene, clip));
    }

    private IEnumerator PlayThenLoad(string scene, AudioClip clip)
{
    // pick which sound to use
    AudioClip toPlay = clip != null ? clip : transitionSound;

    // play sound
    if (toPlay != null)
        src.PlayOneShot(toPlay);

    // ✅ wait only briefly so the sound starts, but never hangs the load
    yield return new WaitForSeconds(0.3f);

    SceneManager.LoadScene(scene);
}


public void GoBack()
{
    string current = SceneManager.GetActiveScene().name;
    string target = GetReturnScene(current);

    if (string.IsNullOrEmpty(target))
    {
        Debug.Log("No previous scene found!");
        return;
    }

    comingBack = true;
    LoadScene(target);
}


}
