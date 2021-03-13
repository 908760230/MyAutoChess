using UnityEngine;
using NFSDK;
using NFrame;


public class NFSecondMap : ChessPlane
{

    private void Start()
    {
        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, onGameStateChange);

    }

    public override void Init()
    {
        mKernelModule.RegisterRecordCallback(playerID, NFrame.Player.ownInventory.ThisName, OnOwnInventoryChange);
        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, RecoverChessPlane);
    }

    private void onGameStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long gameSate = newVar.IntVal();
        NFIRecord battlePlane = mKernelModule.FindRecord(self, NFrame.Group.ChessPlane1.ThisName);
        
        for (int col = 0; col < 7; col++)
        {
            for (int row = 0; row < 8; row++)
            {
                NFGUID indent = battlePlane.QueryObject(col, row);
                if (indent != NFGUID.Zero)
                {
                    NFGUID clonedID = mKernelModule.QueryPropertyObject(indent, NFrame.Player.Mirror);
                    GameObject clonedNPC = mSceneModule.GetObject(clonedID);

                    if (gameSate == 1)
                    {
                        clonedNPC.SetActive(true);
                        int index = 7 - row;
                        clonedNPC.transform.position = mapGridPositions[col, index];
                        ChessController controller = clonedNPC.GetComponent<ChessController>();
                        controller.gridTargetPosition = mapGridPositions[col, index];
                        if (index >= 4) clonedNPC.transform.rotation = Quaternion.Euler(Vector3.up);
                    }
                    else clonedNPC.SetActive(false);
                    
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
        long gameSate = newVar.IntVal();
        if (gameSate == 0)
        {
            Debug.Log("recover second map player id :" + playerID.ToString());

            NFIRecord battlePlane = mKernelModule.FindRecord(playerID, NFrame.Player.ChessPlane.ThisName);

            for (int col = 0; col < 7; col++)
            {
                for (int row = 0; row < 4; row++)
                {
                    NFGUID indent = battlePlane.QueryObject(col, row);
                    if (indent != NFGUID.Zero)
                    {
                        GameObject chessObject = mSceneModule.GetObject(indent);
                        chessObject.transform.position = mapGridPositions[col,row];
                        ChessController controller = chessObject.GetComponent<ChessController>();
                        controller.gridTargetPosition = mapGridPositions[col, row];
                        chessObject.transform.rotation = Quaternion.Euler(Vector3.up*180);
                    }
                }
            }
        }
    }
}
