using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawn : MonoBehaviour
{
    [Tooltip("Prefab de enemigo a spawnear")]
    public GameObject enemy;
    [Tooltip("Rate de spawneo")]
    public float rate;
    [Tooltip("Cantidad de veces que se invocaría")]
    public int repeatSpawn;
   

    private float _lastTime;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < repeatSpawn; i++)
        {
            if (Time.time - _lastTime > rate)
            {
                Invoke("SpawnEnemy", rate);
            }
            else
            {
                CancelInvoke("SpawnEnemy");
            }
        }

        
    }

    private void Update()
    {
      
        
    }
    // Update is called once per frame
    private void SpawnEnemy()
    {
        if (Time.time - _lastTime > rate)
        {
            GameObject _enemy = Instantiate(enemy);
            _enemy.transform.position = transform.position;
            _lastTime = Time.time;
        }
       
    }
}
