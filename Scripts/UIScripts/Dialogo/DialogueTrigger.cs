using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dialogueManager;
    public UnityEvent enableEvent;
    public UnityEvent disableEvent;
    public float dialogueTime;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StopAllCoroutines();
            StartCoroutine(textTrigger());
        }
        
    }

    IEnumerator textTrigger()
    {
        enableEvent.Invoke();
        
        dialogueManager.StartDialogue(dialogue);
        yield return new WaitForSeconds(dialogueTime);
        disableEvent.Invoke();
    }
}
