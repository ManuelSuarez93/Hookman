using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyCollisionPoint : MonoBehaviour
{
    public void CopyPosition(Collision2D collision)
    {
        transform.position = collision.GetContact(0).point;
    }
}
