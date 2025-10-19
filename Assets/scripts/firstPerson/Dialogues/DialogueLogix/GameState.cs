using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;
    private HashSet<string> talkedTo = new HashSet<string>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // stays across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool HasTalkedTo(string id)
    {
        return talkedTo.Contains(id);
    }

    public void MarkTalkedTo(string id)
    {
        talkedTo.Add(id);
        Debug.Log($"[GameState] Talked to {id}");
    }
}
