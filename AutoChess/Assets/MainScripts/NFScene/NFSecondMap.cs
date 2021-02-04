using UnityEngine;
using NFSDK;
using NFrame;


public class NFSecondMap : ChessPlane
{

    private void Start()
    {
        mKernelModule.RegisterGroupPropertyCallback(NFrame.Group.GameState, onGameStateChange);

    }

    private void onGameStateChange(NFGUID self, string strProperty, NFDataList.TData oldVar, NFDataList.TData newVar)
    {
        long gameSate = newVar.IntVal();
        NFIRecord battlePlane = mKernelModule.FindRecord(self, NFrame.Group.ChessPlane1.ThisName);
        NFGUID empty = new NFGUID(0, 0);
        
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                NFGUID indent = battlePlane.QueryObject(i, j);
                if (indent != empty)
                {
                    
                    NFGUID clonedID = mKernelModule.QueryPropertyObject(indent, NFrame.Player.Mirror);
                    GameObject gameObject = mSceneModule.GetObject(clonedID);
                    if (gameSate == 1)
                    {
                        gameObject.SetActive(true);
                        gameObject.transform.position = mapGridPositions[i, j];
                    }else gameObject.SetActive(false);

                }
            }
        }
    }
}
