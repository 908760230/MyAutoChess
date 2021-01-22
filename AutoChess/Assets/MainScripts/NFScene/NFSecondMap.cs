using UnityEngine;
using NFSDK;
using NFrame;


public class NFSecondMap : ChessPlane
{
    private NFSceneModule SceneModule;
    private NFNetModule mNetModule;
    private NFUIModule mUIModule;
    private NFIEventModule mEventModule;
    private NFIKernelModule mKernelModule;
    private NFIElementModule mElementModule;

    private void Start()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mNetModule = xPluginManager.FindModule<NFNetModule>();
        mUIModule = xPluginManager.FindModule<NFUIModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();

        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
        mElementModule = xPluginManager.FindModule<NFIElementModule>();
        SceneModule = xPluginManager.FindModule<NFSceneModule>();
    }


}
