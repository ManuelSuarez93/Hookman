using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownScript : MonoBehaviour
{
    public float timescalee;

    void Start()
    {
        
    }


    void Update()
    {
        timescalee = Time.timeScale;
        if(Input.GetButtonUp("Submit"))
        {
           
            if(Time.timeScale == 0.5f)
            {
                Time.timeScale = 1;
                Debug.Log("Time normal");
            }
            else
            {
                Time.timeScale = 0.5f;
                Debug.Log("Time slowed");
            }

        }
    }
}
