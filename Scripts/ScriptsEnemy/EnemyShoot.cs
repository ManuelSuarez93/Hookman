using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyShoot : MonoBehaviour
{
    [Tooltip("Object to shoot.")]
    public GameObject projectile;
    
    [Tooltip("Shoot speed.")]
    public float speed;
    [Tooltip("Distancia en que empieza ejecutar el disparo")]
    public float shootDistance;
    public float startTimeShots;
    public float actualDistance;

    //Gameobject del player
    private GameObject _player;
    //Ultimo disparo y tiempo etnre disparos
    private float _lastShot;
    private float _timebetweenShots;
    private ObjectConnected _objectConnected;
    private AudioSource _sound;
    public Animator animatorController;
    void Start()
    {
        _objectConnected = GetComponent<ObjectConnected>();
        _player = GameObject.FindGameObjectWithTag("Player");
        _sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    void Shoot()
    {
        if (!_objectConnected.isConnected() && _player != null && _player.activeInHierarchy)
        {
            if (Vector2.Distance(transform.position, _player.transform.position) < shootDistance)
            {
                if (_timebetweenShots <= 0)
                {
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    if(!_sound.isPlaying)
                    {
                        _sound.Play();
                    }
                    else
                    {
                        _sound.Stop();
                        _sound.Play();
                    }
                    animatorController.SetTrigger("Attack");
                    _timebetweenShots = startTimeShots;
                }
                else
                {
                    _timebetweenShots -= Time.deltaTime;
                }
                transform.right = transform.position - _player.transform.position;
            }
        }
    }
}
