using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour
{
    public Slider bgslider;
    public Slider msSlider;
    public Slider enSlider;
    public Slider footSlider;
    public AudioManager audioManager;
    

    public void start()
    {
        bgslider.value = audioManager.BackgroundVolume;
        msSlider.value = audioManager.MusicVolume;
        enSlider.value = audioManager.EnemyVolume;
        footSlider.value = audioManager.FootstepVolume;
    }
    
    public void changeBackGround()
    {
        audioManager.switchVolume("Background", bgslider.value);
        PlayerPrefs.SetFloat("BackgroundVolume", bgslider.value);
    }

    public void changeMusic()
    {
        audioManager.switchVolume("Music", msSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", msSlider.value);
    }

    public void changefootstep()
    {
        audioManager.switchVolume("Footstep", footSlider.value);
        PlayerPrefs.SetFloat("FootstepVolume", footSlider.value);

    }

    public void changeEnemy()
    {
        audioManager.switchVolume("Enemy", enSlider.value);
        PlayerPrefs.SetFloat("EnemyVolume", enSlider.value);
    }
}
