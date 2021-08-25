using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExecuteEvent : MonoBehaviour
{
    public UnityEvent[] onExecute;

    private bool canExecute = true;

    public void Execute(int eventNumber)
    {
        if (canExecute)
        {
            onExecute[eventNumber].Invoke();
        }
    }

    public void CanExecuteEvent(bool canExecute)
    {
        this.canExecute = canExecute;
    }
}
