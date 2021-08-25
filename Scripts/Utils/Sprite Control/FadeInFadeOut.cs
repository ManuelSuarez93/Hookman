using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FadeInFadeOut : MonoBehaviour
{
    [Tooltip("Color to fade the sprite.")]
    public Color fadeTo = Color.white;
    [Tooltip("Duration in seconds of the effect.")]
    public float duration = 0;
    [Tooltip("Delay in seconds after the fade in to fade out.")]
    public float fadeOutDelay = 0;
    public AudioSource hurtSound;

    private SpriteRenderer sprite;
    private Color currentColor;
    private bool coroutineRunning = false;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        hurtSound = GetComponent<AudioSource>();
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
        hurtSound.Play();
        while (currentTime < duration / 2)
        {
            yield return null;
            currentTime += Time.deltaTime;
            sprite.color = Color.Lerp(currentColor, fadeTo, currentTime / (duration / 2));
        }

        yield return new WaitForSeconds(fadeOutDelay);

        while (currentTime > 0)
        {
            yield return null;
            currentTime -= Time.deltaTime;
            sprite.color = Color.Lerp(currentColor, fadeTo, currentTime / (duration / 2));
        }

        coroutineRunning = false;
    }
}
