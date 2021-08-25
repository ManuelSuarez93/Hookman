using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Struggle : MonoBehaviour
{
    [Tooltip("Time in seconds needed to disconnect the hook.")]
    public float timeToDisconnect = 3;
    [Tooltip("Force contrary to the hook that this object will perform.")]
    public float struggleForce = 1;
    [Tooltip("Color to which the rope color will transition.")]
    public Color ropeColor = Color.white;
    [Tooltip("The hook object.")]
    public GameObject hook;
    [Tooltip("Line renderer that connect the weapon to the hook.")]
    public LineRenderer line;

    private bool struggle = false;
    private float timer = 0;
    private Color lineStartColor;
    private HingeJoint2D hj;
    private Rigidbody2D hookRb;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            gameObject.AddComponent<Rigidbody2D>();
        }
        
        if (!hook)
        {
            Debug.LogError("Script Struggle error: missing hook reference.");
        }

        if (!line)
        {
            Debug.LogError("Script Struggle error: missing line reference.");
        }
    }

    private void Start()
    {
        hj = hook.GetComponent<HingeJoint2D>();
        if (!hj)
        {
            Debug.LogError("Script Struggle error: missing HingeJoint2D component in hook reference.");
        }

        hookRb = hook.GetComponent<Rigidbody2D>();
        if (!hookRb)
        {
            Debug.LogError("Script Struggle error: missing Rigidbody2D component in hook reference.");
        }

        if (line)
        {
            lineStartColor = line.startColor;
        }
    }

    private void FixedUpdate()
    {
        if (struggle && hook)
        {
            rb.velocity = -(hookRb.velocity.normalized * struggleForce);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (struggle && hook && line)
        {
            timer -= Time.deltaTime;
            Color lerpedColor = Color.Lerp(ropeColor, lineStartColor, timer / timeToDisconnect);
            line.endColor = lerpedColor;
            line.startColor = lerpedColor;
            if (timer <= 0 || hj.connectedBody == null)
            {
                line.endColor = lineStartColor;
                line.startColor = lineStartColor;
                hj.connectedBody = null;
                struggle = false;
                rb.velocity = (hook.transform.position - transform.position).normalized * struggleForce * -1;
            }
        }
    }

    public void StartStruggle()
    {
        struggle = true;
        timer = timeToDisconnect;
    }

    private void OnDestroy()
    {
        if (line)
        {
            line.endColor = lineStartColor;
            line.startColor = lineStartColor;
        }
    }
}
