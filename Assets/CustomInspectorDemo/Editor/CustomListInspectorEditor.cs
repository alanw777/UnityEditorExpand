using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(CustomListInspector))]
public class CustomListInspectorEditor : Editor
{
    CustomListInspector _target;
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        _target = (CustomListInspector) target;
        DrawCustomListInspector();
    }

    private void DrawCustomListInspector()
    {
        GUILayout.Space(5);
        GUILayout.Label("States", EditorStyles.boldLabel);
        for (int i = 0; i < _target.States.Count; ++i)
        {
            DrawState(i);
        }

        DrawAddStateButton();
    }

    private void DrawAddStateButton()
    {
        if (GUILayout.Button("Add new state"))
        {
            _target.States.Add(new CustomState());
        }
    }

    private void DrawState(int index)
    {
        if (index < 0 || index >= _target.States.Count)
        {
            return;
        }
        SerializedProperty listIterator = serializedObject.FindProperty("States");
        GUILayout.BeginHorizontal();
        {
            GUILayout.Label("Name", EditorStyles.label, GUILayout.Width(50));
            EditorGUI.BeginChangeCheck();
            string newName = GUILayout.TextField(_target.States[index].Name, GUILayout.Width(120));
            Vector3 newPosition = EditorGUILayout.Vector3Field("", _target.States[index].Position);
            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_target, "Modify State");
                _target.States[index].Name = newName;
                _target.States[index].Position = newPosition;
                EditorUtility.SetDirty(_target);
            }

            if (GUILayout.Button("Remove"))
            {
                EditorApplication.Beep();
                if (EditorUtility.DisplayDialog("Really?", "Do you really want to remove the state '" + _target.States[index].Name + "'?", "Yes", "No"))
                {
                    Undo.RecordObject(_target, "Delete State");
                    _target.States.RemoveAt(index);
                    EditorUtility.SetDirty(_target);
                }
            }
        }
        GUILayout.EndHorizontal();
    }

}
