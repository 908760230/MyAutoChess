using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NFSDK;

public class NFHPBar : MonoBehaviour
{
    public Text levelText;
    public Slider hpBar;
    public Slider mpBar;

    private NFIKernelModule mKernelModule;

    private void Awake()
    {

        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();


    }

    public void Init(NFGUID id)
    {
        mKernelModule.RegisterPropertyCallback(id, NFrame.NPC.Level, OnLevelChange);
        mKernelModule.RegisterPropertyCallback(id, NFrame.NPC.HP, OnHpChange);
        mKernelModule.RegisterPropertyCallback(id, NFrame.NPC.MP, OnMpChange);

    }

    private void OnLevelChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long lvlValue = newVar.IntVal();
        levelText.text = lvlValue.ToString();
    }

    private void OnHpChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        float hpValue = (float)newVar.FloatVal();
        float maxHp = (float)mKernelModule.QueryPropertyFloat(self, NFrame.NPC.MAXHP);
        hpBar.value = hpValue / maxHp; 
    }

    private void OnMpChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        float mpValue = newVar.IntVal();
        float maxMp = mKernelModule.QueryPropertyInt(self, NFrame.NPC.MAXMP);
        mpBar.value = mpValue / maxMp;
    }
}
