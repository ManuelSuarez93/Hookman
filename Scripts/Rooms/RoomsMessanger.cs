using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RoomsMessanger : MonoBehaviour
{
    public float speed = 1;
    private Camera cam;
    private string direction;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        switch (direction)
        {
            case "Right":
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                break;
            case "Left":
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                break;
            case "Up":
                transform.position = new Vector3(transform.position.x, transform.position.y + speed, transform.position.z);
                break;
            case "Down":
                transform.position = new Vector3(transform.position.x, transform.position.y - speed, transform.position.z);
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Room"))
        {
            collision.GetComponent<SetCameraToNewPosition>().setCameraPosition();
            transform.position = cam.transform.position;
            direction = "None";
        }
    }

    public void setDirection(string newDirection)
    {
        direction = newDirection;
    }
}
