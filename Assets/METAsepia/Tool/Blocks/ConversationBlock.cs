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
    private Rect inputRect;
    #endregion

    #region Main Methods
    public string GetTextPrompt()
    {
        if (inputPlug)
        {
            //return 
        }
        return null;
    }

    public string GetTextResponse()
    {
        //placeholder
        return null;
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
        string inputName = "None";
        if (inputPlug)
        {
            inputName = inputPlug.GetTextResponse();
        }
        if(e.type == EventType.Repaint)
        {
            inputRect = GUILayoutUtility.GetLastRect();
        }
        GUILayout.Label("Responding to: " + inputName);
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
            /*if (responseRects[i].Contains(e.mousePosition))
            {
                Debug.Log(responses[i]);
            }*/
        }
    }

    public override void SetInput(PlugBase input, Vector2 clickPos)
    {
        clickPos.x -= blockRect.x;
        clickPos.y -= blockRect.y;

        if (inputRect.Contains(clickPos))
        {
            inputPlug = input as ResponsePlug;
        }
    }

    public override void DrawStrands()
    {
        if (inputPlug)
        {
            Rect rect = blockRect;

            rect.x += inputRect.x;
            rect.y += inputRect.y + inputRect.height / 2;

            rect.height = 1;
            rect.width = 1;

            METAsepiaEditor.DrawBlockStrands(inputRect, rect);

        }
    }

    public override void ClearInput(BlockBase block)
    {
        if (block.Equals(inputPlug))
        {
            inputPlug = null;
        }
    }



    #endregion
}
