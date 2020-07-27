using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
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
      torque = 50;
      rb = this.GetComponent<Rigidbody>();
      // myCam = GameObject.Find("Main Camera");
      // myCamScript = myCam.GetComponent<cameraFollow>();
      // myCamAngle = myCamScript.camAngle;
    }

    // Update is called once per frame
    void Update()
    {
      // myCamAngle = myCamScript.camAngle;
      inputDrive = new Vector3(Input.GetAxis("Vertical"),0f,0f);
      inputTurn = Input.GetAxis("Horizontal");
      potGain = rb.velocity.magnitude + inputDrive.magnitude;
      // input = Vector3.Scale(input, myCamAngle);
    }

    void FixedUpdate()
    {
      // print(potGain);
      // rb.AddRelativeTorque(Vector3.up * torque * inputTurn);
      rb.AddRelativeTorque(0,torque*inputTurn,0,ForceMode.Force);
      if (potGain < maxSpeed)
      {
        moveChar(inputDrive);
      }
    }

    void moveChar(Vector3 direction)
    {
      rb.AddRelativeForce(direction * force);
    }
}
