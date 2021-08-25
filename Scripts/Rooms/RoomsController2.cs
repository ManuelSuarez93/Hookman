using Cinemachine;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoomsController2 : MonoBehaviour
{
    [Tooltip("Distance added to the teleport of the player between rooms.")]
    public float addedDistance = 0.5f;
    [Tooltip("Maximum distance in units that the current room will travel in search of a new room.")]
    public float maxRoomDistance = 50;
    [Tooltip("Rooms layer that will be used to identify rooms.")]
    public string layerRoom = "Room";
    [Tooltip("Start room where the player start the game.")]
    public GameObject startRoom;
    [Tooltip("Transform of the object Player to controll.")]
    public Transform player;
    [Tooltip("¿This object should control a virtual camera?")]
    public bool useCVC = false;
    [Tooltip("The virtual camera that contains a CinemachineConfiner. If Use CVC is False, this field can be Null.")]
    public CinemachineConfiner virtualCamera;
    [Tooltip("Whether to disable the children objects of the rooms to optimize the resources use.")]
    public bool optimize = true;
    public AudioManager audioM;

    public UnityEvent onRoomChange;

    private LayerMask layer;
    private Camera cam;
    private float horizontalDistance;
    private float verticalDistance;
    private float xPlayer;
    private float yPlayer;
    private string direction;
    private bool searchRoom = true;
    private Transform lastRoom;



    private void Awake()
    {
        audioM = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        cam = Camera.main;
        layer = LayerMask.GetMask(layerRoom);
        startRoom.layer = LayerMask.NameToLayer("Ignore Raycast");
    }

    private void Start()
    {
        if (optimize)
        {
            List<Transform> myRooms = new List<Transform>();
            for (int i = 0; i < transform.childCount; i++)
            {
                myRooms.Add(transform.GetChild(i));
            }

            foreach (Transform room in myRooms)
            {
                if (room != startRoom.transform)
                {
                    List<Transform> myElements = new List<Transform>();
                    for (int i2 = 0; i2 < room.childCount; i2++)
                    {
                        myElements.Add(room.GetChild(i2));
                    }

                    foreach (Transform element in myElements)
                    {
                        element.gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null && searchRoom)
        {
            verticalDistance = cam.orthographicSize;
            horizontalDistance = cam.orthographicSize * cam.aspect;
            xPlayer = player.position.x;
            yPlayer = player.position.y;

            if (xPlayer >= cam.transform.position.x + horizontalDistance)
            {
                direction = "Right";
                searchNewRoom(cam.transform.right);
            }
            else if (xPlayer <= cam.transform.position.x - horizontalDistance)
            {
                direction = "Left";
                searchNewRoom(-cam.transform.right);
            }
            else if (yPlayer >= cam.transform.position.y + verticalDistance)
            {
                direction = "Up";
                searchNewRoom(cam.transform.up);
            }
            else if (yPlayer <= cam.transform.position.y - verticalDistance)
            {
                direction = "Down";
                searchNewRoom(-cam.transform.up);
            }
        }
    }

    public void searchNewRoom(Vector2 searchDirection)
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, searchDirection, maxRoomDistance, layer);
        if (hit.collider != null)
        {
            startRoom.layer = LayerMask.NameToLayer("Room");
            if (optimize)
            {
                lastRoom = startRoom.transform;
            }
            startRoom = hit.collider.gameObject;
            startRoom.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (useCVC)
            {
                if (virtualCamera)
                {
                    virtualCamera.m_BoundingShape2D = hit.collider;
                    StartCoroutine(TPInTheNextFrame());
                }
                else
                {
                    Debug.LogError("VirtualCamera of RoomsController2 is not setted.");
                }
            }
            else
            {
                SetCameraToNewPosition setCamera = hit.collider.GetComponent<SetCameraToNewPosition>();
                if (!setCamera)
                {
                    setCamera = hit.collider.gameObject.AddComponent<SetCameraToNewPosition>();
                    setCamera.cameraPosition = hit.collider.transform;
                }
                setCamera.setCameraPosition();
                teleportPlayer();
            }

            if (optimize)
            {
                for (int i = 0; i < startRoom.transform.childCount; i++)
                {
                    startRoom.transform.GetChild(i).gameObject.SetActive(true);
                }

                for (int i = 0; i < lastRoom.childCount; i++)
                {
                    lastRoom.GetChild(i).gameObject.SetActive(false);
                }
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

        onRoomChange.Invoke();
    }

    public IEnumerator TPInTheNextFrame()
    {
        searchRoom = false;
        yield return null;
        teleportPlayer();
        searchRoom = true;
    }

    public void ChangeRoom()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.position, Vector2.down, maxRoomDistance, layer);
        RaycastHit2D hit2 = Physics2D.Raycast(player.position, -Vector2.down, maxRoomDistance, layer);
        if (hit.collider != null && hit2.collider != null && hit.collider == hit2.collider)
        {
            startRoom.layer = LayerMask.NameToLayer("Room");
            if (optimize)
            {
                lastRoom = startRoom.transform;
            }
            startRoom = hit.collider.gameObject;
            startRoom.layer = LayerMask.NameToLayer("Ignore Raycast");
            if (useCVC)
            {
                if (virtualCamera)
                {
                    virtualCamera.m_BoundingShape2D = hit.collider;
                }
                else
                {
                    Debug.LogError("VirtualCamera of RoomsController2 is not setted.");
                }
            }

            if (optimize)
            {
                for (int i = 0; i < startRoom.transform.childCount; i++)
                {
                    startRoom.transform.GetChild(i).gameObject.SetActive(true);
                }

                for (int i = 0; i < lastRoom.childCount; i++)
                {
                    lastRoom.GetChild(i).gameObject.SetActive(false);
                }
            }

        }

        StartCoroutine(WaitFramesToSearch(10));
    }

    public IEnumerator WaitFramesToSearch(int framesCount)
    {
        searchRoom = false;
        for (int i = 0; i < framesCount; i++)
        {
            yield return null;
        }
        searchRoom = true;
    }
}
   
