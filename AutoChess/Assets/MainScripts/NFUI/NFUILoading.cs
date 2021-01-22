﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFSDK;
using UnityEngine.SceneManagement;
using Packages.Rider.Editor;
using UnityEngine.UI;

public class NFUILoading : NFUIDialog
{
    private NFUIModule mUIModule;
    public Slider sliderProgress;

    private float time;
    int progress = 0;

    AsyncOperation async = null;

    private void Awake()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();

        mUIModule = xPluginManager.FindModule<NFUIModule>();
    }

    

    public override void Init()
    {

    }

    private void Update()
    {
        if(async != null)
        {
            progress = (int)(async.progress * 100);
            sliderProgress.value = progress;
            if (async.isDone)
            {
                mUIModule.HidenUI<NFUILoading>();
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
                break;
            case 3:
                async = SceneManager.LoadSceneAsync("TwoPlayers");
                break;
        }
        yield return async;
    }
    public void LoadScene(int sceneId)
    {
        StartCoroutine(loadScene(sceneId));
    }
}