using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBlink : MonoBehaviour
{
    [Tooltip("")]
    public Color firstColor = Color.white;
    [Tooltip("")]
    public Color secondColor = Color.white;
    [Tooltip("")]
    public float firstDuration = 0.5f;
    [Tooltip("")]
    public float secondDuration = 0.5f;

    private float defaultEffectDuration = 999;
    private Color originalColor;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        if (!sprite)
        {
            gameObject.AddComponent<SpriteRenderer>();
            sprite = GetComponent<SpriteRenderer>();
        }
        originalColor = sprite.color;
    }

    public void Stop()
    {
        StopAllCoroutines();
        sprite.color = originalColor;
    }

    public void Play()
    {
        Play(defaultEffectDuration);
    }

    public void Play(float duration)
    {
        if (sprite)
        {
            StartCoroutine(PlayEffect(duration));
        }
        else
        {
            Debug.LogError("ColorBlink Script error: sprite variable is NULL but is trying to access it.");
        }
    }

    private IEnumerator PlayEffect(float duration)
    {
        float timer = duration;
        float firstTimer = firstDuration;
        float secondTimer = secondDuration;
        yield return null;

        while (timer > 0)
        {
            if (firstTimer > 0)
            {
                firstTimer -= Time.deltaTime;
                if (sprite.color != firstColor) { sprite.color = firstColor; }
            }
            else
            {
                secondTimer -= Time.deltaTime;
                if (sprite.color != secondColor) { sprite.color = secondColor; }
                if (secondTimer <= 0)
                {
                    firstTimer = firstDuration;
                    secondTimer = secondDuration;
                }
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        sprite.color = originalColor;
    }
}
