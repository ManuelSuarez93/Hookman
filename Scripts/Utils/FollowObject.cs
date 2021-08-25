using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform objectToFollow;
    private float xDistance;
    private float yDistance;
    // Start is called before the first frame update
    void Start()
    {
        xDistance = objectToFollow.position.x - transform.position.x;
        yDistance = objectToFollow.position.y - transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(objectToFollow.position.x - xDistance, objectToFollow.position.y - yDistance, transform.position.z);
    }
}
