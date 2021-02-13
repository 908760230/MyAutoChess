using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.EventSystems;
using NFSDK;
using NFrame;
using System.IO;
using Google.Protobuf;


public class NFGameSceneUI : NFUIDialog
{

    public GameObject[] championsFrameArray;
    public GameObject[] bonusPanels;

    public Button playerOne;
    public Text playerOneName;
    public Text playerOneHP;
    public Button playerTwo;
    public Text playerTwoName;
    public Text playerTwoHP;

    public Button BtnBuyLvL;
    public Button BtnRefresh;
    public Button BtnChampionOne;
    public Button BtnChampionTwo;
    public Button BtnChampionThree;
    public Button BtnChampionFour;
    public Button BtnChampionFive;

    public Text timerText;
    public Text championCountText;
    public Text goldText;
    public Text lvlText;
    public Text maxHeroText;
    public Text heroCountText;

    public GameObject shop;
    public GameObject placementText;
    public GameObject gold;
    public GameObject bonusContainer;
    public GameObject bonusUIPrefab;
    public GameObject placement;
    public GameObject championLimit;

    public static Vector3 cameraOnePosition = new Vector3(0, (float)26.33, (float)18.56);
    public static Vector3 cameraTwoPosition = new Vector3(100, (float)26.33, (float)18.56);

    private NFLoginModule mLoginModule;
    private NFNetModule mNetModule;
    private NFUIModule mUIModule;
    private NFIEventModule mEventModule;
    private NFIKernelModule mKernelModule;
    private NFIElementModule mElementModule;
    private NFSceneModule sceneModule;

    private MemoryStream mxBody = new MemoryStream();

