using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeartsController : MonoBehaviour
{
    public ScriptHealth enemyHealth;
    public GameObject heart;

    private Animator[] animations;

    private void Awake()
    {
        if (enemyHealth && heart)
        {
            animations = new Animator[enemyHealth.maxhealth];
            for (int i = 0; i < enemyHealth.maxhealth; i++)
            {
                GameObject instance = Instantiate(heart, transform);
                animations[i] = instance.GetComponent<Animator>();
            }

            if (!animations[0])
            {
                Debug.LogError("EnemyHeartsController Script error: the game object in Heart field has not Animator component.");
            }
        }
        else
        {
            Debug.LogError("EnemyHeartsController Script error: the object " + gameObject.name + " has missing components type of Enemy Health or Heart.");
        }
    }

    public void Damaged()
    {
        int i = transform.childCount;
        while (i > enemyHealth.currentHealth)
        {
            animations[i - 1].SetBool("Broken", true);
            i--;
        }
    }
}
