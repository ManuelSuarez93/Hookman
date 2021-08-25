using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public ScriptHealth _healthScript;
    public Image img;
    public Sprite sprite;
    public float percent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((_healthScript != null))
        {
            percent = _healthScript.currentHealth / (float)_healthScript.maxhealth;
            img.fillAmount = percent;
        }
        
    }
}

