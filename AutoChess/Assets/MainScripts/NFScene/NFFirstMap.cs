using System.Collections.Generic;
using UnityEngine;
using NFSDK;
using NFrame;


public class NFFirstMap : ChessPlane
{
    
    public Dictionary<string,int> bonusData = new Dictionary<string, int>();

    private NFUIModule uiModule;
    private NFIEventModule mEventModule;
    private void Start()
    {

        NFIPluginManager pluginManager = GameMain.Instance().GetPluginManager();
        uiModule = pluginManager.FindModule<NFUIModule>();
        mEventModule = pluginManager.FindModule<NFIEventModule>();

        mKernelModule.RegisterRecordCallback((NFGUID)mSceneModule.playerList[0], NFrame.Player.ownInventory.ThisName, OnOwnInventoryChange);
        mKernelModule.RegisterGroupRecordCallback(Group.ChessPlane1.ThisName, OnBattleMapChange);

        mKernelModule.RegisterPropertyCallback((NFGUID)mSceneModule.playerList[0], Player.State, refreshMap);
        mKernelModule.RegisterRecordCallback((NFGUID)mSceneModule.playerList[0], NFrame.Player.ChessPlane.ThisName, OnChessPlaneChange);

        mEventModule.RegisterCallback((int)NFLoginModule.Event.UpdatePlayerOneBonusUI, updateBonusUI);

    }

    void updateBonusUI(NFDataList valueList)
    {
        NFGameSceneUI gameUI = uiModule.GetUI<NFGameSceneUI>();
        gameUI.updateBunusPanel(bonusData);
    }

    void OnBattleMapChange(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        NFGUID empty = new NFGUID(0, 0);
        NFGUID id = newVar.ObjectVal();
        if(id != empty)
        {
            GameObject chess = mSceneModule.GetObject(id);
            ChessController controller = chess.GetComponent<ChessController>();
            controller.gridTargetPosition = mapGridPositions[nRow,nCol];
        }
    }

    void OnChessPlaneChange(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        caculateBonus();
        mEventModule.DoEvent((int)NFLoginModule.Event.UpdatePlayerOneBonusUI);
    }


    void OnOwnInventoryChange(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        NFIRecord record = mKernelModule.FindRecord((NFGUID)mSceneModule.playerList[0], strRecordName);
        if(eType == NFIRecord.ERecordOptype.Add && record.IsUsed(nRow))
        {
                NFGUID npcID = record.QueryObject(nRow, 0);
                GameObject npcPrefab = mSceneModule.GetObject(npcID);
                if (npcPrefab == null) Debug.Log("npc object is null");
                Debug.Log(ownInventoryGridPositions[nRow]);
                npcPrefab.transform.position = ownInventoryGridPositions[nRow];
        }
    }

    private void refreshMap(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {

        NFIRecord mapRecord = mKernelModule.FindRecord(self, Player.ChessPlane.ThisName);
        NFIRecord battleMapRecord = mKernelModule.FindRecord(new NFGUID(0,0), Group.ChessPlane1.ThisName);

        if (battleMapRecord == null) Debug.LogError("refreshMap battleMapRecord is null ");

        NFGUID flag = new NFGUID(0, 0);

        for (int row = 0; row < 4; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                NFGUID indent = null;
                if (GameMain.Instance().currentGameStage == GameStage.Preparation)
                {
                     indent = mapRecord.QueryObject(row, col);
                }
                else
                {
                     indent = battleMapRecord.QueryObject(row, col);
                }
                if (indent != flag)
                {
                    GameObject gameObject = mSceneModule.GetObject(indent);
                    gameObject.transform.position = mapGridPositions[row, col];
                }
            }
        }

    }
    void caculateBonus()
    {
        bonusData.Clear();
        NFIRecord mapRecord = mKernelModule.FindRecord((NFGUID)mSceneModule.playerList[0], Player.ChessPlane.ThisName);
        NFGUID empty = new NFGUID(0, 0);
        for(int i = 0; i < 4; i++)
        {
            for(int j = 0; j < 7; j++)
            {
                NFGUID id = mapRecord.QueryObject(i, j);
                if (id != empty)
                {
                    string element = mKernelModule.QueryPropertyString(id, NPC.ElementType);
                    string race = mKernelModule.QueryPropertyString(id, NPC.RaceType);
                    if (bonusData.ContainsKey(element))
                    {
                        bonusData[element]++;
                    }
                    else
                    {
                        bonusData.Add(element, 1);
                    }

                    if (bonusData.ContainsKey(race))
                    {
                        bonusData[race]++;
                    }
                    else
                    {
                        bonusData.Add(race, 1);
                    }
                }
            }
        }
    }
}
