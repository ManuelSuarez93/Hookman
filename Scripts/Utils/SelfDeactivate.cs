﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDeactivate : MonoBehaviour
{
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
