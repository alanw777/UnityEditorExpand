using UnityEditor;
using UnityEngine;
//在鼠标位置，如果是有效范围则化预览框，点击菜单可以在该位置添加或者删除方块，按住shift可以多选。
[InitializeOnLoad]
public class LevelEditor : Editor {
    public static Vector3 CurrentHandlePosition = Vector3.zero;
    public static bool IsMouseInValidArea = false;

    static Vector3 _OldHandlePosition = Vector3.zero;

    static LevelEditor()
    {
        // OnSceneGUI委托在Scene视图每次被重绘时被调用
        // 这允许我们可以在Scene视图绘制自定义的GUI元素
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        if (IsInCorrectLevel() == false)
        {
            return;
        }
        // 更新Handle的位置
        UpdateHandlePosition();
        // 检查鼠标所在的位置是否有效
        UpdateIsMouseInValidArea(sceneView.position);
        // 检测是否需要重新绘制Handle
        UpdateRepaint();

        DrawCubeDrawPreview();
    }

    private static void DrawCubeDrawPreview()
    {
        if (IsMouseInValidArea == false)
        {
            return;
        }
        Handles.color = Color.yellow;
        DrawHandlesCube(CurrentHandlePosition);
    }

    private static void DrawHandlesCube(Vector3 center)
    {
        Vector3 p1 = center + Vector3.up * 0.5f + Vector3.right * 0.5f + Vector3.forward * 0.5f;
        Vector3 p2 = center + Vector3.up * 0.5f + Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p3 = center + Vector3.up * 0.5f - Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p4 = center + Vector3.up * 0.5f - Vector3.right * 0.5f + Vector3.forward * 0.5f;

        Vector3 p5 = center - Vector3.up * 0.5f + Vector3.right * 0.5f + Vector3.forward * 0.5f;
        Vector3 p6 = center - Vector3.up * 0.5f + Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p7 = center - Vector3.up * 0.5f - Vector3.right * 0.5f - Vector3.forward * 0.5f;
        Vector3 p8 = center - Vector3.up * 0.5f - Vector3.right * 0.5f + Vector3.forward * 0.5f;

        Handles.DrawLine(p1, p2);
        Handles.DrawLine(p2, p3);
        Handles.DrawLine(p3, p4);
        Handles.DrawLine(p4, p1);

        Handles.DrawLine(p5, p6);
        Handles.DrawLine(p6, p7);
        Handles.DrawLine(p7, p8);
        Handles.DrawLine(p8, p5);

        Handles.DrawLine(p1, p5);
        Handles.DrawLine(p2, p6);
        Handles.DrawLine(p3, p7);
        Handles.DrawLine(p4, p8);
    }

    private static void UpdateRepaint()
    {
        if (CurrentHandlePosition != _OldHandlePosition)
        {
            SceneView.RepaintAll();
            _OldHandlePosition = CurrentHandlePosition;
        }
    }

    private static void UpdateIsMouseInValidArea(Rect sceneViewRect)
    {
        bool isInValidArea = true;

        if (isInValidArea != IsMouseInValidArea)
        {
            IsMouseInValidArea = isInValidArea;
            SceneView.RepaintAll();
        }
    }

    private static void UpdateHandlePosition()
    {
        if (Event.current.type == EventType.MouseUp && Event.current.alt==true)
        {
            Vector2 mousePosition = new Vector2(Event.current.mousePosition.x, Event.current.mousePosition.y);
            Ray ray = HandleUtility.GUIPointToWorldRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity) == true)
            {
                Vector3 offset = Vector3.zero;

                CurrentHandlePosition.x = Mathf.Floor(hit.point.x - hit.normal.x * 0.001f + offset.x);
                CurrentHandlePosition.y = Mathf.Floor(hit.point.y - hit.normal.y * 0.001f + offset.y);
                CurrentHandlePosition.z = Mathf.Floor(hit.point.z - hit.normal.z * 0.001f + offset.z);

                CurrentHandlePosition += new Vector3(0.5f, 0.5f, 0.5f);
            }
        }
    }

    static bool IsInCorrectLevel()
    {
        return UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name == "LevelEditor";
    }
}
