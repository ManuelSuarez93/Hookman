using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHealthBar : MonoBehaviour
{
    [Range(0, 1)] [Tooltip("Bar default percent.")]
    public float defaultValue = 1;
    public int health = 1;
    public int maxHealth = 1;
    [Tooltip("The bar image object.")]
    public GameObject image;
    [Tooltip("Bar color above 50%.")]
    public Color fullHealth;
    [Tooltip("Bar color above 25% and below 50%.")]
    public Color halfHealth;
    [Tooltip("Bar color above 0% and below 25%.")]
    public Color quarterHealth;
    [Tooltip("Whether reverse the progress of the bar (0% is 100% and vice versa).")]
    public bool inverted = false;

    private float healthPercent = 100;
    private Image imageComponent;

    private void Awake()
    {
        if (inverted) { healthPercent = 1f - defaultValue; }
        else { healthPercent = defaultValue; }
    }

    private void Start()
    {
        imageComponent = image.GetComponent<Image>();
    }

    public void setHealth(int newHealth)
    {
        health = newHealth;
        if (inverted) { healthPercent = 1f - ((float)health / (float)maxHealth); }
        else { healthPercent = (float)health / (float)maxHealth; }
        ChangeBar();
    }

    public void setMaxHealth(int newMaxHealth)
    {
        maxHealth = newMaxHealth;
        if (inverted) { healthPercent = 1f - ((float)health / (float)maxHealth); }
        else { healthPercent = (float)health / (float)maxHealth; }
        ChangeBar();
    }

    private void ChangeBar()
    {
        image.transform.localScale = new Vector3(healthPercent, 1, 1);
        if (healthPercent > 0.5f)
        {
            imageComponent.color = fullHealth;
        }
        else if (healthPercent > 0.25f)
        {
            imageComponent.color = halfHealth;
        }
        else
        {
            imageComponent.color = quarterHealth;
        }
    }
}
