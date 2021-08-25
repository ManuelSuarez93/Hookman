using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.Events;

public class PickUpMuestra : MonoBehaviour
{
    public UnityEvent collected;
    public EventListener listener;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerSample sample = collision.gameObject.GetComponent<PlayerSample>();
            if (sample)
            {
                sample.currentSampleAmount += 1;
                collected.Invoke();
            }
        }

    }
}
