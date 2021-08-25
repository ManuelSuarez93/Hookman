using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMoved : MonoBehaviour
{

    ObjectConnected objectConnected;
    HingeJoint2D _hinge;
    Rigidbody2D _rigid;
    public float speed = 1;
    Rigidbody2D _playerRigid;
    // Start is called before the first frame update
    void Start()
    {
        objectConnected = gameObject.GetComponent<ObjectConnected>();
        _hinge = GameObject.Find("Hook").GetComponent<HingeJoint2D>();
        _rigid = gameObject.GetComponent<Rigidbody2D>();
        _playerRigid = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(objectConnected)
        {
            Move();
        }
        
    }

    
    public void Move()
    {
        Vector2 _vector;
        _vector = _hinge.reactionForce;
        Vector2 _fuerza;
        _fuerza = new Vector2(0, 0);
        if(_playerRigid.velocity.x == 0 || _playerRigid.velocity.y == 0)
        {
            if (_vector.x > 0)
            {
                _fuerza.x = speed;
            }
            else
            {
                _fuerza.x = -speed;
            }

            if (_vector.y > 0)
            {
                _fuerza.y = speed;
            }
            else
            {
                _fuerza.y = -speed;
            }

            _rigid.velocity = _fuerza;
        }
        
        
      
        
    }
}
