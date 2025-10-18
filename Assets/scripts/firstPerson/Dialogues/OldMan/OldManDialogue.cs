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
        dialogueNodes[0].line = "Hello there, traveler...";
        dialogueNodes[0].choices = new DialogueChoice[]
        {
            new DialogueChoice(){ choiceText = "Who are you?", nextNodeIndex = 1 },
            new DialogueChoice(){ choiceText = "Goodbye.", nextNodeIndex = -1 }
        };

        // Node 1
        dialogueNodes[1] = new DialogueNode();
        dialogueNodes[1].line = "Just an old man watching over these ruins.";
        dialogueNodes[1].choices = new DialogueChoice[]
        {
            new DialogueChoice(){ choiceText = "Tell me more.", nextNodeIndex = 2 },
            new DialogueChoice(){ choiceText = "I must go.", nextNodeIndex = -1 }
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
