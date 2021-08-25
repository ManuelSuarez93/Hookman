using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHealth : MonoBehaviour
{
     ScriptHealth healthScript;
     Text texto;

    void Start()
    {
        healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<ScriptHealth>();
        texto = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((healthScript != null))
        {
            texto.text = healthScript.currentHealth.ToString();
        }
        
    }
}
