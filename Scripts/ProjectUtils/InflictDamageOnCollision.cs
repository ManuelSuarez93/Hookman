using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageEvent : UnityEvent<int>
{

}
public class InflictDamageOnCollision : MonoBehaviour
{
    [Tooltip("Damage inflicted.")]
    public int damage = 1;
    [Tooltip("¿This script should respond to a trigger of this object?")]
    public bool isTrigger;
    [Tooltip("Filter the collision by tag.")]
    public bool useTag = true;
    [Tooltip("Tag to use as a filter.")]
    public string validTag = "Player";
    [Tooltip("Whether the target will be repelled using physics or not.")]
    public bool usePhysics = false;
    [Tooltip("Repulsion force used to repell the target if using physics.")]
    public float repulsionForce = 1;
    public DamageEvent damageInflicted;

    private bool canDamage = true;

    private void Awake()
    {
        if (damage > 0)
        {
            damage *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScriptHealth health = collision.collider.GetComponent<ScriptHealth>();
        if (canDamage && !isTrigger && health && !(useTag && !collision.collider.CompareTag(validTag)))
        {
            health.modifyHealth(damage);
            damageInflicted.Invoke(damage);
            if (usePhysics)
            {
                collision.collider.GetComponent<Rigidbody2D>().velocity = (-(transform.position - collision.collider.transform.position)).normalized * repulsionForce;
            }
            else
            {
                collision.collider.transform.position += new Vector3(-collision.GetContact(0).point.x, -collision.GetContact(0).point.y, 0).normalized;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScriptHealth health = collision.gameObject.GetComponent<ScriptHealth>();
        if (canDamage && isTrigger && health && !(useTag && !collision.gameObject.CompareTag(validTag)))
        {
            health.modifyHealth(damage);
            damageInflicted.Invoke(damage);
        }
    }

    public void setActiveDamage(bool can)
    {
        canDamage = can;
    }
}
