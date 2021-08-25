using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InflictFireState : MonoBehaviour
{
    [Tooltip("Total damage the fire will cause.")]
    public int totalDamage = 1;
    [Tooltip("Duration of the state in seconds.")]
    public int time;

    private void Awake()
    {
        if (totalDamage > 0)
        {
            totalDamage *= -1;
        }
    }
    public void ApplyState(Collider2D collision)
    {
        StatesController sc = collision.gameObject.GetComponent<StatesController>();
        if (sc)
        {
            sc.FireState(totalDamage, time);
        }
    }
}
