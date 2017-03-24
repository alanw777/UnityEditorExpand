using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawValidArea : MonoBehaviour
{
    public float Height;
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y + Height/2, transform.position.z), new Vector3(10f, Height, 10f));
    }
}
