using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [Tooltip("Velocidad del disparo")]
    public float speed;

    private Transform _player;
    private Vector2 _target;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _target = new Vector2(_player.position.x, _player.position.y);

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, _target, speed*Time.deltaTime);
        transform.right = _player.position.normalized;
        if(new Vector2(transform.position.x, transform.position.y) == _target)
        {
            Destroy(gameObject);
        }
    }
}
