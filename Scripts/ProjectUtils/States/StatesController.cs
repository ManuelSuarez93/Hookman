using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class intEvent : UnityEvent<int>
{ }

public class StatesController : MonoBehaviour
{
    public intEvent damageFromFire;

    private int totalFireDamage = 0;
    private float totalFireTime = 0;
    private int fireDamage = -1;
    private float fireTime = 0;

    public void FireState(int damage, int time)
    {
        totalFireDamage += damage;
        totalFireTime += time;
        fireTime = totalFireTime / -totalFireDamage;
        CancelInvoke("InflictFireDamage");
        InvokeRepeating("InflictFireDamage", fireTime, fireTime);
    }

    private void InflictFireDamage()
    {
        if (totalFireDamage < 0)
        {
            damageFromFire.Invoke(fireDamage);
            totalFireDamage -= fireDamage;
            totalFireTime -= fireTime;
        }
        else
        {
            CancelInvoke("InflictFireDamage");
        }
    }
}
