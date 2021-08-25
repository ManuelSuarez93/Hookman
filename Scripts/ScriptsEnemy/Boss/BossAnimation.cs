using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimation : MonoBehaviour
{
    public Animator bossAnimator;

    public void SetSpinningState(bool state)
    {
        if (bossAnimator)
        {
            bossAnimator.SetBool("Spinning", state);
        }
        else
        {
            Debug.LogError("BossAnimation Script error: Boss Animator missing reference.");
        }
    }

    public void ShootToxicGas()
    {
        if (bossAnimator)
        {
            bossAnimator.SetTrigger("Gas Attack");
        }
        else
        {
            Debug.LogError("BossAnimation Script error: Boss Animator missing reference.");
        }
    }

    public void SetIsDead(bool isDead)
    {
        bossAnimator.SetBool("isDead", true);
    }
}
