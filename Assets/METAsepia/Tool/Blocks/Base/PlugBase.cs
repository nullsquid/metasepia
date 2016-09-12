using UnityEngine;
using System.Collections;

public class PlugBase : BlockBase {

    private bool hasOutput = false;

    #region Main Methods
    public virtual PlugBase ClickedOnPlug(Vector2 pos)
    {
        return null;
    }
    #endregion

    #region Utility Methods
    public override void DrawBlock()
    {
        base.DrawBlock();
    }
    public override void DrawStrands()
    {

    }
    #endregion

}
