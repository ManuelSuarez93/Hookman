using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomsController : MonoBehaviour
{
    [Tooltip("Distance added to the teleport of the player between rooms.")]
    public float addedDistance = 0.5f;
    public Transform player;
    public RoomsMessanger referencePoint;
    private Camera cam;
    private float horizontalDistance;
    private float verticalDistance;
    private float xPlayer;
    private float yPlayer;
    private bool searchingNewRoom = true;
    private string direction;

    private void Awake()
    {
        cam = Camera.main;
        verticalDistance = cam.orthographicSize;
        horizontalDistance = cam.orthographicSize * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (referencePoint.transform.position == cam.transform.position && searchingNewRoom == false)
            {
                searchingNewRoom = true;
                teleportPlayer();
            }

            xPlayer = player.position.x;
            yPlayer = player.position.y;
            if (xPlayer >= cam.transform.position.x + horizontalDistance && searchingNewRoom)
            {
                direction = "Right";
                referencePoint.setDirection(direction);
                searchingNewRoom = false;
            }
            else if (xPlayer <= cam.transform.position.x - horizontalDistance && searchingNewRoom)
            {
                direction = "Left";
                referencePoint.setDirection(direction);
                searchingNewRoom = false;
            }
            else if (yPlayer >= cam.transform.position.y + verticalDistance && searchingNewRoom)
            {
                direction = "Up";
                referencePoint.setDirection(direction);
                searchingNewRoom = false;
            }
            else if (yPlayer <= cam.transform.position.y - verticalDistance && searchingNewRoom)
            {
                direction = "Down";
                referencePoint.setDirection(direction);
                searchingNewRoom = false;
            }
        }
        
    }

    public void teleportPlayer()
    {
        switch (direction)
        {
            case "Right":
                player.position = new Vector3(cam.transform.position.x - horizontalDistance + addedDistance, player.transform.position.y, player.transform.position.z);
                break;
            case "Left":
                player.position = new Vector3(cam.transform.position.x + horizontalDistance - addedDistance, player.transform.position.y, player.transform.position.z);
                break;
            case "Up":
                player.position = new Vector3(player.transform.position.x, cam.transform.position.y - verticalDistance + addedDistance, player.transform.position.z);
                break;
            case "Down":
                player.position = new Vector3(player.transform.position.x, cam.transform.position.y + verticalDistance - addedDistance, player.transform.position.z);
                break;
        }
    }
}
