using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCoRoutine : MonoBehaviour
{

    public static DialogueCoRoutine dialogueCo;

    public DialogueManager dialogueManager;

    private void Awake()
    {
    }
    public IEnumerator dialogueSent(Dialogue dialogue, float dialogueTime)
    {
        dialogueManager.StartDialogue(dialogue);
        yield return new WaitForSeconds(dialogueTime);
    }
}

