using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextSample : MonoBehaviour
{

    PlayerSample _sample;
    Text texto;

    void Start()
    {
        texto = GetComponent<Text>();
        _sample = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerSample>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_sample != null)
        {
            texto.text = _sample.currentSampleAmount.ToString();
        }
        
    }
}
