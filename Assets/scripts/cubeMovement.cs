using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeMovement : MonoBehaviour
{
    public float force = 20f;
    public float maxSpeed = 40f;
    private float potGain;

    public Rigidbody rb;
    public GameObject myCam;
    private Vector3 myCamAngle;
    private cameraFollow myCamScript;
    public Vector3 input;
    public Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
      rb = this.GetComponent<Rigidbody>();
      myCam = GameObject.Find("Main Camera");
      myCamScript = myCam.GetComponent<cameraFollow>();
      myCamAngle = myCamScript.camAngle;
    }

    // Update is called once per frame
    void Update()
    {
      myCamAngle = myCamScript.camAngle;
      input = new Vector3(Input.GetAxis("Horizontal"),0f,
                          Input.GetAxis("Vertical"));
      input = Vector3.Scale(input, myCamAngle);
      potGain = rb.velocity.magnitude + input.magnitude;
    }

    void FixedUpdate()
    {
      // print(potGain);
      if (potGain < maxSpeed)
      {
        moveChar(input);
      }
    }

    void moveChar(Vector3 direction)
    {
      rb.AddForce(direction * force);
    }
}
