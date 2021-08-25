using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleBar : MonoBehaviour
{
    
    PlayerSample _sample;

    // Start is called before the first frame update
    void Start()
    {
        _sample = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSample>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_sample != null)
        {
            float percent = _sample.currentSampleAmount / (float)_sample.maxSampleAmount;
            transform.localScale = new Vector2(percent, 1);
        }
        
    }
}
