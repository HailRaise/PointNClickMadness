using UnityEngine;

public class OldManDialogue : MonoBehaviour
{
    public DialogueNode[] dialogueNodes;
    public AudioClip blipSound;

    void Awake()
    {
        
        dialogueNodes = new DialogueNode[3];

        // Node 0
        dialogueNodes[0] = new DialogueNode();
        dialogueNodes[0].line = "ILYA PIDARAS?";
        dialogueNodes[0].choices = new DialogueChoice[]
        {
            new DialogueChoice(){ choiceText = "DA", nextNodeIndex = 1 },
            new DialogueChoice(){ choiceText = "NET.", nextNodeIndex = -1 }
        };

        // Node 1
        dialogueNodes[1] = new DialogueNode();
        dialogueNodes[1].line = "POCHEMMU?";
        dialogueNodes[1].choices = new DialogueChoice[]
        {
            new DialogueChoice(){ choiceText = "IDET ON NAHUI", nextNodeIndex = 2 },
            new DialogueChoice(){ choiceText = "Ladno etot", nextNodeIndex = -1 }
        };

        // Node 2
        dialogueNodes[2] = new DialogueNode();
        dialogueNodes[2].line = "There was once a time when this place thrived...";
        dialogueNodes[2].choices = new DialogueChoice[]
        {
            new DialogueChoice(){ choiceText = "Continue...", nextNodeIndex = -1 }
        };
    }

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogueNodes, blipSound);
    }
}
