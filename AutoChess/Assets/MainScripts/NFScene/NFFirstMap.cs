using System.Collections.Generic;
using UnityEngine;
using NFSDK;
using NFrame;


public class NFFirstMap : ChessPlane
{
    
    public Dictionary<string,int> bonusData = new Dictionary<string, int>();

    private void Start()
    {

        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, onGameStateChange);
        mEventModule.RegisterCallback((int)NFLoginModule.Event.UpdatePlayerOneBonusUI, updateBonusUI);
    }

    public override void Init()
    {
        mKernelModule.RegisterRecordCallback(mLoginModule.mRoleID, NFrame.Player.ChessPlane.ThisName, OnChessPlaneChange);
        mKernelModule.RegisterRecordCallback(playerID, NFrame.Player.ownInventory.ThisName, OnOwnInventoryChange);
        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, RecoverChessPlane);
    }

    void updateBonusUI(NFDataList valueList)
    {
        NFGameSceneUI gameUI = mUIModule.GetUI<NFGameSceneUI>();
        gameUI.updateBunusPanel(bonusData);
    }

    void OnChessPlaneChange(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        caculateBonus(self);
        mEventModule.DoEvent((int)NFLoginModule.Event.UpdatePlayerOneBonusUI);
    }


    private void onGameStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long gameSate = newVar.IntVal();
        NFIRecord battlePlane = mKernelModule.FindRecord(self, NFrame.Group.ChessPlane1.ThisName);

        if (gameSate == 1)
        {
            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 8; row++)
                {
                    NFGUID indent = battlePlane.QueryObject(col, row);
                    if (indent != NFGUID.Zero)
                    {
                        GameObject chessObject = mSceneModule.GetObject(indent);
                        chessObject.transform.position = mapGridPositions[col, row];
                        ChessController controller = chessObject.GetComponent<ChessController>();
                        controller.gridTargetPosition = mapGridPositions[col, row];
                        if (row >= 4) chessObject.transform.rotation = Quaternion.Euler(Vector3.up);
                    }
                }
            }
        }
    
    }

    void caculateBonus(NFGUID self)
    {
        bonusData.Clear();
        NFIRecord mapRecord = mKernelModule.FindRecord(self, Player.ChessPlane.ThisName);
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

    public void OnOwnInventoryChange(NFGUID self, string strRecordName, NFIRecord.ERecordOptype eType, int nRow, int nCol, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        NFIRecord record = mKernelModule.FindRecord(self, strRecordName);
        if (eType == NFIRecord.ERecordOptype.Add && record.IsUsed(nRow))
        {
            NFGUID npcID = record.QueryObject(nRow, 0);
            GameObject npcPrefab = mSceneModule.GetObject(npcID);
            npcPrefab.transform.position = ownInventoryGridPositions[nRow];
        }
    }

    public void RecoverChessPlane(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        //long oldGameState = oldVar.IntVal();
        long gameSate = newVar.IntVal();
        if (gameSate == 0 )
        {

            NFIRecord battlePlane = mKernelModule.FindRecord(playerID, NFrame.Player.ChessPlane.ThisName);

            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    NFGUID indent = battlePlane.QueryObject(col, row);
                    if (indent != NFGUID.Zero)
                    {
                        GameObject chessObject = mSceneModule.GetObject(indent);
                        chessObject.SetActive(true);
                        chessObject.transform.position = mapGridPositions[col, row];
                        chessObject.transform.rotation = Quaternion.Euler(Vector3.up*180); 
                    }
                }
            }
        }
    }
}
