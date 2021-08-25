using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button startGame;
    public Scene scene;

    public void StartScene()
    {
        SceneManager.LoadScene(1);
    }

   
}
