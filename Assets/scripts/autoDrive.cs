using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDrive : MonoBehaviour
{
    // general rigidbody stuff
    public Rigidbody rb;
    private bool isgrounded;

    // signal-source
    public GameObject curBeacon;
    public Transform target;

    //for driving to a target
    public float toVel = 2.5f;
    public float maxVel = 15.0f;
    public float maxForce = 40.0f;
    public float gain = 5f;

    //for turning to a target
    private readonly VectorPid angularVelocityController = new VectorPid(33.7766f, 0, 0.2553191f);
    private readonly VectorPid headingController = new VectorPid(9.244681f, 0, 0.06382979f);

    // Start is called before the first frame update
    void Start()
    {
      rb = this.GetComponent<Rigidbody>();
      curBeacon = GameObject.Find("beacon1");
    }

    // Update is called once per frame
    void Update()
    {
      target = curBeacon.transform;
    }
    void FixedUpdate()
    {
      if (isgrounded==true && Input.GetButton("Jump"))
      {
        var desiredHeading = target.position - transform.position;
        desiredHeading.y = 0;
        steerToTarget(desiredHeading);
        driveToTarget(desiredHeading);

      }
    }

    void steerToTarget(Vector3 desiredHeading)
    {
      var currentHeading = transform.forward;
      Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);
      Debug.DrawRay(transform.position, desiredHeading, Color.magenta);

      var headingError = Vector3.Cross(currentHeading, desiredHeading);
      var headingCorrection = headingController.Update(headingError, Time.deltaTime);
      rb.AddTorque(headingCorrection);

      var angularVelocityError = rb.angularVelocity * -1;
      var angularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.deltaTime);
      rb.AddTorque(angularVelocityCorrection);
    }
    void driveToTarget(Vector3 desiredHeading)
    {
      // calc a target vel proportional to distance (clamped to maxVel)
      Vector3 tgtVel = Vector3.ClampMagnitude(toVel * desiredHeading, maxVel);
      // calculate the velocity error
      Vector3 error = tgtVel - rb.velocity;
      // calc a force proportional to the error (clamped to maxForce)
      Vector3 force = Vector3.ClampMagnitude(gain * error, maxForce);
      rb.AddForce(force);
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
      if (collisionInfo.gameObject.name == "Terrain")
      {
        isgrounded = false;
      }
    }
}


public class VectorPid
{
    public float pFactor, iFactor, dFactor;

    private Vector3 integral;
    private Vector3 lastError;

    public VectorPid(float pFactor, float iFactor, float dFactor)
    {
        this.pFactor = pFactor;
        this.iFactor = iFactor;
        this.dFactor = dFactor;
    }

    public Vector3 Update(Vector3 currentError, float timeFrame)
    {
        integral += currentError * timeFrame;
        var deriv = (currentError - lastError) / timeFrame;
        lastError = currentError;
        return currentError * pFactor
            + integral * iFactor
            + deriv * dFactor;
    }
}

// PID-Stuff from
// https://answers.unity.com/questions/199055/addtorque-to-rotate-rigidbody-to-look-at-a-point.html
