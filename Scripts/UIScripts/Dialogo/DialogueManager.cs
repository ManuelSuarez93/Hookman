using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Text dialogueText;
    public Image img;
    public float dialogueSpeed;
    public float time;
    void Start()
    {
        gameObject.SetActive(false);
        sentences = new Queue<string>(); 
    }

    public void StartDialogue(Dialogue dialogue)
    {    
        
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence, dialogue.portrait));
        }
        

    }

    IEnumerator TypeSentence(string sentence, Sprite sprite)
    {
        img.sprite = sprite;
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueSpeed);
        }
    }
}
