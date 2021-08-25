using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Script : MonoBehaviour
{
    ObjectConnected _scriptConnected;
    HingeJoint2D _hinge;
    Rigidbody2D _rigid;
    public float time;
    public float limit;
    public float deacX;
    public float deacY;

    void Start()
    {
        _scriptConnected = GetComponent<ObjectConnected>();
        _hinge = GameObject.Find("Hook").GetComponent<HingeJoint2D>();
        _rigid = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_scriptConnected.isConnected() && _hinge != null)
        {
            time += Time.deltaTime;
            if (time > limit)
            {
                _rigid.bodyType = RigidbodyType2D.Dynamic;
                _rigid.AddForce(new Vector2(-deacX, -deacY), ForceMode2D.Impulse);
                _hinge.connectedBody = null;
                time = 0;
            }

          
        }

    }


}
