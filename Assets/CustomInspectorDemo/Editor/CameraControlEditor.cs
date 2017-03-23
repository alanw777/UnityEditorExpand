using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraControl))]
public class CameraControlEditor : Editor
{
    private CameraControl _target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _target = (CameraControl) target;

        DrawCameraHeightPreviewSlider();
    }

    private void DrawCameraHeightPreviewSlider()
    {
        GUILayout.Space(10);
        Vector3 cameraPosition = _target.transform.position;
        cameraPosition.y = EditorGUILayout.Slider("Camera Height", cameraPosition.y, _target.MinimumHeight,
            _target.MaximumHeight);

        if (cameraPosition.y != _target.transform.position.y)
        {
            Undo.RecordObject(_target, "Change Camera Height");
            _target.transform.position = cameraPosition;
        }
    }
}
