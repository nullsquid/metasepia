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

    //private List<>
    #endregion

    #region Public Events
    public delegate void AddResponseAction();
    public static event AddResponseAction OnAddResponse;
    #endregion

    #region Private Variables
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

        prompt = EditorGUILayout.TextField("Prompt:", prompt);
        if(GUILayout.Button("Add Response"))
        {
            if (OnAddResponse != null)
            {
                OnAddResponse();
            }
        }
    }
    public override void DrawStrands()
    {

    }
    #endregion
}
