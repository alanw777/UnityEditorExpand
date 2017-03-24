using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public class PreviewTime 
{
    public static float Time
    {
        get
        {
            if( Application.isPlaying == true )
            {
                return UnityEngine.Time.timeSinceLevelLoad;
            }
            return EditorPrefs.GetFloat( "PreviewTime", 0f );
        }
        set
        {
            EditorPrefs.SetFloat( "PreviewTime", value );
        }
    }
}
public class PreviewPlaybackWindow : EditorWindow
{
    [MenuItem("Window/Preview Playback Window")]
    static void OpenPreviewPlaybackWindow()
    {
        EditorWindow.GetWindow<PreviewPlaybackWindow>(false, "Playback");
    }

    float m_PlaybackModifier;
    float m_LastTime;

    void OnEnable()
    {
        EditorApplication.update -= OnUpdate;
        EditorApplication.update += OnUpdate;
    }

    void OnDisable()
    {
        EditorApplication.update -= OnUpdate;
    }

    void OnUpdate()
    {
        if (m_PlaybackModifier != 0f)
        {
            // m_PlaybackModifier是用于控制预览播放速率的变量
            // 当它不为0的时候，说明需要刷新界面，更新时间
            PreviewTime.Time += (Time.realtimeSinceStartup - m_LastTime) * m_PlaybackModifier;

            // 当预览时间改变时，我们需要确保重绘这个窗口以便我们可以立即看到它的更新
            // 而Unity只会在它认为该窗口需要重绘时（例如我们移动了窗口）才会重绘
            // 因此我们可以调用Repaint函数来强制马上重绘
            Repaint();

            // 由于预览时间发生了变化，我们也希望可以立刻重绘Scene视图的界面
            SceneView.RepaintAll();
        }

        m_LastTime = Time.realtimeSinceStartup;
    }

    void OnGUI()
    {
        float seconds = Mathf.Floor(PreviewTime.Time % 60);
        float minutes = Mathf.Floor(PreviewTime.Time / 60);

        GUILayout.Label("Preview Time: " + minutes + ":" + seconds.ToString("00"));
        GUILayout.Label("Playback Speed: " + m_PlaybackModifier);

        GUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("|<", GUILayout.Height(30)))
            {
                PreviewTime.Time = 0f;
                SceneView.RepaintAll();
            }

            if (GUILayout.Button("<<", GUILayout.Height(30)))
            {
                m_PlaybackModifier = -5f;
            }

            if (GUILayout.Button("<", GUILayout.Height(30)))
            {
                m_PlaybackModifier = -1f;
            }

            if (GUILayout.Button("||", GUILayout.Height(30)))
            {
                m_PlaybackModifier = 0f;
            }

            if (GUILayout.Button(">", GUILayout.Height(30)))
            {
                m_PlaybackModifier = 1f;
            }

            if (GUILayout.Button(">>", GUILayout.Height(30)))
            {
                m_PlaybackModifier = 5f;
            }
        }
        GUILayout.EndHorizontal();
    }
}
