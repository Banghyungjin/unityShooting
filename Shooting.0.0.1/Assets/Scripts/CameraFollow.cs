using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offsetX = 0f;
    public float offsetY = 10f;
    public float offsetZ = 0f;
    Vector3 cameraPosition;

    void Start()
    {
        cameraPosition.x = 100f;
        cameraPosition.y = 10f;
        cameraPosition.z = 10f;
    }

    void LateUpdate()
    {
        cameraPosition.x = player.transform.position.x + offsetX;
        cameraPosition.y = player.transform.position.y + offsetY;
        cameraPosition.z = player.transform.position.z + offsetZ;

        transform.position = cameraPosition;

    }
}
