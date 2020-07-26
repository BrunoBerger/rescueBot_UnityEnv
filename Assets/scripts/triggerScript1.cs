using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerScript1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print("Ready to trigger the Player");
    }

    void OnTriggerStay(Collider temp)
    {
      Debug.Log(temp + " is in the Zone");
    }
    void OnTriggerExit(Collider other)
    {
      Debug.Log("Player left the Zone");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
