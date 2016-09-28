using UnityEngine;
using System.Collections;
using UnityEditor;
public class ResponsePlug : PlugBase {

    private string response;
    private int id;
#if UNITY_EDITOR

    public delegate void AddStrandsAction();
    public static event AddStrandsAction OnAddStrand;

    public void OnEnable()
    {

    }
    public void OnDisable()
    {

    }
    public override void DrawBlock()
    {
        base.DrawBlock();
        response = EditorGUILayout.TextField("Response: ", response);
        if (GUILayout.Button("Add Strand"))
        {
            //Debug.Log(OnAddStrand);
            if(OnAddStrand != null)
            {
                OnAddStrand();
                Debug.Log("Drawing?");
            }
            Debug.Log("new strand? ");
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
