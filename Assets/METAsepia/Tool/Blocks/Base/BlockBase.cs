using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public abstract class BlockBase : ScriptableObject {
    #region Public Variables
    public Rect blockRect;
    public bool hasInputs = false;
    public string blockTitle = "";
    #endregion

    #region Private Variables

    #endregion

    #region Main Methods
    public virtual void SetInput(PlugBase input, Vector2 clickPos)
    {

    }


    #endregion

    #region Utility Methods
    public virtual void DrawBlock()
    {
        blockTitle = EditorGUILayout.TextField("Title: ", blockTitle);
    }

    public abstract void DrawStrands();

    //public virtual void BlockIsRemoved(BlockBase block) { }
    #endregion

}
