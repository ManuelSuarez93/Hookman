using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;
//
public class AudioManager : MonoBehaviour
{
    public static AudioManager am;
    public AudioSource camAudio;
    public AudioSource playerAudio;
    public GameObject player;

    public List<AudioSource> background = new List<AudioSource>();
    public List<AudioSource> enemy = new List<AudioSource>();

    [Range(0, 1)]
    public float BackgroundVolume;
    [Range(0, 1)]
    public float MusicVolume;
    [Range(0, 1)]
    public float FootstepVolume;
    [Range(0, 1)]
    public float EnemyVolume;

    private void Awake()
    {
        player = GameObject.Find("Player");
        if (player)
        {
            playerAudio = player.GetComponent<AudioSource>();
        }

        loadSettings();
        camAudio = Camera.main.GetComponent<AudioSource>();
        camAudio.volume = MusicVolume;
        playerAudio.volume = FootstepVolume;
        background = setAudioSourceList("AudioBackground");
        enemy = setAudioSourceList("Enemy");
        setVolume(background, BackgroundVolume);
        setVolume(enemy,EnemyVolume);
        

    }
    /*
    public Dictionary<int, AudioClip> loadstuff(List<UnityEngine.Object> files)
    {
        Debug.Log("HERE COMES THE PAIN");
        int j = 1;        
        Dictionary<int, AudioClip> dick = new Dictionary<int, AudioClip>();
        for (int i = 0; i < files.Count; i++)
        {     
            dick.Add(j, Resources.Load<AudioClip>("music/music" + (i+1)));
            Debug.Log(dick[j]);
            j++;
        }

        return dick;
    }*/ 


    public List<AudioSource> setAudioSourceList(string tag)
    {
        GameObject[] list = GameObject.FindGameObjectsWithTag(tag);
        List<AudioSource> source = new List<AudioSource>();
        foreach (GameObject i in list)
        {
        source.Add(i.GetComponent<AudioSource>());
        }
        
        return source;
    }



    public void switchVolume(string volumeName, float volume)
    {
        Debug.Log("Entro a Switch volume");
        switch(volumeName)
        {
            case "Background": setVolume(background, volume); break;
            case "Music": camAudio.volume = volume; break;
            case "Footstep": playerAudio.volume = volume; break;
            case "Enemy": setVolume(enemy, volume); break;
            default: break;
        }
    }

    void setVolume(List<AudioSource> source, float volume)
    {
        foreach(AudioSource i in source)
        {
            if(i != null)
            {
                i.volume = volume;  
            }
            
        }
    }

    public void loadSettings()
    {
        MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
        BackgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume");
        FootstepVolume = PlayerPrefs.GetFloat("FootstepVolume");
        EnemyVolume = PlayerPrefs.GetFloat("EnemyVolume");
    }
    private IEnumerator saveSettings()
    {
        yield return new WaitForSeconds(1);
        PlayerPrefs.SetFloat("MusicVolume",MusicVolume);
        PlayerPrefs.SetFloat("BackgroundVolume",BackgroundVolume);
        PlayerPrefs.SetFloat("FootstepVolume", FootstepVolume);
        PlayerPrefs.SetFloat("EnemyVolume", EnemyVolume);
    }


}
