using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NFrame;
using NFSDK;

public class NFChatRoom : NFUIDialog
{

    public InputField chatInput;
    public Text chatText;
    public ScrollRect scrollRect;


    private NFLoginModule mLoginModule;
    private NFNetModule mNetModule;
    private NFIEventModule mEventModule;
    private NFIKernelModule mKernelModule;

    public override void Init()
    {

    }

    private void Awake()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mNetModule = xPluginManager.FindModule<NFNetModule>();
        mLoginModule = xPluginManager.FindModule<NFLoginModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (gameObject.activeSelf && chatInput.text != "")
            {
                string addText = "\n  " + "<color=red>" + "张三" + "</color>: " + chatInput.text;
                chatText.text += addText;
                chatInput.text = "";
                chatInput.ActivateInputField();
                Canvas.ForceUpdateCanvases();       //关键代码
                scrollRect.verticalNormalizedPosition = 0f;  //关键代码
                Canvas.ForceUpdateCanvases();   //关键代码
            }
            else if (gameObject.activeSelf && chatInput.text == "")
            {
                gameObject.SetActive(false);
            }

        }
    }
}
