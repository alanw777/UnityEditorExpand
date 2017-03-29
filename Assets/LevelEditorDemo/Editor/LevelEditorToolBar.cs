using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[InitializeOnLoad]
public class LevelEditorToolBar : Editor
{
    static List<GameObject> _cubeList = new List<GameObject>(); 
    public static int SelectedTool
    {
        get
        {
            return EditorPrefs.GetInt("SelectedEditorTool", 0);
        }
        set
        {
            if (value == SelectedTool)
            {
                return;
            }

            EditorPrefs.SetInt("SelectedEditorTool", value);
        }
    }

    static LevelEditorToolBar()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
        SceneView.onSceneGUIDelegate += OnSceneGUI;
    }

    void OnDestroy()
    {
        SceneView.onSceneGUIDelegate -= OnSceneGUI;
    }


    static bool IsInCorrectLevel()
    {
        return UnityEditor.SceneManagement.EditorSceneManager.GetActiveScene().name == "LevelEditor";
    }

    static void OnSceneGUI(SceneView sceneView)
    {
        if (IsInCorrectLevel() == false)
        {
            return;
        }
        DrawToolsMenu(sceneView.position);
        if (SelectedTool == 0)
        {
            return;
        }
        // 通过创建一个新的ControlID我们可以把鼠标输入的Scene视图反应权从Unity默认的行为中抢过来
        // FocusType.Passive意味着这个控制权不会接受键盘输入而只关心鼠标输入
        int controlId = GUIUtility.GetControlID(FocusType.Passive);
        if (Event.current.type == EventType.mouseDown &&
            Event.current.button == 0 &&
            Event.current.alt == false &&
            Event.current.shift == false &&
            Event.current.control == false)
        {
            if (LevelEditor.IsMouseInValidArea == true)
            {
                if (SelectedTool == 1)
                {
                    // 如果选择的是erase按键（从场景七的静态变量SelectedTool判断得到），移除Cube          
                    RemoveBlock(LevelEditor.CurrentHandlePosition);
                }

                if (SelectedTool == 2)
                {
                    /// 如果选择的是add按键（从场景七的静态变量SelectedTool判断得到），添加Cube
                    AddBlock(LevelEditor.CurrentHandlePosition);
                }
            }
        }
        // 如果按下了Escape，我们就自动取消选择当前的按钮
        if (Event.current.type == EventType.keyDown &&
            Event.current.keyCode == KeyCode.Escape)
        {
            SelectedTool = 0;
        }
        // 把我们自己的controlId添加到默认的control里，这样Unity就会选择我们的控制权而非Unity默认的Scene视图行为
        HandleUtility.AddDefaultControl(controlId);
        
    }

    private static void AddBlock(Vector3 currentHandlePosition)
    {
        for (int i = 0; i < _cubeList.Count; i++)
        {
            if (_cubeList[i].transform.position == currentHandlePosition)
            {
                return;
            }
        }
        Debug.Log("add block");
        var cubeP= (GameObject)PrefabUtility.InstantiatePrefab(Resources.Load<GameObject>("Cube"));
        cubeP.transform.position = currentHandlePosition;
        _cubeList.Add(cubeP);
    }

    private static void RemoveBlock(Vector3 currentHandlePosition)
    {
        for (int i = 0; i < _cubeList.Count; i++)
        {
            if (_cubeList[i].transform.position == currentHandlePosition)
            {
                Debug.Log("remove block");
                DestroyImmediate(_cubeList[i]);
                _cubeList.RemoveAt(i);
            }
        }
    }

    static void DrawToolsMenu(Rect position)
    {
        Handles.BeginGUI();
        GUILayout.BeginArea(new Rect(0, position.height - 35, position.width, 20), EditorStyles.toolbar);
        {
            string[] buttonLabels = new string[] { "None", "Erase", "Paint" };
            SelectedTool = GUILayout.SelectionGrid(
                SelectedTool,
                buttonLabels,
                3,
                EditorStyles.toolbarButton,
                GUILayout.Width(300));
        }
        GUILayout.EndArea();
        Handles.EndGUI();
    }
}