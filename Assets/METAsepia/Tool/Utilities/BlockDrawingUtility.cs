using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class BlockDrawingUtility : MonoBehaviour {
	
    public static void DrawBlockWindow(Dialogue curDialogue, int blockID)
    {
        curDialogue.blocks[blockID].DrawBlock();
        GUI.DragWindow();
    }
    
}
