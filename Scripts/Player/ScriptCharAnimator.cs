using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCharAnimator : MonoBehaviour
{
    private float speedX;
    private Animator anim;
    private Rigidbody2D rigid;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponentInParent<Rigidbody2D>();

        if (!anim) { Debug.LogError("ScriptCharAnimatios script error: there aren't an Animator component in " + gameObject.name); }

        if (!rigid) { Debug.LogError("ScriptCharAnimatios script error: there aren't an Rigidbody2D component in " + gameObject.name); }
    }

    // Update is called once per frame
    void Update()
    {
        if (rigid && anim) 
        { 
            speedX = Math.Abs(rigid.velocity.x) + Math.Abs(rigid.velocity.y);
            anim.SetFloat("SpeedX", speedX);
        }
    }
}
