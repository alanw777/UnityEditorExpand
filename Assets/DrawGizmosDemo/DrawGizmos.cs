﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawGizmos : MonoBehaviour
{
    public Color gizmosColor;
    public Vector3 wireCubeSize;
    
    //在game模式点击Gizmos也可以看到
    public void OnDrawGizmos()
    {
        Gizmos.color = gizmosColor;
        Gizmos.DrawWireCube(transform.position, wireCubeSize);
        Gizmos.DrawSphere(transform.position, wireCubeSize.x/2);
    }


}

    