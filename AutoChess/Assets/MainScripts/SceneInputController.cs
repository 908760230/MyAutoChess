using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NFrame;
using NFSDK;


public class SceneInputController : MonoBehaviour
{

    //map script
    public ChessPlane chessPlane;

    public LayerMask triggerLayer;
    private GameObject draggedChampion = null;
    private TriggerInfo dragStartTrigger = null;

    private Vector3 rayCastStartPosition;
    private Vector3 mousePosition;
    private NFSceneModule mSceneModule;
    private NFLoginModule mLoginModule;
    private NFIKernelModule mKernelModule;

    [HideInInspector]
    public TriggerInfo triggerInfo = null;

    // Start is called before the first frame update
    void Start()
    {
        NFIPluginManager xmanager = GameMain.Instance().GetPluginManager();
        mSceneModule = xmanager.FindModule<NFSceneModule>();
        mLoginModule = xmanager.FindModule<NFLoginModule>();
        mKernelModule = xmanager.FindModule<NFIKernelModule>();

        rayCastStartPosition = new Vector3(0, 20, 0);
        chessPlane = mSceneModule.chessPlaneDict[mLoginModule.mRoleID];
    }

    // Update is called once per frame
    void Update()
    {
        triggerInfo = null;
        if (chessPlane == null) Debug.Log("input controller chess plane is null");
        chessPlane.resetIndicators();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // 检测射线是否击中物体
        if (Physics.Raycast(ray, out hit, 100f, triggerLayer, QueryTriggerInteraction.Collide))
        {
            triggerInfo = hit.collider.gameObject.GetComponent<TriggerInfo>();

            if (triggerInfo != null)
            {
                GameObject indicator = chessPlane.GetIndicatorFromTriggerInfo(triggerInfo);
                indicator.GetComponent<MeshRenderer>().material.color = chessPlane.indicatorActiveColor;
            }
            else chessPlane.resetIndicators();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartDrag();
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopDrag();
        }

        //store mouse position
        mousePosition = Input.mousePosition;
    }

    private void StartDrag()
    {
        //Debug.Log("Drag ---------------");
        if (GameMain.Instance().currentGameStage  != GameStage.Preparation) return;

        if (triggerInfo != null)
        {
            dragStartTrigger = triggerInfo;
            GameObject championGO = chessPlane.GetChampionFromTriggerInfo(triggerInfo);
            if (championGO != null)
            {
                chessPlane.ShowIndicators();
                draggedChampion = championGO;
                championGO.GetComponent<ChessController>().IsDragged = true;
            }
        }
    }

    public void StopDrag()
    {
        chessPlane.HideIndicators();

        if (draggedChampion != null)
        {
            ChessController draggedChampionController = draggedChampion.GetComponent<ChessController>();
            draggedChampionController.IsDragged = false;

            if (triggerInfo != null)
            {
                if (triggerInfo == dragStartTrigger)
                {
                    if(triggerInfo.gridType == ChessPlane.GRID_TYPE_OWN_INVENTORY)
                    {
                        draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];
                    }
                    else if (triggerInfo.gridType == ChessPlane.GRID_TYPE_HEXA_MAP)
                    {
                        draggedChampionController.gridTargetPosition = chessPlane.mapGridPositions[triggerInfo.gridX,triggerInfo.gridZ];
                    }
                }
                else
                {
                    if (dragStartTrigger.gridType == ChessPlane.GRID_TYPE_OWN_INVENTORY && triggerInfo.gridType == ChessPlane.GRID_TYPE_OWN_INVENTORY)
                    {
                        NFIRecord record = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ownInventory.ThisName);
                        NFGUID npcID = record.QueryObject(triggerInfo.gridX, 0);
                        GameObject championGO = mSceneModule.GetObject(npcID);

                        if (championGO != null)
                        {
                            ChessController championGOController = championGO.GetComponent<ChessController>();
                            // 交换 位置
                            championGOController.gridTargetPosition = chessPlane.ownInventoryGridPositions[dragStartTrigger.gridX];
                            draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];

                            // 交换record的id
                            record.SetObject(triggerInfo.gridX, 0, record.QueryObject(dragStartTrigger.gridX, 0));
                            record.SetObject(dragStartTrigger.gridX, 0, npcID);
                        }
                        else
                        {
                            draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];
                            record.Remove(dragStartTrigger.gridX);
                            NFDataList data = new NFDataList();
                            data.AddObject(draggedChampion.GetComponent<ChessController>().id);
                            record.AddRow(triggerInfo.gridX, data);
                        }
                    }
                    else if (dragStartTrigger.gridType == ChessPlane.GRID_TYPE_OWN_INVENTORY && triggerInfo.gridType == ChessPlane.GRID_TYPE_HEXA_MAP)
                    {

                        long heroCount = mKernelModule.FindProperty(mLoginModule.mRoleID, Player.HeroCount).GetData().IntVal();
                        long maxHeroCount = mKernelModule.FindProperty(mLoginModule.mRoleID, Player.MaxHero).GetData().IntVal();
                        if(heroCount >= maxHeroCount)
                        {
                            draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];
                            draggedChampion = null;
                            return;
                        }

