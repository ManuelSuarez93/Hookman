using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSample : MonoBehaviour
{
    [Tooltip("Sample amount needed to pass to the next level.")]
    public int maxSampleAmount;
    [Tooltip("Current sample amount the player has.")]
    public int currentSampleAmount;
    public UnityEvent levelUp;

    private int level;

    private void Awake()
    {
        currentSampleAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSampleAmount >= maxSampleAmount)
        {
            level += 1;
            currentSampleAmount = 0;
            levelUp.Invoke();
        }
    }
}
