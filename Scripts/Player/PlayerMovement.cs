using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("Speed movement of the player in units per second.")]
    public float speed = 2;
    [Tooltip("Input from Input Manager that represents the vertical movement.")]
    public string verticalInput = "Vertical";
    [Tooltip("Input from Input Manager that represents the horizontal movement.")]
    public string horizontalInput = "Horizontal";
    public AudioSource audio;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        Movement();
    }

    // Update is called once per frame
    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            if(!audio.isPlaying)
            {
              audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Movement()
    {

        float horizontalMovement = Input.GetAxis(horizontalInput);
        float verticalMovement = Input.GetAxis(verticalInput);
        horizontalMovement *= speed;
        verticalMovement *= speed;
        Vector3 movement = Vector3.zero;
        movement += Vector3.right * horizontalMovement;
        movement += Vector3.up * verticalMovement;
        if (movement.magnitude > speed)
        {
            movement = movement.normalized * speed;
        }
        rb.velocity = movement;
       
    }
}
