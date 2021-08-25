using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteFunctionOnTrigger : MonoBehaviour
{
    [Tooltip("Whether if filter the collision by tag.")]
    public bool useTagFilter = false;
    [Tooltip("Tag to use as filter.")]
    public string tagFilter = "Player";
    public UnityEvent onTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!(useTagFilter && !collision.CompareTag(tagFilter)))
        {
            onTrigger.Invoke();
        }
    }
}
