using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FollowObject1 : MonoBehaviour
{
    [Tooltip("Object transform to follow.")]
    public Transform follow;
    [Tooltip("Speed in units per second of this object.")]
    public float speed = 1;
    [Tooltip("Maximum distance in units between this object and the object to follow, at which this object will follow the another object.")]
    public float perception = 1;
    [Tooltip("Minimum distance that this object will maintain between itself and the object to follow.")]
    public float personalSpace = 0;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            Vector2 direction = follow.position - transform.position;
            if (direction.magnitude <= perception && direction.magnitude > personalSpace)
            {
                rb.velocity = direction.normalized * speed;
            }
        }
    }
}
