using UnityEngine;
using System.Collections;
using UnityEditor;
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

        BeginWindows();
        
        if (curDialogue != null)
        {
            for (int i = 0; i < curDialogue.blocks.Count; i++)
            {
                curDialogue.blocks[i].blockRect = GUI.Window(i, curDialogue.blocks[i].blockRect, DrawBlockWindow, curDialogue.blocks[i].blockTitle);
                if (curDialogue.blocks[i] is ConversationBlock && curDialogue.blocks[i].blockRect.Contains(mousePos))
                {
                    
                    curConvBlock = curDialogue.blocks[i] as ConversationBlock;
                    Debug.Log("current conv block is " + curConvBlock.blockTitle);
                    selectedBlock = curConvBlock;

                    if (curConvBlock != null) {
                        if (curConvBlock.responses.Count > 0)
                        {
                            for (int j = 0; j < curConvBlock.responses.Count; j++)
                            {
                                //need to set up selectedblock
                                //why the hell isn't this working -_-
                                //curConvBlock.responses[j].blockRect = GUI.Window(j, curConvBlock.responses[j].blockRect, DrawPlugWindow, curConvBlock.responses[j].blockTitle);
                                
                                Debug.Log("number of responses = " + curConvBlock.responses.Count);
                            }
                        }
                    }
                }
            }
        }
        EndWindows();
    }

    void AddResponse()
    {
        ContextCallback("addPlug");
    }

    #endregion

    #region Utility Methods
    void OnEnable()
    {
        ConversationBlock.OnAddResponse += AddResponse;
    }

    void OnDisable()
    {
        ConversationBlock.OnAddResponse -= AddResponse;
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
            Debug.Log("plug added");
            ConversationBlock selectedConvBlock = selectedBlock as ConversationBlock;
            curConvBlock.blockRect.height += 20f;

            PlugBase plug = (PlugBase)ScriptableObject.CreateInstance("PlugBase");
            //plug.blockRect = new Rect(mousePos.x, mousePos.y, 100, 50);
            
            //ConversationBlock selectedConvBlock;
            //selectedConvBlock = selectedBlock as ConversationBlock;

            //PlugBase plug = (PlugBase)ScriptableObject.CreateInstance("PlugBase");

            //well this is fucked
            selectedConvBlock.responses.Add(plug);
            //plug.blockRect = new Rect(mousePos.x, mousePos.y, 100, 50);
            
            

        }
        ////
        else if (callback.Equals("newDialogue"))
        {
            curDialogue = null;
            curDialogue = (Dialogue)ScriptableObject.CreateInstance("Dialogue");
        }
    }
    #endregion

}
