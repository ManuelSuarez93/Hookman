using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpHealth : MonoBehaviour
{
    public int health = 1;
    public UnityEvent collected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScriptHealth healthscript = collision.gameObject.GetComponent<ScriptHealth>();
        if (collision.gameObject.CompareTag("Player") && healthscript)
        {
            if (healthscript.currentHealth < healthscript.maxhealth)
            {
                healthscript.modifyHealth(health);
                collected.Invoke();
            }
        }
    }
}
