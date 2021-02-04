using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NFSDK;

public class NFHPBar : MonoBehaviour
{
    public Text level;
    public Slider hpBar;

    private NFIKernelModule mKernelModule;

    private void Awake()
    {

        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();

    }

    public void Init(NFGUID id)
    {
        mKernelModule.RegisterPropertyCallback(id, NFrame.NPC.State, OnStateChange);
    }

    private void OnStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long state = newVar.IntVal();
        switch (state)
        {
            case 1:
                gameObject.SetActive(true);
                break;
            case 0:
                gameObject.SetActive(false);
                break;
        }
    }
}