    private void Awake()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mNetModule = xPluginManager.FindModule<NFNetModule>();
        mLoginModule = xPluginManager.FindModule<NFLoginModule>();
        mUIModule = xPluginManager.FindModule<NFUIModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();
        sceneModule = xPluginManager.FindModule<NFSceneModule>();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
        mElementModule = xPluginManager.FindModule<NFIElementModule>();
    }

    public override void Init()
    {
        playerOne.onClick.AddListener(OnPlayerOneButtonClicked);
        playerTwo.onClick.AddListener(OnPlayerTwoButtonClicked);
        BtnRefresh.onClick.AddListener(RefreshButtonClicked);
        BtnBuyLvL.onClick.AddListener(BuyXPButtonClicked);
        BtnChampionOne.onClick.AddListener(OnChampionOneClicked);
        BtnChampionTwo.onClick.AddListener(OnChampionTwoClicked);
        BtnChampionThree.onClick.AddListener(OnChampionThreeClicked);
        BtnChampionFour.onClick.AddListener(OnChampionFourClicked);
        BtnChampionFive.onClick.AddListener(OnChampionFiveClicked);

        mNetModule.AddReceiveCallBack((int)NFMsg.EGameMsgID.AckEventBuyChampion, onChampionShopCLicked);

        //mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.State, onPlayerStateChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.GameGold, onGameGoldChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.MaxHero, OnMaxHeroChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.GameLVL, OnLVLChange);
        mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.HeroCount, OnHeroCountChange);
        //mKernelModule.RegisterPropertyCallback(mLoginModule.mRoleID, NFrame.Player.HP, onHpChange);

        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameTime, UpdateTimerText);
        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, onGameStateChange);

        mKernelModule.RegisterRecordCallback(mLoginModule.mRoleID, NFrame.Player.ChampionShop.ThisName, onShopRecordEvent);

        mEventModule.RegisterCallback((int)NFLoginModule.Event.InitGameUISetting, initGameUI);
        mEventModule.RegisterCallback((int)NFLoginModule.Event.SetCameraPos, setCameraPos);

    }

    private void onChampionShopCLicked(int id, MemoryStream stream)
    {
        NFMsg.MsgBase xMsg = NFMsg.MsgBase.Parser.ParseFrom(stream);

        NFMsg.ReqAckSwapScene xData = NFMsg.ReqAckSwapScene.Parser.ParseFrom(xMsg.MsgData);
        int index = (int)xData.X;
        Debug.Log("onChampionShopCLicked: " + index);
        championsFrameArray[index].SetActive(false);
    }

    public void RefreshButtonClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventRefreshShop,mxBody);
    }

    public void BuyXPButtonClicked()
    {

        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyLevel, mxBody);
    }

    private void OnChampionOneClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 0;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyChampion, mxBody);
    }

    private void OnChampionTwoClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 1;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyChampion, mxBody);
    }

    private void OnChampionThreeClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 2;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyChampion, mxBody);
    }

    private void OnChampionFourClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 3;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyChampion, mxBody);
    }

    private void OnChampionFiveClicked()
    {
        NFMsg.ReqAckSwapScene xData = new NFMsg.ReqAckSwapScene();
        xData.TransferType = 0;
        xData.SceneId = 3;
        xData.X = 4;
        xData.Y = 0;
        xData.Z = 0;

        mxBody.SetLength(0);
        xData.WriteTo(mxBody);
        mNetModule.SendMsg((int)NFMsg.EGameMsgID.EventBuyChampion, mxBody);
    }

    public void ShowShopItems()
    {
        for (int i = 0; i < championsFrameArray.Length; i++)
        {
            championsFrameArray[i].SetActive(true);
        }
    }
    // 根据索引在UI上展示英雄信息
    void onShopRecordEvent(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {

        NFIRecord record = mKernelModule.FindRecord(self,strRecordName);
        for (int i = 0; i < record.GetRows(); i++)
        {
            championsFrameArray[i].SetActive(true);
            Transform championUI = championsFrameArray[i].transform;
            Transform top = championUI.Find("top");
            Transform bottom = championUI.Find("bottom");
            Transform type1 = top.Find("type 1");
            Transform type2 = top.Find("type 2");
            Transform name = bottom.Find("Name");
            Transform cost = bottom.Find("Cost");
            Transform icon1 = top.Find("icon 1");
            Transform icon2 = top.Find("icon 2");

            string element = record.QueryString(i, 0);
            string race = record.QueryString(i, 1);
            name.GetComponent<Text>().text = element + race;
            cost.GetComponent<Text>().text = record.QueryInt(i, 2).ToString();
            type1.GetComponent<Text>().text = element;
            type2.GetComponent<Text>().text = race;

            icon1.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + element);
            icon2.GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + race);
        }
    }

    public void updateBunusPanel(Dictionary<string,int> bonusData)
    {
        hidenBonusPanel();
        int i = 0;
        foreach (KeyValuePair<string, int> m in bonusData)
        {
            Debug.Log("updateBunusPanel() bonusData " + i.ToString() + " key: " + m.Key + " " + m.Value.ToString());
            GameObject bonusUI = bonusPanels[i];
            bonusUI.transform.SetParent(bonusContainer.transform);
            bonusUI.transform.Find("icon").GetComponent<Image>().sprite = Resources.Load<Sprite>("Sprites/" + m.Key);
            bonusUI.transform.Find("name").GetComponent<Text>().text = m.Key;
            bonusUI.transform.Find("count").GetComponent<Text>().text = m.Value.ToString();
            bonusUI.SetActive(true);
            i++;
        }
    }
    

    public void SetTimerTextActive(bool b)
    {
        timerText.gameObject.SetActive(b);
        placementText.SetActive(b);
    }
    public void ShowLossScreen()
    {

    }
    public void ShowGameScreen()
    {

    }

    private void OnPlayerOneButtonClicked()
    {
        Camera.main.transform.position = cameraOnePosition;
        mEventModule.DoEvent((int)NFLoginModule.Event.UpdatePlayerOneBonusUI);
    }

    private void OnPlayerTwoButtonClicked()
    {
        Camera.main.transform.position = cameraTwoPosition;
        mEventModule.DoEvent((int)NFLoginModule.Event.UpdatePlayerTwoBonusUI);
    }
    public void setCameraPos(NFDataList valueList)
    {
        NFVector3 pos = mKernelModule.QueryPropertyVector3(mLoginModule.mRoleID, NFrame.Player.Position);
        int index = (int)pos.X();

        switch (index)
        {
            case 0:
                OnPlayerOneButtonClicked();
                break;
            case 1:
                OnPlayerTwoButtonClicked();
                break;
        }
    }

    private void UpdateTimerText(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        timerText.text = newVar.IntVal().ToString();
    }
    private void onHpChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {

        string HpVal = mKernelModule.FindProperty(self, NFrame.Player.HP).GetData().IntVal().ToString();

        NFVector3 pos = mKernelModule.QueryPropertyVector3(self, NFrame.Player.Position);
        int index = (int)pos.X();

        switch (index)
        {
            case 0:
                playerOneHP.text = HpVal;
                break;
            case 1:
                playerTwoHP.text = HpVal;
                break;
        }
        
    }
    private void initGameUI(NFDataList valueList)
    {
       
        for (int i = 0; i < sceneModule.playerList.Count; i++)
        {
            NFGUID id = (NFGUID)sceneModule.playerList[i];
            NFVector3 pos = mKernelModule.QueryPropertyVector3(id, NFrame.Player.Position);
            Debug.Log(id.ToString()+" position: " + pos.ToString());
            int index = (int)pos.X();
            switch (index)
            {
                case 0:
                    playerOneName.text = mKernelModule.FindProperty(id, NFrame.Player.NickName).GetData().StringVal();
                    playerOneHP.text = mKernelModule.FindProperty(id, NFrame.Player.HP).GetData().IntVal().ToString();
                    mKernelModule.RegisterPropertyCallback(id, NFrame.Player.HP, onHpChange);
                    break;

                case 1:
                    playerTwoName.text = mKernelModule.FindProperty(id, NFrame.Player.NickName).GetData().StringVal();
                    playerTwoHP.text = mKernelModule.FindProperty(id, NFrame.Player.HP).GetData().IntVal().ToString();
                    mKernelModule.RegisterPropertyCallback(id, NFrame.Player.HP, onHpChange);
                    break;
            }
            
        }
      
        hidenBonusPanel();

    }
    private void onGameGoldChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        goldText.text = newVar.IntVal().ToString();
    }
    private void onPlayerStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        
    }

    private void onGameStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long gameState = newVar.IntVal();
        switch (gameState)
        {
            case 0:
                placement.SetActive(true); //preparation stage
                championLimit.SetActive(true);
                GameMain.Instance().currentGameStage = GameStage.Preparation;
                break;
            case 1:
                placement.SetActive(false); // combat stage
                championLimit.SetActive(false);
                GameMain.Instance().currentGameStage = GameStage.Combat;
                break;
        }
    }

    private void OnLVLChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        lvlText.text = newVar.IntVal().ToString();
    }

    private void OnMaxHeroChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        maxHeroText.text = newVar.IntVal().ToString();
    }
    private void OnHeroCountChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        heroCountText.text = newVar.IntVal().ToString();
    }
    
    private void hidenBonusPanel()
    {
        foreach (GameObject go in bonusPanels)
        {
            go.SetActive(false);
        }
    }
}
