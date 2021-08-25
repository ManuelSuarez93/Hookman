using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public int SceneTo;
    public UnityEvent evento;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneTo);
        evento.Invoke();    
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(SceneTo);
        evento.Invoke();
    }
}
