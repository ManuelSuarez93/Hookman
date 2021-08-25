using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PullDoor : MonoBehaviour
{
    public float returnSpeed;
    public UnityEvent connectEvent;
    public UnityEvent disconnectEvent;
    public float maxDistance;

    Rigidbody2D _buttonrigid;
    Vector2 _buttonPosition;
    ObjectConnected objectConnected;
    void Start()
    {
        objectConnected = GetComponent<ObjectConnected>();
        _buttonrigid = GetComponent<Rigidbody2D>();
        _buttonPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectConnected.isConnected())
        {
            connectEvent.Invoke();
        }
        else
        {
            disconnectEvent.Invoke();
            _buttonrigid.MovePosition(Vector2.Lerp(_buttonrigid.transform.position, _buttonPosition, returnSpeed / Time.fixedUnscaledDeltaTime));
        }
        /*
        if(Vector2.Distance(transform.position, _buttonPosition)  > maxDistance || Vector2.Distance(transform.position, _buttonPosition) < -maxDistance)
        {
            transform.position = new Vector2(_buttonPosition.x + maxDistance, _buttonPosition.y + maxDistance);
        }*/
    }
}

