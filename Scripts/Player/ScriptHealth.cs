using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class HealthModifiedEvent : UnityEvent<int>
{ }

[System.Serializable]
public class Invulnerability : UnityEvent<float>
{ }

public class ScriptHealth : MonoBehaviour
{

    [Tooltip("Maximum health this object can reach.")]
    public int maxhealth = 3;
    [Tooltip("The current health, obviously.")]
    public int currentHealth;
    public HealthModifiedEvent damaged;
    public HealthModifiedEvent healthRestored;
    public UnityEvent destroyed;
    public HealthModifiedEvent maxHealthLevelUp;
    public Invulnerability onInvulnerable;
    public UnityEvent onVulnerable;

    private bool invulnerable = false;

    public void modifyHealth(int healthModifier)
    {
        if (!(invulnerable && healthModifier < 0))
        {
            currentHealth += healthModifier;

            if (currentHealth > maxhealth)
            {
                currentHealth = maxhealth;
            }
            else if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            if (healthModifier > 0)
            {
                healthRestored.Invoke(currentHealth);
            }
            else if (healthModifier < 0 && currentHealth > 0)
            {
                damaged.Invoke(currentHealth);
            }

            if (currentHealth == 0)
            {
                destroyed.Invoke();
            }
        }
    }

    public void ModifyMaxHealth(int maxHealthModifier)
    {
        maxhealth += maxHealthModifier;
        maxHealthLevelUp.Invoke(maxhealth);
    }

    public void InvulnerabilityActivate(float duration)
    {
        invulnerable = true;
        onInvulnerable.Invoke(duration);
        StartCoroutine(InvulnerabilityTimer(duration));
    }

    private IEnumerator InvulnerabilityTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        invulnerable = false;
        onVulnerable.Invoke();
    }

}
