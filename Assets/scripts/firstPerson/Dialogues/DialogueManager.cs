using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    [HideInInspector]
    public bool isDialogueActive = false;

    public static DialogueManager Instance;

    [Header("UI References")]
    [Header("Audio")]
    public AudioSource blipSound;
    public int lettersPerBlip = 2; // play every 2 letters for pacing

    public GameObject dialogueBox;
    public TMP_Text dialogueText;
    public Transform choicesParent;
    public GameObject choiceButtonPrefab;

    [Header("Typing Settings")]
    public float typingSpeed = 0.03f;

    private DialogueNode[] currentDialogue;
    private int currentNodeIndex = 0;
    private Coroutine typingCoroutine;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogueBox.SetActive(false);
    }

    public void StartDialogue(DialogueNode[] dialogue, AudioClip npcBlip)
    {
       dialogueBox.SetActive(true);
        currentDialogue = dialogue;
        currentNodeIndex = 0;

        // assign NPC-specific sound
        if (blipSound != null)
            blipSound.clip = npcBlip;
        Debug.Log("Blip sound set to: " + (npcBlip != null ? npcBlip.name : "NULL"));

        isDialogueActive = true;
        DisplayNode();
    }

    private void DisplayNode()
    {
        DialogueNode node = currentDialogue[currentNodeIndex];

        // Clear old choice buttons
        foreach (Transform child in choicesParent)
            Destroy(child.gameObject);

        // Type text
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeLine(node.line));

        // Create choice buttons
        foreach (DialogueChoice choice in node.choices)
        {
            GameObject btn = Instantiate(choiceButtonPrefab, choicesParent);
            btn.GetComponentInChildren<TMP_Text>().text = choice.choiceText;
            btn.GetComponent<Button>().onClick.AddListener(() =>
            {
                currentNodeIndex = choice.nextNodeIndex;
                if (currentNodeIndex >= 0 && currentNodeIndex < currentDialogue.Length)
                    DisplayNode();
                else
                    EndDialogue();
            });
        }
    }

    private IEnumerator TypeLine(string line)
    {
     dialogueText.text = "";
    int letterCount = 0;

    foreach (char c in line.ToCharArray())
    {
    dialogueText.text += c;
    letterCount++;
    Debug.Log("Typed: " + c + "  Count: " + letterCount);

    if (blipSound != null)
    {
        Debug.Log("Playing blip...");
        blipSound.volume = 0.5f;
        blipSound.pitch = Random.Range(0.7f, 1.0f);
        blipSound.PlayOneShot(blipSound.clip);
    }

    yield return new WaitForSeconds(typingSpeed);
}

}


    private void EndDialogue()
    {
        dialogueBox.SetActive(false);
        currentDialogue = null;
        isDialogueActive = false;
    }
}
