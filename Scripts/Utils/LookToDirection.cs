using System;
using System.Collections;
using UnityEngine;

public class LookToDirection : MonoBehaviour
{
    public enum objectDirection {right, left, up, down};
    [Tooltip("Object direction that will look to the mouse or an object.")]
    public objectDirection objectAxisDirection = objectDirection.right;
    [Tooltip("¿This object should look to mouse instead of an object?")]
    public bool lookToMouse = true;
    [Tooltip("The object transform this object will look at. Can be None if Look To Mouse is True.")]
    public Transform objectToLook;

    private Camera cam;
    private Vector3 direction = Vector3.zero;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale > 0)
        {
            // The direction vector is defined here
            if (lookToMouse)
            {
                Vector3 mousePosition = Input.mousePosition;
                mousePosition = cam.ScreenToWorldPoint(mousePosition);
                direction = new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, 0);
            }
            else if (objectToLook)
            {
                direction = (objectToLook.position - transform.position).normalized;
            }

            // And then, the direction vector it's applied to the axis choosed
            switch (objectAxisDirection)
            {
                case objectDirection.right:
                    transform.right = direction;
                    break;
                case objectDirection.left:
                    transform.right = direction * -1;
                    break;
                case objectDirection.up:
                    transform.up = direction;
                    break;
                case objectDirection.down:
                    transform.up = direction * -1;
                    break;
            }
        }
    }

    public void ChangeTarget()
    {
        lookToMouse = !lookToMouse;
    }

    public void setObjectToLook(Transform newObject)
    {
        objectToLook = newObject;
    }

    public void setObjectAxisDirection(string newDirection)
    {
        switch (newDirection)
        {
            case "Right":
                objectAxisDirection = objectDirection.right;
                break;
            case "Left":
                objectAxisDirection = objectDirection.left;
                break;
            case "Up":
                objectAxisDirection = objectDirection.up;
                break;
            case "Down":
                objectAxisDirection = objectDirection.down;
                break;
        }
    }
}
