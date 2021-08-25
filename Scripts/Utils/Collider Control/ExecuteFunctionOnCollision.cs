using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteFunctionOnCollision : MonoBehaviour
{
    [Tooltip("Whether if filter the collision by tag.")]
    public bool useTagFilter = false;
    [Tooltip("Tag to use as filter.")]
    public string tagFilter = "Untagged";
    public UnityEvent onCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(useTagFilter && !collision.collider.CompareTag(tagFilter)))
        {
            onCollision.Invoke();
        }
    }
}
