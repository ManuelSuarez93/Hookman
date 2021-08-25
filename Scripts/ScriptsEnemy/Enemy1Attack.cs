using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy1Attack : MonoBehaviour
{
    [Tooltip("El animator del Enemigo para asi usar el trigger de ataque")]
    public Animator animatorController;
    [Tooltip("El punto centro del ataque")]
    public GameObject attackPoint;
    [Tooltip("Layermask para el player")]
    public LayerMask playerLayer;
    [Tooltip("Radio de circulo de ataque")]
    public float attackRadius;
    [Tooltip("Tiempo de entre ataques")]
    public float attackTime;

    public UnityEvent attackEvent;

    //Tiempo real entre ataques
    private float timeBetweenAttack;
    private bool canAttack = false;

     void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (timeBetweenAttack <= 0 && canAttack)
        {
            timeBetweenAttack = attackTime;
            //RaycastHit2D attackCircle = Physics2D.CircleCast(attackPoint.transform.position, attackRadius, attackPoint.transform.position);
            //if (attackCircle.collider.CompareTag("Player"))
            //{
                animatorController.SetTrigger("Attack");
            //}
        }
        else if (timeBetweenAttack > 0)
        {
           timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void AttackTrigger(bool newCanAttack)
    {
        canAttack = newCanAttack;
    }

    public void PlayAttackEvent()
    {
        attackEvent.Invoke();
    }
}
