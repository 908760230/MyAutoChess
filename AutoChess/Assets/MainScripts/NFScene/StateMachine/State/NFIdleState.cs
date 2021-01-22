using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NFrame;
using NFSDK;

public class NFIdleState : NFIState
{
    private NFBodyIdent xBodyIdent;
    //private NFHeroMotor xHeroMotor;
    //private NFHeroSync xHeroSync;

    private Vector3 vector3 = new Vector3();


    private NFIKernelModule mKernelModule;

    private NFLoginModule mLoginModule;
    private NFSceneModule mSceneModule;

    public NFIdleState(GameObject gameObject, NFAnimaStateType eState, NFAnimaStateMachine xStateMachine, float fHeartBeatTime, float fExitTime, bool input = false)
        : base(gameObject, eState, xStateMachine, fHeartBeatTime, fExitTime, input)
    {
        NFIPluginManager pluginManager = GameMain.Instance().GetPluginManager();

        mKernelModule = pluginManager.FindModule<NFIKernelModule>();
        mLoginModule = GameMain.Instance().GetPluginManager().FindModule<NFLoginModule>();
        mSceneModule = GameMain.Instance().GetPluginManager().FindModule<NFSceneModule>();
    }

    private bool Fall()
    {
        return false;
    }

    public override void Enter(GameObject gameObject, int index)
    {
        xBodyIdent = gameObject.GetComponent<NFBodyIdent>();
        //xHeroMotor = gameObject.GetComponent<NFHeroMotor>();
        //xHeroSync = gameObject.GetComponent<NFHeroSync>();

        base.Enter(gameObject, index);

        if (mStateMachine.IsMainRole())
        {
            //xHeroSync.SendSyncMessage();
        }
        //看是否还按住移动选项，如果按住，则继续walk

        /*if (!xHeroMotor.isOnGround)
        {
            mAnimatStateController.PlayAnimaState(NFAnimaStateType.Fall, -1);
        }*/
    }

    public override void Execute(GameObject gameObject)
    {
        base.Execute(gameObject);
        /*if (gameObject.transform.position.y < -10)
        {
            GameObject go = mSceneModule.GetObject(mLoginModule.mRoleID);
            if (go != null)
            {
                if (Vector3.Distance(go.transform.position, gameObject.transform.position) < 50)
                {
                    vector3.x = gameObject.transform.position.x;
                    vector3.y = 22;
                    vector3.z = gameObject.transform.position.z;
                    gameObject.transform.position = vector3;
                }
            }
        }*/
    }

}