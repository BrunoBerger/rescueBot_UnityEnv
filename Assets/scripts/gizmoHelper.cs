using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gizmoHelper : MonoBehaviour
{
    // public float gizmoSize = .75f;
    // public Color gizmoColor = Color.yellow;
    //
    // void OnDrawGizmos()
    // {
    //   Gizmos.color = gizmoColor;
    //   Gizmos.DrawWireSphere(transform.position, gizmoSize);
    // }
    void Update()
    {
      var currentHeading = transform.forward;
      Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);
    }
}
