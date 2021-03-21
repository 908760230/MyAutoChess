using UnityEngine;
using UnityEngine.UI;
using NFSDK;
using NFrame;
using System.IO;
using Google.Protobuf;
using System.Collections;

public class NFUIMain : NFUIDialog
{
    public Button btnPlay;
    public Button btnSmaller;
    public Button btnBigger;
    public Button btnExit;
    public Button btnSinglePlayer;
    public Button btnOneVsOne;
    public Image iconImage;
    public Text textName;
    public Text textGold;
    public Image expValue;

    public GameObject queuePanle;
    public Text queueTime;
    public Button BtnCancel;

    private NFLoginModule mLoginModule;
    private NFNetModule mNetModule;
    private NFUIModule mUIModule;
    private NFIEventModule mEventModule;
    private NFIKernelModule mKernelModule;
    private NFIElementModule mElementModule;


    private MemoryStream mxBody = new MemoryStream();
    Coroutine coroutineTimer;
    private int totalTime =0;

    private void Awake()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mNetModule = xPluginManager.FindModule<NFNetModule>();
        mLoginModule = xPluginManager.FindModule<NFLoginModule>();
        mUIModule = xPluginManager.FindModule<NFUIModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();

        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
        mElementModule = xPluginManager.FindModule<NFIElementModule>();

    }

    public override void Init() 
    {
        btnPlay.onClick.AddListener(BtnPlayClicked);
        btnSmaller.onClick.AddListener(BtnSmallerClicked);
        btnBigger.onClick.AddListener(BtnBiggerClicked);
        btnExit.onClick.AddListener(BtnExitClicked);
        btnSinglePlayer.onClick.AddListener(BtnSinglePlayerClicked);
        btnOneVsOne.onClick.AddListener(btnOneVsOneClicked);
        BtnCancel.onClick.AddListener(onBtnCancelClicked);

        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.MAXEXP, OnExpChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.Gold, OnGoldChange);
    }

    private void Update()
    {
        
    }

    private void BtnPlayClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;
        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.RequireIntoQueue, mxBody);
        queuePanle.SetActive(true);
        btnPlay.gameObject.SetActive(false);
        coroutineTimer = StartCoroutine(startTimer());

    }

    private void BtnSmallerClicked()
    {

    }

    private void BtnBiggerClicked()
    {

    }

    private void BtnExitClicked()
    {
        Application.Quit();
    }

    private void BtnSinglePlayerClicked()
    {
        mUIModule.CloseAllUI();
        NFUILoading mUI =  mUIModule.ShowUI<NFUILoading>();
        mUI.LoadScene(2);
    }

    private void btnOneVsOneClicked()
    {
        
    }

    private void OnExpChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {

    }

    private void OnGoldChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {

    }

    public void SetDefaultInformation()
    {
        NFIObject mObj = mKernelModule.GetObject(mLoginModule.mRoleID);
        if (mObj != null)
        {
            textGold.text = mObj.GetPropertyManager().GetProperty("Gold").GetData().ToString();
            textName.text = mObj.GetPropertyManager().GetProperty("NickName").GetData().ToString();
        }
    }

    private void  onBtnCancelClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;
        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.CancleIntoQueue, mxBody);
        if (coroutineTimer != null) StopCoroutine(coroutineTimer);
        btnPlay.gameObject.SetActive(true);
        queuePanle.SetActive(false);
        totalTime = 0;
        queueTime.text = "00:00";
    }
    private IEnumerator startTimer()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1);
        
        while(totalTime >= 0)
        {
            yield return waitForSeconds;
            totalTime++;
            int minute = totalTime / 60; //输出显示分
            int second = totalTime % 60; //输出显示秒
            string formateTime;
            if (minute >= 10)
            {
                formateTime = minute + ":";
            }
            //如果秒小于10的时候，就输出格式为 00：00
            else formateTime = "0" + minute + ":" ;
            if (second >= 10)
            {
                formateTime +=  second;
            }
            //如果秒小于10的时候，就输出格式为 00：00
            else formateTime += "0" + second;

            queueTime.text = formateTime;
        }

    }
}
