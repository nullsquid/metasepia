using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Linq;

public class METAsepiaEditor : EditorWindow {
    #region Private Variables
    private Dialogue curDialogue;
    private Vector2 mousePos;
    private BlockBase selectedBlock;
    private bool strandModeEnabled = false;
    private ConversationBlock curConvBlock;
    //will use to get data during runtime
    private BlockBase curBlock;
    #endregion

    #region Public Variables
    #endregion

    #region Main Methods
    [MenuItem("METAsepia/New METAsepia Window")]
    static void ShowEditor()
    {
        METAsepiaEditor editor = EditorWindow.GetWindow<METAsepiaEditor>();
        editor.titleContent = new GUIContent("METAsepia");
    }
    //will need to use preprocessor here
    void OnGUI()
    {
        Event e = Event.current;
        mousePos = e.mousePosition;

        if(e.button == 1 && !strandModeEnabled && curDialogue == null)
        {

            if (e.type == EventType.MouseDown)
            {

                GenericMenu menu = new GenericMenu();

                menu.AddItem(new GUIContent("Create New Dialogue"), false, ContextCallback, "newDialogue");
                menu.AddSeparator("");
                menu.AddItem(new GUIContent("Load Dialogue"), false, ContextCallback, "loadDialogue");

                menu.ShowAsContext();

                e.Use();
            }

        }
        else if(e.button == 1 && !strandModeEnabled && curDialogue != null)
        {
            if(e.type == EventType.MouseDown)
            {
                bool clickedOnBlock = false;
                int selectedIndex = -1;
                if (curDialogue.blocks.Count > 0)
                {
                    for (int i = 0; i < curDialogue.blocks.Count; i++)
                    {
                        if (curDialogue.blocks[i].blockRect.Contains(mousePos))
                        {
                            selectedIndex = i;
                            clickedOnBlock = true;
                            break;
                        }
                    }
                }
                if (!clickedOnBlock)
                {
                    GenericMenu menu = new GenericMenu();
                    menu.AddItem(new GUIContent("Conversation Block"), false, ContextCallback, "convBlock");

                    menu.ShowAsContext();
                    e.Use();
                }
            }
        }
        if(strandModeEnabled && selectedBlock != null)
        {
            Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
            DrawBlockStrands(selectedBlock.blockRect, mouseRect);

            //The following is probably closer to the right way of doing it

            /*for(int i = 0; i < curConvBlock.responses.Count; i++)
            {
                if (curConvBlock.responses[i].plugRect.Contains(e.mousePosition))
                {
                    Debug.Log(curConvBlock.responses[i].plugRect);
                    DrawBlockStrands(curConvBlock.responses[i].plugRect, mouseRect);
                    Debug.Log("Add strand");
                    break;
                }
            }*/
            //DrawBlockStrands(curConvBlock.responses[])
            Repaint();
        }
        if (curDialogue != null)
        {
            foreach (BlockBase b in curDialogue.blocks)
            {
                b.DrawStrands();
            }

        }
        BeginWindows();
        
        if (curDialogue != null)
        {
            for (int i = 0; i < curDialogue.blocks.Count; i++)
            {
                curDialogue.blocks[i].blockRect = GUI.Window(i, curDialogue.blocks[i].blockRect, DrawBlockWindow, curDialogue.blocks[i].blockTitle);
                if (curDialogue.blocks[i] is ConversationBlock && curDialogue.blocks[i].blockRect.Contains(mousePos))
                {
                    
                    curConvBlock = curDialogue.blocks[i] as ConversationBlock;
                    
                    selectedBlock = curConvBlock;

                    
                }
            }
        }
        EndWindows();
        
    }

    void AddResponse()
    {
        ContextCallback("addPlug");
    }

    void RemoveResponse()
    {
        ContextCallback("removePlug");
    }

    void AddStrand()
    {
        ContextCallback("makeStrand");
    }

    #endregion

