using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookForceLevel : MonoBehaviour
{
    public HookController hookController;
    public Text forceLevel;

    // Update is called once per frame
    void Update()
    {
        forceLevel.text = hookController.forceLevel.ToString();
    }
}
