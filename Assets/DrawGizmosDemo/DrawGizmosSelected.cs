using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmosSelected : MonoBehaviour
{
    public Color gizmosColor;
    public Vector3 wireCubeSize;
  
    //选中当前object时显示gizmos
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position, wireCubeSize);
        Gizmos.DrawSphere(transform.position, wireCubeSize.x / 2);
    }


}

    