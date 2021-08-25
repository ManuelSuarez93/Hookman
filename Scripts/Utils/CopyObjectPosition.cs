using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyObjectPosition : MonoBehaviour
{
    [Tooltip("Object transform position to copy.")]
    public Transform anotherObject;
    [Tooltip("¿This object will copy the X position?")]
    public bool copyX = true;
    [Tooltip("¿This object will copy the Y position?")]
    public bool copyY = true;
    [Tooltip("¿This object will copy the Z position?")]
    public bool copyZ = true;

    // Update is called once per frame
    void Update()
    {
        CopyPosition();
    }

    public void CopyPosition()
    {
        if (anotherObject != null)
        {
            Vector3 newPosition = transform.position;
            if (copyX)
            {
                newPosition.x = anotherObject.position.x;
            }

            if (copyY)
            {
                newPosition.y = anotherObject.position.y;
            }

            if (copyZ)
            {
                newPosition.z = anotherObject.position.z;
            }

            transform.position = newPosition;
        }
    }
}
