using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class EnemyFollow : MonoBehaviour
{
    [Tooltip("Velocidad del enemigo al moverse")]
    public float speed;
    [Tooltip("Distancia en donde empieza a perseguir al jugador")]
    public float followDistance;
    [Tooltip("Distancia minima que mantiene entre el y el jugador")]
    public float MinDistance;

   //Transofrm del objetivo(jugador), Rigidbody para mover el enemigo y script apra ver si esta conectado
    Transform _playerPosition;
    Rigidbody2D _rigid;
    private ObjectConnected _objectConnected;

    void Start()
    {
        //Inicialización de variables
        _rigid = GetComponent<Rigidbody2D>();
        _playerPosition = GameObject.FindGameObjectWithTag("Player").transform;
        _objectConnected = gameObject.GetComponent<ObjectConnected>();
    }

    void Update()
    {
        //Si el objeto no esta conectado
        if(!_objectConnected.isConnected() && _playerPosition != null)
        {
            if (Vector2.Distance(transform.position,_playerPosition.position) <followDistance && Vector2.Distance(transform.position, _playerPosition.position) > MinDistance)
            {
                Vector2 screenPoint = Camera.main.WorldToViewportPoint(transform.position);
                bool onScreen = screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                if (_playerPosition != null)
                {
                    if (onScreen)
                    {
                        _rigid.MovePosition(transform.position + ((Vector3)_playerPosition.position - transform.position).normalized * speed * Time.fixedDeltaTime);
                        transform.right = -new Vector2(transform.position.x - _playerPosition.position.x, transform.position.y - _playerPosition.position.y);
                    }
                }
            }
        }
    }
}
