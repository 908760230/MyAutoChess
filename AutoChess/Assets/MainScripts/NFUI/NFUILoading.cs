using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using NFrame;

public class NFUILoading : NFUIDialog
{
    private NFUIModule mUIModule;
    private NFIEventModule mEventModule;
    private NFSceneModule mSceneModule;
    private NFIKernelModule mKernelModule;

    public Slider sliderProgress;

    private float time;
    int progress = 0;

    AsyncOperation async = null;
    int currentScene = 0;

    private void Awake()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();

        mUIModule = xPluginManager.FindModule<NFUIModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();
        mSceneModule = xPluginManager.FindModule<NFSceneModule>();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
    }



    public override void Init()
    {

    }

    private void Update()
    {
        if (async != null)
        {
            progress = (int)(async.progress * 100);
            sliderProgress.value = progress;
            if (async.isDone)
            {
                mUIModule.HidenUI<NFUILoading>();

                switch (currentScene)
                {
                    case 3:
                        mUIModule.ShowUI<NFGameSceneUI>();

                        mEventModule.DoEvent((int)NFLoginModule.Event.SetCameraPos);
                        mEventModule.DoEvent((int)NFLoginModule.Event.InitGameUISetting);


                        ChessPlane firstMap = mSceneModule.createChessPlane("FirstMap");
                        ChessPlane secondMap = mSceneModule.createChessPlane("SecondMap");

                        for (int i = 0; i < mSceneModule.playerList.Count; i++)
                        {
                            NFGUID id = (NFGUID)mSceneModule.playerList[i];
                            NFVector3 pos = mKernelModule.QueryPropertyVector3(id, NFrame.Player.Position);
                            int index = (int)pos.X();
                            switch (index)
                            {
                                case 0:
                                    mSceneModule.chessPlaneDict[id] = firstMap;
                                    firstMap.PlayerID = id;
                                    firstMap.Init();
                                    break;
                                case 1:
                                    mSceneModule.chessPlaneDict[id] = secondMap;
                                    secondMap.PlayerID = id;
                                    secondMap.Init();
                                    break;
                            }
                        }

                        break;
                }

                sliderProgress.value = 0;
                async = null;
            }

        }
    }

    private IEnumerator loadScene(int sceneID)
    {
        switch (sceneID)
        {
            /*case 1:
                async = SceneManager.LoadSceneAsync("defualt");
                break;*/
            case 2:
                async = SceneManager.LoadSceneAsync("SinglePlayer");

                currentScene = 2;
                break;
            case 3:
                async = SceneManager.LoadSceneAsync("TwoPlayers");
                currentScene = 3;
                break;
        }
        yield return async;
    }
    public void LoadScene(int sceneId)
    {
        StartCoroutine(loadScene(sceneId));
    }
}