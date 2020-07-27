using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    public bool isgrounded;
    public float force = 20f;
    public float torque;
    public float maxSpeed = 40f;
    private float potGain;

    public Rigidbody rb;
    public Vector3 velocity;
    public Vector3 inputDrive;
    public float inputTurn;
    // public GameObject myCam;
    // private Vector3 myCamAngle;
    // private cameraFollow myCamScript;
    // Start is called before the first frame update
    void Start()
    {
      torque = 1;
      rb = this.GetComponent<Rigidbody>();
      // myCam = GameObject.Find("Main Camera");
      // myCamScript = myCam.GetComponent<cameraFollow>();
      // myCamAngle = myCamScript.camAngle;
    }

    // Update is called once per frame
    void Update()
    {
      // myCamAngle = myCamScript.camAngle;
      inputDrive = new Vector3(0f,0f,Input.GetAxis("Vertical"));
      inputTurn = Input.GetAxis("Horizontal");
      potGain = rb.velocity.magnitude + inputDrive.magnitude;
      // input = Vector3.Scale(input, myCamAngle);
    }

    void FixedUpdate()
    {
      if (isgrounded==true)
      {
        rb.AddRelativeTorque(0,torque*inputTurn,0,ForceMode.Impulse);
        if (potGain < maxSpeed)
        {
          moveChar(inputDrive);
        }
      }
    }

    void moveChar(Vector3 direction)
    {
      rb.AddRelativeForce(direction * force);
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
      if (collisionInfo.gameObject.name == "Terrain")
      {
        isgrounded = true;
      }
    }
    void OnCollisionExit(Collision collisionInfo)
    {
      if (collisionInfo.gameObject.name == "floor")
      {
        isgrounded = false;
      }
    }
}
