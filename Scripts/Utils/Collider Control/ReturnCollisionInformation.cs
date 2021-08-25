using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ReturnInformationEvent : UnityEvent<Collision2D>
{ }

public class ReturnCollisionInformation : MonoBehaviour
{
    [Tooltip("Whether if filter the collision by tag.")]
    public bool useTagFilter = false;
    [Tooltip("Tag to use as filter.")]
    public string tagFilter = "Player";
    public ReturnInformationEvent onCollision;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!(useTagFilter && !collision.collider.CompareTag(tagFilter)))
        {
            onCollision.Invoke(collision);
        }
    }
}
