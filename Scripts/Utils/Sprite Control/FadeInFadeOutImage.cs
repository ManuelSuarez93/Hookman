using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInFadeOutImage : MonoBehaviour
{
    [Tooltip("Color to fade the image.")]
    public Color fadeTo = Color.white;
    [Tooltip("Fade in duration in seconds.")]
    public float fadeInDuration = 1;
    [Tooltip("Fade out duration in seconds.")]
    public float fadeOutDuration = 1;
    [Tooltip("Delay in seconds after the fade in to fade out.")]
    public float fadeOutDelay = 0;

    private Image sprite;
    private Color currentColor;
    private bool coroutineRunning = false;

    private void Awake()
    {
        sprite = GetComponent<Image>();
        fadeInDuration = Mathf.Abs(fadeInDuration);
        fadeOutDuration = Mathf.Abs(fadeOutDuration);
    }

    public void Play()
    {
        // Play or restart the effect if it was running 
        if (coroutineRunning)
        {
            sprite.color = currentColor;
            StopAllCoroutines();
        }
        currentColor = sprite.color;
        StartCoroutine(Fade(currentColor));
    }

    public void Cancel()
    {
        if (coroutineRunning)
        {
            sprite.color = currentColor;
            coroutineRunning = false;
            StopAllCoroutines();
        }
    }

    public IEnumerator Fade(Color currentColor)
    {
        coroutineRunning = true;
        float currentTime = 0;

        if (fadeInDuration > 0)
        {
            while (currentTime <= fadeInDuration)
            {
                currentTime += Time.deltaTime;
                sprite.color = Color.Lerp(currentColor, fadeTo, currentTime / fadeInDuration);
                yield return null;
            }

        }
        else
        {
            sprite.color = fadeTo;
            yield return null;
        }

        yield return new WaitForSeconds(fadeOutDelay);
        currentTime = 0;

        if (fadeOutDuration > 0)
        {
            while (currentTime <= fadeOutDuration)
            {
                currentTime += Time.deltaTime;
                sprite.color = Color.Lerp(fadeTo, currentColor, currentTime / fadeOutDuration);
                yield return null;
            }
        }
        else
        {
            sprite.color = currentColor;
        }

        coroutineRunning = false;
    }
}
