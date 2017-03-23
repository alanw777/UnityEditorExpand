using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
    [Space(10)]
    public float MaximumHeight;
    public float MinimumHeight;

    [Header("Safe Frame")]
    [Range(0f, 1f)]
    public float SafeFrameTop;
    [Range(0f, 1f)]
    public float SafeFrameBottom;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