                        NFIRecord ownInventory = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ownInventory.ThisName);
                        NFIRecord chessPlaneMap = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ChessPlane.ThisName);

                        NFGUID npcID = chessPlaneMap.QueryObject(triggerInfo.gridX, triggerInfo.gridZ);
                        GameObject championGO = mSceneModule.GetObject(npcID);

                        if (championGO != null)
                        {
                            // 交换 位置
                            ChessController championGOController = championGO.GetComponent<ChessController>();
                            championGOController.gridTargetPosition = chessPlane.ownInventoryGridPositions[dragStartTrigger.gridX];
                            draggedChampionController.gridTargetPosition = chessPlane.mapGridPositions[triggerInfo.gridX,triggerInfo.gridZ];

                            // 交换record的id
                            chessPlaneMap.SetObject(triggerInfo.gridX, triggerInfo.gridZ, ownInventory.QueryObject(dragStartTrigger.gridX, 0));
                            ownInventory.SetObject(dragStartTrigger.gridX, 0, npcID);
                        }
                        else
                        {
                            draggedChampionController.gridTargetPosition = chessPlane.mapGridPositions[triggerInfo.gridX, triggerInfo.gridZ];

                            ownInventory.Remove(dragStartTrigger.gridX);
                            chessPlaneMap.SetObject(triggerInfo.gridX, triggerInfo.gridZ, draggedChampionController.id);
                        }
                    }
                    else if (dragStartTrigger.gridType == ChessPlane.GRID_TYPE_HEXA_MAP && triggerInfo.gridType == ChessPlane.GRID_TYPE_HEXA_MAP)
                    {
                        NFIRecord chessPlaneMap = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ChessPlane.ThisName);

                        NFGUID npcID = chessPlaneMap.QueryObject(triggerInfo.gridX, triggerInfo.gridZ);
                        GameObject championGO = mSceneModule.GetObject(npcID);

                        if (championGO != null)
                        {
                            // 交换 位置
                            ChessController championGOController = championGO.GetComponent<ChessController>();
                            championGOController.gridTargetPosition = chessPlane.mapGridPositions[dragStartTrigger.gridX, dragStartTrigger.gridZ];
                            draggedChampionController.gridTargetPosition = chessPlane.mapGridPositions[triggerInfo.gridX, triggerInfo.gridZ];

                            // 交换record的id
                            chessPlaneMap.SetObject(triggerInfo.gridX, 0, chessPlaneMap.QueryObject(dragStartTrigger.gridX, 0));
                            chessPlaneMap.SetObject(triggerInfo.gridX, 0, npcID);
                        }
                        else
                        {
                            draggedChampionController.gridTargetPosition = chessPlane.mapGridPositions[triggerInfo.gridX, triggerInfo.gridZ];
                            chessPlaneMap.SetObject(dragStartTrigger.gridX, dragStartTrigger.gridZ, new NFGUID(0, 0));
                            chessPlaneMap.SetObject(triggerInfo.gridX, triggerInfo.gridZ, draggedChampionController.id);
                        }
                    }
                    else if (dragStartTrigger.gridType == ChessPlane.GRID_TYPE_HEXA_MAP && triggerInfo.gridType == ChessPlane.GRID_TYPE_OWN_INVENTORY)
                    {
                        NFIRecord chessPlaneMap = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ChessPlane.ThisName);
                        NFIRecord ownInventory = mKernelModule.FindRecord(mLoginModule.mRoleID, Player.ownInventory.ThisName);

                        NFGUID npcID = ownInventory.QueryObject(triggerInfo.gridX, triggerInfo.gridZ);
                        GameObject championGO = mSceneModule.GetObject(npcID);

                        if (championGO != null)
                        {
                            // 交换 位置
                            ChessController championGOController = championGO.GetComponent<ChessController>();
                            championGOController.gridTargetPosition = chessPlane.mapGridPositions[dragStartTrigger.gridX, dragStartTrigger.gridZ];
                            draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];

                            // 交换record的id
                            ownInventory.SetObject(triggerInfo.gridX, 0, chessPlaneMap.QueryObject(dragStartTrigger.gridX, 0));
                            chessPlaneMap.SetObject(dragStartTrigger.gridX, dragStartTrigger.gridZ, npcID);
                        }
                        else
                        {
                            draggedChampionController.gridTargetPosition = chessPlane.ownInventoryGridPositions[triggerInfo.gridX];
                            chessPlaneMap.SetObject(dragStartTrigger.gridX, dragStartTrigger.gridZ, new NFGUID(0, 0));
                            NFDataList data = new NFDataList();
                            data.AddObject(draggedChampionController.id);
                            ownInventory.AddRow(triggerInfo.gridX, data);
                        }
                    }
                }
            }            
            draggedChampion = null;
        }
    }
}

