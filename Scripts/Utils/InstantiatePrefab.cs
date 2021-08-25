using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiatePrefab : MonoBehaviour
{
    [Tooltip("This array will contain the prefabs that can be instantiated.")]
    public GameObject[] prefab = new GameObject[1];

    public void Instantiate(int prefabNum)
    {
        Instantiate(prefab[prefabNum], transform.position, Quaternion.Euler(0, 0, 0));
    }
}
