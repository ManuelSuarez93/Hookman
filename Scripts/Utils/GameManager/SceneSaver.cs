using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSaver : MonoBehaviour
{
    [Tooltip("Whether save the current scene on awake.")]
    public bool saveOnAwake = false;
    [Tooltip("Default scene to load if there isn't one already saved.")]
    public int defaultScene = 0;

    private void Awake()
    {
        if (saveOnAwake)
        {
            Save();
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Scene Save", SceneManager.GetActiveScene().buildIndex);
    }

    public void Load()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Scene Save", defaultScene));
    }
}
