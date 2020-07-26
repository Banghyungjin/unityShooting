using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CapsuleCollider))]
public class PlayerCamera : MonoBehaviour
{

    private CapsuleCollider bounder;
    private List<GameObject> listPrevObstacleObject = new List<GameObject>();
    public GameObject player;
    public float offsetX = 0f;
    public float offsetY = 7f;
    public float offsetZ = 0f;
    Vector3 cameraPosition;

    // Use this for initialization
    void Start()
    {
        bounder = GetComponent<CapsuleCollider>();
        cameraPosition.x = 0f;
        cameraPosition.y = 7f;
        cameraPosition.z = 7f;
    }
    // Update is called once per frame
    void Update()
    {
        
        Vector3 pointCenter = transform.TransformPoint(bounder.center);
        Vector3 pointLeft = transform.TransformPoint(bounder.center) - new Vector3(bounder.radius, 0, 0);
        Vector3 pointRight = transform.TransformPoint(bounder.center) + new Vector3(bounder.radius, 0, 0);
        Vector3 pointUp = transform.TransformPoint(bounder.center) + new Vector3(0, bounder.height / 2.0f, 0);
        Vector3 pointDown = transform.TransformPoint(bounder.center) - new Vector3(0, bounder.height / 2.0f, 0);
        
        List<Ray> listRay = new List<Ray>();
        Vector3 targetPosition = Camera.main.transform.position;    // camera world position

        listRay.Add(new Ray(pointCenter, targetPosition - pointCenter));
        listRay.Add(new Ray(pointLeft, targetPosition - pointLeft));
        listRay.Add(new Ray(pointRight, targetPosition - pointRight));
        listRay.Add(new Ray(pointUp, targetPosition - pointUp));
        listRay.Add(new Ray(pointDown, targetPosition - pointDown));

        List<RaycastHit[]> listHitInfo = new List<RaycastHit[]>();

        //
        foreach (Ray ray in listRay)
        {
            RaycastHit[] hitInfo = Physics.RaycastAll(ray, 1000.0f);
            listHitInfo.Add(hitInfo);

            Debug.DrawRay(ray.origin, ray.direction * 500, Color.red);
        }

        //
        RaycastHit[] listHit = listHitInfo[0];

        List<GameObject> listNewObstacleObject = new List<GameObject>();

        foreach (RaycastHit hitInfo in listHit)
        {
            if (gameObject.name == hitInfo.collider.name)
            {
                continue;
            }
            if (hitInfo.collider.name.Contains("Bip"))
            {
                continue;
            }

            if (FindColliderByName(listHitInfo[1], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[2], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[3], hitInfo.collider.name)
                && FindColliderByName(listHitInfo[4], hitInfo.collider.name)
                )
            {
                listNewObstacleObject.Add(hitInfo.transform.gameObject);
            }
        }

        // new
        foreach (GameObject obstacleObject in listNewObstacleObject)
        {
            // add
            if (!listPrevObstacleObject.Find(delegate (GameObject inObject) { return (inObject.name == obstacleObject.name); }))
            {
                // changed to transparent
                string nameShader = "Transparent/VertexLit";

                MeshRenderer renderer = obstacleObject.GetComponent<MeshRenderer>();
                renderer.material.shader = Shader.Find(nameShader);
                if (renderer.material.HasProperty("_Color"))
                {
                    Color prevColor = renderer.material.GetColor("_Color");
                    renderer.material.SetColor("_Color", new Color(prevColor.r, prevColor.g, prevColor.b, 0.5f));
                }

            }
        }
        // prev
        foreach (GameObject obstacleObject in listPrevObstacleObject)
        {
            // remove
            if (!listNewObstacleObject.Find(delegate (GameObject inObject) { return (inObject.name == obstacleObject.name); }))
            {
                // changed to opaque
                string nameShader = "Mobile/Unlit (Supports Lightmap)";
                MeshRenderer renderer = obstacleObject.GetComponent<MeshRenderer>();
                renderer.material.shader = Shader.Find(nameShader);

            }
        }
        // swap
        listPrevObstacleObject = listNewObstacleObject;
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

    private bool FindColliderByName(RaycastHit[] inListRayCastInfo, string inName)
    {
        foreach (RaycastHit hitInfo in inListRayCastInfo)
        {

            if (hitInfo.collider.name == inName)
            { return true; }
        }

        return false;
    }
}