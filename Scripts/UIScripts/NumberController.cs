using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberController : MonoBehaviour
{
    public int startNumber = 1;
    public Text textComponent;

    private void Start()
    {
        textComponent.text = startNumber.ToString();
    }

    public void ModifyNumber(int modifier)
    {
        startNumber += modifier;
        textComponent.text = startNumber.ToString();
    }

    public void setNumber(int newNumber)
    {
        startNumber = newNumber;
        textComponent.text = startNumber.ToString();
    }
}
