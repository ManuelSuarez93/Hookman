using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownBar : MonoBehaviour
{
    public HookController hookController;
    Image imagine;
    public float percent;

    // Start is called before the first frame update
    void Start()
    {
        imagine = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((hookController != null))
        {
            float percent = (hookController.CooldownTime - hookController.TimeBetweenCooldown);
            imagine.fillAmount = percent;
            if(percent == hookController.CooldownTime)
            {
                imagine.color = Color.green;
            }
            else
            {
                imagine.color = Color.yellow;
            }
            
        }
    }
}
