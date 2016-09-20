using UnityEngine;
using System.Collections;
using UnityEditor;
public class ResponsePlug : PlugBase {

    private string response;
    #if UNITY_EDITOR
    public override void DrawBlock()
    {
        base.DrawBlock();
        response = EditorGUILayout.TextField("Response: ", response);
        if (GUILayout.Button("Add Strand"))
        {
            Debug.Log("new strand?");
            //need to add event here to notify metasepia editor
        }
    }
    //might need to let response know what node its attached to. do in METASEPIA editor
    public string GetTextResponse()
    {
        return response.ToString();
    }

    public override void DrawStrands()
    {

    }
#endif

}
