using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PickUpText : MonoBehaviour
{
    public Dropdown dropdown;
    public string optionname;
    public UnityEvent collected;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            List<string> value = new List<string>();
            value.Add(optionname);
            dropdown.AddOptions(value);
            collected.Invoke();
        }
    }
}
