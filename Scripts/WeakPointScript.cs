using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeakPointScript : MonoBehaviour
{
    public float weakPointRadius;
    public LayerMask rockLayer;
    public GameObject particleprefab;

    public void collide()
    {
       GameObject instanz = Instantiate(particleprefab);
       instanz.transform.position = transform.position;
    }
}
