using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HookSampleLevel : MonoBehaviour
{
    public HookController hookController;
    public PlayerSample playerSample;
    public Image image;
    public Text LevelText;
    public Text AmountText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = "Length: " + hookController.maxRange.ToString();
        AmountText.text = playerSample.currentSampleAmount.ToString() + "/" + playerSample.maxSampleAmount.ToString();
        float percent = (float)playerSample.currentSampleAmount / (float)playerSample.maxSampleAmount;
        image.fillAmount = percent;
    }

}
