using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDrive : MonoBehaviour
{
    public float maxSpeed = 20f;
    private Vector3 turnSpeed;
    public Rigidbody rb;
    private bool isgrounded;

    public GameObject curBeacon;
    public Transform target;

    private readonly VectorPid angularVelocityController = new VectorPid(33.7766f, 0, 0.2553191f);
    private readonly VectorPid headingController = new VectorPid(9.244681f, 0, 0.06382979f);

    // Start is called before the first frame update
    void Start()
    {
      rb = this.GetComponent<Rigidbody>();
      curBeacon = GameObject.Find("beacon1");
      turnSpeed= new Vector3(0, 5, 0);
    }

    // Update is called once per frame
    void Update()
    {
      target = curBeacon.transform;
    }
    void FixedUpdate()
    {
      if (isgrounded==true)
      {
        var angularVelocityError = rb.angularVelocity * -1;
        Debug.DrawRay(transform.position, rb.angularVelocity * 10, Color.black);

        var angularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.deltaTime);
        Debug.DrawRay(transform.position, angularVelocityCorrection, Color.green);

        rb.AddTorque(angularVelocityCorrection);

        var desiredHeading = target.position - transform.position;
        Debug.DrawRay(transform.position, desiredHeading, Color.magenta);

        var currentHeading = transform.forward;
        Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);

        var headingError = Vector3.Cross(currentHeading, desiredHeading);
        var headingCorrection = headingController.Update(headingError, Time.deltaTime);

        rb.AddTorque(headingCorrection);
      }
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
