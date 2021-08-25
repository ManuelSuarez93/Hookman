using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HookLengthLevel : MonoBehaviour
{
    public HookController hookController;
    public PlayerSample playerSample;
    public Image image;
    public Text LevelText;
    public Text AmountText;
    public float percent;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LevelText.text = "Force: " + hookController.maxRange.ToString();
        AmountText.text = playerSample.currentSampleAmount.ToString() + "/" + playerSample.maxSampleAmount.ToString();
        percent = (float)playerSample.currentSampleAmount / (float)playerSample.maxSampleAmount;
        image.fillAmount = percent;
    }
}
