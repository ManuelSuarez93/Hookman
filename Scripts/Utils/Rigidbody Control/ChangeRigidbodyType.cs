using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ChangeRigidbodyType : MonoBehaviour
{
    [Tooltip("Mass of the rigidbody if it's Dynamic.")]
    public int mass = 1;
    [Tooltip("Linear drag of the rigidbody if it's Dynamic.")]
    public int linearDrag = 1;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void ChangeType(string newType)
    {
        switch (newType)
        {
            case "Dynamic":
                rb.bodyType = RigidbodyType2D.Dynamic;
                rb.mass = mass;
                rb.drag = linearDrag;
                break;
            case "Kinematic":
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case "Static":
                rb.bodyType = RigidbodyType2D.Static;
                break;
        }
    }
}
