using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Dialogue : ScriptableObject {

    #region Private Variables
    public List<BlockBase> blocks;
    #endregion

    #region Public Variables
    public string dialogueTitle = "";
    #endregion

    #region Main Methods
    public void OnEnable()
    {
        blocks = new List<BlockBase>();
    }

    public void AddBlock(BlockBase block)
    {
        blocks.Add(block);

    }

    public void RemoveBlock(int index)
    {
        blocks.RemoveAt(index);

    }

    public void RemoveBlock(BlockBase block, int index)
    {

    }
    #endregion

}
