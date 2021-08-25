using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [Tooltip("Speed of this object in units per second.")]
    public float speed = 1;
    [Tooltip("Distance between the object and the current destination point at which the object will change its destination point.")]
    public float minDistanceToPoint;
    [Tooltip("Object with the points that this object will follow.")]
    public GameObject pointGroupObject;

    private enum direction {Going, ComingBack};
    private direction currentDirection = direction.Going;
    private int destinationPoint = 0;
    private Transform[] patrolPoints;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        patrolPoints = new Transform[pointGroupObject.transform.childCount];
        for (int i = 0; i < pointGroupObject.transform.childCount; i++)
        {
            patrolPoints[i] = pointGroupObject.transform.GetChild(i);
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = (patrolPoints[destinationPoint].position - transform.position).normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, patrolPoints[destinationPoint].position) < minDistanceToPoint)
        {
            transform.position = patrolPoints[destinationPoint].position;

            if (patrolPoints[destinationPoint] == patrolPoints[patrolPoints.Length - 1])
            {
                currentDirection = direction.ComingBack;
            }
            else if (patrolPoints[destinationPoint] == patrolPoints[0])
            {
                currentDirection = direction.Going;
            }

            destinationPoint = (currentDirection == direction.Going) ? destinationPoint += 1 : destinationPoint -= 1;
        }
    }
}
