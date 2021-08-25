using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class HeartUi : MonoBehaviour
{
    public ScriptHealth healthScript;
    public Image img;
    public Sprite[] spritelist;
    public float percent;
    public float divPer;

    // Update is called once per frame
    void Update()
    {
        CheckHealth();
    }

    public void CheckHealth()
    {
        percent = ((float) healthScript.currentHealth / (float) healthScript.maxhealth) * 100;
        divPer = 100 / (spritelist.Count() - 1);

        for (int i = 0; i < spritelist.Count(); i++)
        {
            if (percent >= (divPer * i))
            {
                img.sprite = spritelist[i];
            }
        }
    }
}
