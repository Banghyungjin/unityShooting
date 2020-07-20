using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    public float offsetX = 0f;
    public float offsetY = 7f;
    public float offsetZ = 0f;
    Vector3 cameraPosition;

    void Start()
    {

        cameraPosition.x = 0f;
        cameraPosition.y = 7f;
        cameraPosition.z = 7f;
    }

    void LateUpdate()
    {
        if (player.activeSelf == true)
        {
            cameraPosition.x = player.transform.position.x + offsetX;
            cameraPosition.y = player.transform.position.y + offsetY;
            cameraPosition.z = player.transform.position.z + offsetZ;

            transform.position = cameraPosition;
        }
        

    }
}
