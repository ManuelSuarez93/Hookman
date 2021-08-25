using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeEvent : MonoBehaviour
{
    [Tooltip("Time in seconds to execute the event.")]
    public float time = 1;
    [Tooltip("Whether this timer will play on awake.")]
    public bool playOnAwake = true;

    public UnityEvent onTimerOver;

    private bool playingCoroutine = false;
    private bool playing = false;

    // Play the timer on awake
    private void Awake()
    {
        if (playOnAwake)
        {
            playing = true;
            StartCoroutine(PlayCoroutine());
        }
    }

    // Play or continue if the timer isn't over yet
    public void Play()
    {
        playing = true;
        if (!playingCoroutine)
        {
            StartCoroutine(PlayCoroutine());
        }
    }

    // Stop the timer
    public void Stop()
    {
        playing = false;
    }

    // Basically, the timer itself
    private IEnumerator PlayCoroutine()
    {
        float timer = time;
        while (true)
        {
            yield return null;
            if (playing)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    onTimerOver.Invoke();
                    playing = false;
                    StopAllCoroutines();
                }
            }
        }
    }
}
