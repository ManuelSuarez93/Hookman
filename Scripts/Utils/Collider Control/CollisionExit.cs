using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionExit : MonoBehaviour
{
    [Tooltip("Whether this script will verify triggers or collisions.")]
    public bool isTrigger = false;
    [Tooltip("Whether if filter the collision by tag.")]
    public bool useFilterTag = false;
    [Tooltip("Tag to use as filter.")]
    public string tagFilter = "Untagged";
    public UnityEvent onExit;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!(useFilterTag && !collision.gameObject.CompareTag(tagFilter)) && isTrigger)
        {
            onExit.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!(useFilterTag && !collision.collider.CompareTag(tagFilter)) && !isTrigger)
        {
            onExit.Invoke();
        }
    }
}
