using System.Collections.Generic;
using UnityEngine;
[System.Serializable]//可序列化才能在面板上显示
public class CustomState
{
    public string Name = string.Empty;
    public Vector3 Position;
}
public class CustomListInspector : MonoBehaviour {
    [HideInInspector]//隐藏默认的list面板，使用自定义的list面板
    public List<CustomState> States = new List<CustomState>();
	
}
