using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyConnected : MonoBehaviour
{
    [Tooltip("Tag of the object against which it is going to collide and take damage while it is connected.")]
    public string collisionTag = "Untagged";
    public UnityEvent collisionWhileConnected;

    private ObjectConnected objectConnected;

    // Start is called before the first frame update
    void Start()
    {
        objectConnected = GetComponent<ObjectConnected>();
        if (!objectConnected)
        {
            gameObject.AddComponent<ObjectConnected>();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(collisionTag) && objectConnected.isConnected())
        {
            collisionWhileConnected.Invoke();
        }
    }
}
