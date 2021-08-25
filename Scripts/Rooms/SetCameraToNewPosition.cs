using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraToNewPosition : MonoBehaviour
{
    public Transform cameraPosition;
    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void setCameraPosition()
    {
        cam.transform.position = new Vector3(cameraPosition.position.x, cameraPosition.position.y, cam.transform.position.z);
    }
}
