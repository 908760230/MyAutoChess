using UnityEngine;
using UnityEngine.UI;
using NFSDK;
using NFrame;
using System.IO;
using Google.Protobuf;


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



    private NFLoginModule mLoginModule;
    private NFNetModule mNetModule;
    private NFUIModule mUIModule;
    private NFIEventModule mEventModule;
    private NFIKernelModule mKernelModule;
    private NFIElementModule mElementModule;


    private MemoryStream mxBody = new MemoryStream();
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

        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.MAXEXP, OnExpChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.Gold, OnGoldChange);
    }

    private void Update()
    {
        
    }

    private void BtnPlayClicked()
    {

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
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.ReqSwapScene, mxBody);
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
}
