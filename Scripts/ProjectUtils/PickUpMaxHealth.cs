using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpMaxHealth : MonoBehaviour
{
    public int extraMaxHealth = 1;
    public UnityEvent collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScriptHealth healthscript = collision.gameObject.GetComponent<ScriptHealth>();
        if (collision.gameObject.CompareTag("Player") && healthscript)
        {
            healthscript.ModifyMaxHealth(extraMaxHealth);
            collected.Invoke();
        }
    }
}
