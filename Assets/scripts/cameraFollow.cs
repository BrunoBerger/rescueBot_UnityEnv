using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject target;
    public float turnSpeed = 4.0f;

    public Vector3 camAngle;

    private Vector3 cubePos;
    private Quaternion cubeRot;


    // Start is called before the first frame update
    void Start()
    {
      // target = GameObject.Find("Player");
      camAngle.Set(cubePos.x, cubePos.y+4, cubePos.z-16);
    }

    // Update is called once per frame
    void Update()
    {
      cubePos = target.transform.position;
      camAngle = Quaternion.AngleAxis (Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * camAngle;

      transform.position = cubePos + camAngle;
      transform.LookAt(cubePos);
    }

}
