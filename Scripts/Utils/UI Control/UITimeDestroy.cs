using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITimeDestroy : MonoBehaviour
{
    [Tooltip("Time in seconds to destroy the object.")]
    public float time;
    [Tooltip("¿This object will fade over time?")]
    public bool useFadeEffect = false;
    [Tooltip("Sprite Renderer used by Fade Effect. If useFadeEffect is False, this field can be None.")]
    public RawImage sprite;
    public UnityEvent onDestroy;

    private float timeLeft;

    private void Awake()
    {
        timeLeft = time;
        Invoke("SelfDestruct", time);
    }

    // Update is called once per frame
    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (useFadeEffect && sprite)
        {
            sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, timeLeft / time);
        }
    }

    void SelfDestruct()
    {
        onDestroy.Invoke();
        Destroy(gameObject);
    }
}
