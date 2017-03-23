using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereControl : MonoBehaviour {
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + PreviewTime.Time, transform.position.z);
        Gizmos.DrawWireSphere(pos, 0.5f);
    }
}