    #region Utility Methods
    void OnEnable()
    {
        ConversationBlock.OnAddResponse += AddResponse;
        ConversationBlock.OnRemoveResponse += RemoveResponse;

        ResponsePlug.OnAddStrand += AddStrand;
    }

    void OnDisable()
    {
        ConversationBlock.OnAddResponse -= AddResponse;
        ConversationBlock.OnRemoveResponse -= RemoveResponse;

        ResponsePlug.OnAddStrand -= AddStrand;
    }

    void DrawBlockWindow(int id)
    {
        curDialogue.blocks[id].DrawBlock();
        GUI.DragWindow();
    }

    void DrawPlugWindow(int plugID)
    {
        curConvBlock.responses[plugID].DrawBlock();
        GUI.DragWindow();
    }
    public static void DrawBlockStrands(Rect start, Rect end)
    {
        Vector3 startPos = new Vector3(start.x + start.width / 2, start.y + start.height / 2, 0);
        Vector3 endPos = new Vector3(end.x + end.width / 2, end.y + end.height / 2, 0);
        Vector3 startTan = startPos + Vector3.right * 50;
        Vector3 endTan = endPos + Vector3.left * 50;

        Color shadowColor = new Color(.9f, 0, .7f, 0.08f);


        for (int i = 0; i < 3; i++)
        {
            Handles.DrawBezier(startPos, endPos, startTan, endTan, shadowColor, null, (i + 1) * 5);
        }
        Handles.DrawBezier(startPos, endPos, startTan, endTan, Color.magenta, null, 1);

    }
    void ContextCallback(object obj)
    {
        string callback = obj.ToString();
        if (callback.Equals("convBlock"))
        {
            ConversationBlock convBlock = (ConversationBlock)ScriptableObject.CreateInstance("ConversationBlock");
            convBlock.blockRect = new Rect(mousePos.x, mousePos.y, 200, 100);

            curDialogue.AddBlock(convBlock);
        }
        else if (callback.Equals("addPlug"))
        {
            ConversationBlock selectedConvBlock = selectedBlock as ConversationBlock;

            //need to math out how high this can and should be
            curConvBlock.blockRect.height += 50f;

            ResponsePlug resPlug = (ResponsePlug)ScriptableObject.CreateInstance("ResponsePlug");
            resPlug.plugRect = new Rect(curConvBlock.blockRect.x, 100f/*curConvBlock.blockRect.height * curConvBlock.responses.Count*/ , 50f, curConvBlock.blockRect.width);
            //resPlug.plugRect = new Rect
            
            selectedConvBlock.responses.Add(resPlug);
            
            

        }
        else if (callback.Equals("removePlug"))
        {
            Debug.Log("plug removed");
            
            ConversationBlock selectedConvBlock = selectedBlock as ConversationBlock;
            if (selectedConvBlock.responses.Count > 0){

                

                for (int i = 0; i < selectedConvBlock.responses.Count; i++)
                {
                    if (selectedConvBlock.responses[i] == selectedConvBlock.responses.Last())
                    {
                        selectedConvBlock.responses.Remove(selectedConvBlock.responses[i]);
                    }
                }
                curConvBlock.blockRect.height -= 50f;

            }


        }
        ////////////////////////////////////////////////////
        else if (callback.Equals("newDialogue"))
        {
            curDialogue = null;
            curDialogue = (Dialogue)ScriptableObject.CreateInstance("Dialogue");
        }
        ///////////////////////////////////////////////////
        else if (callback.Equals("makeStrand"))
        {
            bool clickedOnBlock = false;
            int selectedIndex = -1;

            for(int i = 0; i < curDialogue.blocks.Count; i++)
            {
                if (curDialogue.blocks[i].blockRect.Contains(mousePos))
                {
                    selectedIndex = i;
                    clickedOnBlock = true;
                    break;
                }
            }
            if (clickedOnBlock)
            {
                selectedBlock = curDialogue.blocks[selectedIndex];
                strandModeEnabled = true;
            }
        }
    }

    #endregion

}
