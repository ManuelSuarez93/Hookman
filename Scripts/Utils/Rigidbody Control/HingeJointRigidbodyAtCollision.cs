using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(HingeJoint2D))]
public class HingeJointRigidbodyAtCollision : MonoBehaviour
{
    private HingeJoint2D myJoint;

    private void Awake()
    {
        myJoint = GetComponent<HingeJoint2D>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.otherRigidbody != null)
        {
            myJoint.connectedBody = collision.otherRigidbody;
        }
    }
}
