using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpHookLevelUp : MonoBehaviour
{
    public UnityEvent collected;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            HookController hc = collision.gameObject.GetComponentInChildren<HookController>();
            if (hc)
            {
                hc.ModifyForceLevel(1);
                collected.Invoke();
            }
        }
    }
}
