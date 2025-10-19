using UnityEngine;

public class OldManDialogue : MonoBehaviour
{
    public DialogueNode[] firstDialogue;
    public DialogueNode[] repeatDialogue;
    public AudioClip blipSound;

    private string npcID = "OldMan";

    void Awake()
    {
        // --- First Dialogue ---
        firstDialogue = new DialogueNode[3];
        firstDialogue[0] = new DialogueNode()
        {
            line = "ILYA PIDARAS?",
            choices = new DialogueChoice[]
            {
                new DialogueChoice(){ choiceText = "DA", nextNodeIndex = 1 },
                new DialogueChoice(){ choiceText = "NET.", nextNodeIndex = -1 }
            }
        };

        firstDialogue[1] = new DialogueNode()
        {
            line = "POCHEMMU?",
            choices = new DialogueChoice[]
            {
                new DialogueChoice(){ choiceText = "IDET ON NAHUI", nextNodeIndex = 2 },
                new DialogueChoice(){ choiceText = "Ladno etot", nextNodeIndex = -1 }
            }
        };

        firstDialogue[2] = new DialogueNode()
        {
            line = "There was once a time when this place thrived...",
            choices = new DialogueChoice[]
            {
                new DialogueChoice(){ choiceText = "Continue...", nextNodeIndex = -1 }
            }
        };

        // --- Repeat Dialogue ---
        repeatDialogue = new DialogueNode[1];
        repeatDialogue[0] = new DialogueNode()
        {
            line = "We already spoke, lad. Leave an old man be...",
            choices = new DialogueChoice[]
            {
                new DialogueChoice(){ choiceText = "Okay...", nextNodeIndex = -1 }
            }
        };
    }

    public void StartDialogue()
    {
        // check memory before deciding which dialogue to show
        if (GameState.Instance != null && GameState.Instance.HasTalkedTo(npcID))
        {
            DialogueManager.Instance.StartDialogue(repeatDialogue, blipSound, npcID);
        }
        else
        {
            DialogueManager.Instance.StartDialogue(firstDialogue, blipSound, npcID);

            // mark that the player has now spoken to him
            GameState.Instance.MarkTalkedTo(npcID);
        }
    }
}
