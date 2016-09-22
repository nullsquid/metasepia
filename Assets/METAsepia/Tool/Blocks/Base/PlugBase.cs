using UnityEngine;
using UnityEditor;
using System.Collections;

public class PlugBase : BlockBase {

    public Rect plugRect;

    private bool hasOutput = false;


    #region Main Methods
    public virtual PlugBase ClickedOnPlug(Vector2 pos)
    {
        return null;
    }
    #endregion

    #region Utility Methods
    #if UNITY_EDITOR
    public override void DrawBlock()
    {
        //response = GUILayout.TextField("response", response);
        //response = EditorGUILayout.TextField("Response: ", response);
        
    }
    #endif
    public override void DrawStrands()
    {

    }
    #endregion

}
