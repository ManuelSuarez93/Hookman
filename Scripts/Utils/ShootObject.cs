using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObject : MonoBehaviour
{
    public enum direction { up, down, left, right };
    [Tooltip("Direction axis of the Transform of this object to use as shoot direction.")]
    public direction axisDirection = direction.up;
    [Tooltip("Speed of the object shooted in units per second.")]
    public float speed;
    [Tooltip("Whether the UP axis of the object shooted will look to his direction.")]
    public bool lookDirection = false;
    [Tooltip("Time between shoots used in ShootRepeated.")]
    public float timeBetweenShoots = 1;

    public void Shoot(GameObject prefab)
    {
        GameObject instance = Instantiate(prefab, transform.position, Quaternion.Euler(Vector3.zero));
        Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();

        if (!rb)
        {
            instance.AddComponent<Rigidbody2D>();
        }

        switch (axisDirection)
        {
            case direction.down:
                rb.velocity = -transform.up * speed;
                break;
            case direction.left:
                rb.velocity = -transform.right * speed;
                break;
            case direction.right:
                rb.velocity = transform.right * speed;
                break;
            case direction.up:
                rb.velocity = transform.up * speed;
                break;
        }

        if (lookDirection)
        {
            instance.transform.up = rb.velocity.normalized;
        }
    }

    public void ShootRepeated(GameObject prefab)
    {
        StartCoroutine(SR(prefab));
    }

    public void StopShooting()
    {
        StopAllCoroutines();
    }

    private IEnumerator SR(GameObject prefab)
    {
        while (true)
        {
            Shoot(prefab);
            yield return new WaitForSeconds(timeBetweenShoots);
        }
    }
}
