using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class ConversationBlock : BlockBase {

    #region Public Variables
    public string prompt;
    public List<PlugBase> responses;

    private List<Rect> responseRects;
    //private List<> list of 

    //private List<>
    #endregion

    #region Public Events
    public delegate void AddResponseAction();
    public delegate void RemoveResponseAction();

    public static event AddResponseAction OnAddResponse;
    public static event RemoveResponseAction OnRemoveResponse;
    #endregion

    #region Private Variables
    private ResponsePlug inputPlug;
    private string inputName;
    private Rect inputRect;
    #endregion

    #region Main Methods
    public string GetTextPrompt()
    {
        return prompt.ToString();
    }
    
    #endregion


    #region Utility Methods

    void OnEnable()
    {
        responses = new List<PlugBase>();
    }
    public override void DrawBlock()
    {
        base.DrawBlock();
        Event e = Event.current;
        if (inputPlug)
        {
            inputName = inputPlug.GetTextResponse();
        }
        if(e.type == EventType.Repaint)
        {
            inputRect = GUILayoutUtility.GetLastRect();
        }
        GUILayout.Label("Responding to" + inputName);
        prompt = EditorGUILayout.TextField("Prompt:", prompt);
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Add Response"))
        {
            if (OnAddResponse != null)
            {
                OnAddResponse();
            }
        }
        if(GUILayout.Button("Remove Response"))
        {
            if(OnRemoveResponse != null)
            {
                OnRemoveResponse();
            }
            
        }
        EditorGUILayout.EndHorizontal();
        for(int i = 0; i < responses.Count; i++)
        {
            responses[i].DrawBlock();
        }
    }
    public override void DrawStrands()
    {
        if (inputPlug)
        {
            Rect rect = blockRect;

        }
    }
    #endregion
}
