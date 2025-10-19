using System;
using UnityEngine;

[Serializable]
public class DialogueChoice
{
    public string choiceText;      // what appears on screen
    public int nextNodeIndex;      // which node to go to next
}

[Serializable]
public class DialogueNode
{
    [TextArea(3, 5)]
    public string line;            // NPC dialogue line
    public DialogueChoice[] choices; // available responses
}
