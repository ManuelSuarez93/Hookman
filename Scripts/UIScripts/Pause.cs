using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject player;
    public DialogueManager dialogueManager;
    public UnityEvent reviveEvent;
    public UnityEvent pauseEvent;
    public UnityEvent unPauseEvent;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            pauseMenu();
        }
    }
    public void pauseMenu()
    {
        if(mainMenu != null)
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
                pauseEvent.Invoke();
            }
            else
            {
                Time.timeScale = 1f;
                unPauseEvent.Invoke();
            }
        }
       
    }

    public void Revive()
    {
        if(!player.activeInHierarchy)
        {
            reviveEvent.Invoke();
        }

    }

    public void backMainMenu()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void startGame()
    {
       SceneManager.LoadScene(2);
    }

    public void SelectLevel(int scene)
    {
        SceneManager.LoadScene(scene);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
