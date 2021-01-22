using UnityEngine;
using NFSDK;
using NFrame;

public class ChessPlane : MonoBehaviour
{

    protected NFSceneModule mSceneModule;
    protected NFNetModule mNetModule;
    protected NFUIModule mUIModule;
    protected NFIEventModule mEventModule;
    protected NFIKernelModule mKernelModule;
    protected NFIElementModule mElementModule;


    // 声明方格类型
    public static int GRID_TYPE_OWN_INVENTORY = 0;
    public static int GRID_TYPE_OPENENT_INVENTORY = 1;
    public static int GRID_TYPE_HEXA_MAP = 2;

    public static int hexMapSizeX = 7;
    public static int hexMapSizeZ = 8;
    public static int invetorysize = 9;

    public Plane mPlane;

    // 起始位置
    public Transform ownInventoryStartPosition;
    public Transform oponentInventoryStartPosition;
    public Transform mapStartPosition;

    // 指示器
    public GameObject squareIndicator;
    public GameObject hexaIndicator;
    // 指示器颜色
    public Color indicatorDefaultColor;
    public Color indicatorActiveColor;

    private NFGUID playerID;
    public NFGUID PlayerID
    {
        get
        {
            return playerID;
        }
        set
        {
            playerID = value;
        }
    }

    [HideInInspector]
    public Vector3[] ownInventoryGridPositions;
    [HideInInspector]
    public Vector3[] oponentInventoryGridPositions;
    [HideInInspector]
    public Vector3[,] mapGridPositions;

    public void Awake()
    {
        CreateGripPosition();
        CreateIndicators();
        HideIndicators();

        mPlane = new Plane(Vector3.up, Vector3.zero);

        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        mNetModule = xPluginManager.FindModule<NFNetModule>();
        mUIModule = xPluginManager.FindModule<NFUIModule>();
        mEventModule = xPluginManager.FindModule<NFIEventModule>();

        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();
        mElementModule = xPluginManager.FindModule<NFIElementModule>();
        mSceneModule = xPluginManager.FindModule<NFSceneModule>();
    }

    protected void CreateGripPosition()
    {
        // 9 个 三维向量   放置已购买英雄的 英雄栏，总共9个格子 
        ownInventoryGridPositions = new Vector3[invetorysize];
        oponentInventoryGridPositions = new Vector3[invetorysize];
        // 7x8的三维向量   地图的格子总数  一行7个格子  敌我 各4行
        mapGridPositions = new Vector3[hexMapSizeX, hexMapSizeZ];

        // 创建自身的 物品（inventory grid）的位置
        for(int i=0;i<invetorysize;i++)
        {   //  每个格子的偏移量是 2.5
            float offsetX = i * -2.5f;
            //  保存鼠标点击在9个 英雄栏上的位置
            Vector3 position = GetMapHitPoint(ownInventoryStartPosition.position + new Vector3(offsetX, 0, 0));
            ownInventoryGridPositions[i] = position;
        }
        // 创建对手的 英雄栏位置
        for (int i = 0; i < invetorysize; i++)
        {
            //  每个格子的偏移量是 2.5
            float offsetX = i * -2.5f;
            //  保存鼠标点击在9个 英雄栏上的位置
            Vector3 position = GetMapHitPoint(oponentInventoryStartPosition.position + new Vector3(offsetX, 0, 0));
            oponentInventoryGridPositions[i] = position;
        }
        // 创建地图的 位置
        for(int x =0;x < hexMapSizeX; x++)
        {
            for(int z = 0; z < hexMapSizeZ; z++)
            {
                int rowOffset = z % 2;
                //  奇数行需要一定的额外偏移
                float offsetX = x * -3f + rowOffset * 1.5f;
                float offsetZ = z * -2.5f;
                Vector3 position = GetMapHitPoint(mapStartPosition.position + new Vector3(offsetX, 0, offsetZ));
                mapGridPositions[x, z] = position;

            }
        }
    }
    // 声明保存指示器的数组
    [HideInInspector]
    public GameObject[] ownIndicatorArray;
    [HideInInspector]
    public GameObject[] oponentIndicatorArray;
    [HideInInspector]
    public GameObject[,] mapIndicatorArray;

    [HideInInspector]
    public TriggerInfo[] ownTriggerArray;
    [HideInInspector]
    public TriggerInfo[,] mapGridTriggerArray;

    private GameObject indicatorContainer;

    // 创建地图的 六边形指示器
    protected void CreateIndicators()
    {   // 所有指示器的容器
        indicatorContainer = new GameObject();
        indicatorContainer.name = "IndicatorContainer";
        indicatorContainer.transform.SetParent(GameMain.Instance().transform);

        //  为所有触发器创建容器
        GameObject triggerContainer = new GameObject();
        triggerContainer.name = "TriggerContainer";
        triggerContainer.transform.SetParent(GameMain.Instance().transform);

        //存储指示器的 数组
        ownIndicatorArray = new GameObject[invetorysize];
        oponentIndicatorArray = new GameObject[invetorysize];
        mapIndicatorArray = new GameObject[hexMapSizeX, hexMapSizeZ];

        ownTriggerArray = new TriggerInfo[invetorysize];
        mapGridTriggerArray = new TriggerInfo[hexMapSizeX, hexMapSizeZ];

        // 为我方英雄栏创建方格指示器 和触发器
        for(int i = 0; i < invetorysize; i++)
        {   // 创建方格指示器实体
            GameObject indicatorGO = Instantiate(squareIndicator);
            // 将 英雄栏的位置赋给指示器
            indicatorGO.transform.position = ownInventoryGridPositions[i];
            indicatorGO.transform.parent = indicatorContainer.transform;
            ownIndicatorArray[i] = indicatorGO;

            // 创建触发器
            GameObject trigger = CreateBoxTrigger(GRID_TYPE_OWN_INVENTORY, i);
            trigger.transform.parent = triggerContainer.transform;
            trigger.transform.position = ownInventoryGridPositions[i];

            ownTriggerArray[i] = trigger.GetComponent<TriggerInfo>();
        }

        // 为地图创建六边形指示器和触发器
        for(int x = 0; x < hexMapSizeX; x++)
        {
            for(int z = 0; z < hexMapSizeZ; z++)
            {   //新建一份 指示器
                GameObject indicatorGO = Instantiate(hexaIndicator);
                indicatorGO.transform.position = mapGridPositions[x, z];
                indicatorGO.transform.parent = indicatorContainer.transform;
                mapIndicatorArray[x, z] = indicatorGO;

                // 创建触发器
                GameObject trigger = CreateSphereTrigger(GRID_TYPE_HEXA_MAP, x, z);
                trigger.transform.parent = triggerContainer.transform;
                trigger.transform.position = mapGridPositions[x, z];

                // 存储 triggerinfo
                mapGridTriggerArray[x, z] = trigger.GetComponent<TriggerInfo>();
            }
        }
    }
    Vector3 GetMapHitPoint(Vector3 p)
    {
        Vector3 newPos = p;
        //存储射线碰撞到的第一个物体的信息
        RaycastHit hit;
        // 射线起始点向上移动 10， 方向向下，射线的最大长度为15
        if (Physics.Raycast(newPos + new Vector3(0, 10, 0), Vector3.down, out hit, 15))
        {
            newPos = hit.point; // 保存碰撞点的位置
        }

        return newPos;
    }

    GameObject CreateBoxTrigger(int type,int x)
    {
        GameObject trigger = new GameObject();
        BoxCollider collider = trigger.AddComponent<BoxCollider>();

        //设置碰撞体的 大小
        collider.size = new Vector3(2, 0.5f, 2);
        collider.isTrigger = true;

        TriggerInfo triggerInfo = trigger.AddComponent<TriggerInfo>();
        triggerInfo.gridType = type;
        triggerInfo.gridX = x;

        trigger.layer = LayerMask.NameToLayer("Triggers");
        return trigger;
    }

    GameObject CreateSphereTrigger(int type,int x, int z)
    {
        GameObject trigger = new GameObject();
        // 设置 球形碰撞体
        SphereCollider collider = trigger.AddComponent<SphereCollider>();
        collider.radius = 1.4f;
        collider.isTrigger = true;

        TriggerInfo triggerInfo = trigger.AddComponent<TriggerInfo>();
        triggerInfo.gridType = type;
        triggerInfo.gridX = x;
        triggerInfo.gridZ = z;
        trigger.layer = LayerMask.NameToLayer("Triggers");

        return trigger;
    }

    public void HideIndicators()
    {
        indicatorContainer.SetActive(false);
    }
    public void resetIndicators()
    {
        for (int x = 0; x < hexMapSizeX; x++)
        {
            for (int z = 0; z < hexMapSizeZ; z++)
            {
                mapIndicatorArray[x, z].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
            }
        }

        for (int x = 0; x < invetorysize; x++)
        {
            ownIndicatorArray[x].GetComponent<MeshRenderer>().material.color = indicatorDefaultColor;
        }
    }

    public GameObject GetIndicatorFromTriggerInfo(TriggerInfo triggerinfo)
    {
        GameObject triggerGo = null;

        if (triggerinfo.gridType == GRID_TYPE_OWN_INVENTORY)
        {
            triggerGo = ownIndicatorArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == GRID_TYPE_OPENENT_INVENTORY)
        {
            triggerGo = oponentIndicatorArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == GRID_TYPE_HEXA_MAP)
        {
            triggerGo = mapIndicatorArray[triggerinfo.gridX, triggerinfo.gridZ];
        }

        return triggerGo;
    }
    public void ShowIndicators()
    {
        indicatorContainer.SetActive(true);
    }

    public GameObject GetChampionFromTriggerInfo(TriggerInfo triggerinfo)
    {
        GameObject championGO = null;

        if (triggerinfo.gridType == GRID_TYPE_OWN_INVENTORY)
        {
            NFIRecord record = mKernelModule.FindRecord(playerID, Player.ownInventory.ThisName);
            NFGUID npcID = record.QueryObject(triggerinfo.gridX, 0);
            championGO = mSceneModule.GetObject(npcID);
        }
        else if (triggerinfo.gridType == GRID_TYPE_OPENENT_INVENTORY)
        {
            //championGO = oponentChampionInventoryArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == GRID_TYPE_HEXA_MAP)
        {
            NFIRecord record = mKernelModule.FindRecord(playerID, Player.ChessPlane.ThisName);
            NFGUID npcID = record.QueryObject(triggerinfo.gridX, triggerinfo.gridZ);
            championGO = mSceneModule.GetObject(npcID);
        }

        return championGO;
    }

    public Vector3 GetPosition(int gridType, int gridX, int gridZ)
    {
        Vector3 pos = Vector3.zero;
        if (gridType == GRID_TYPE_OWN_INVENTORY)
        {
            pos = ownInventoryGridPositions[gridX];
        }
        else if (gridType == GRID_TYPE_HEXA_MAP)
        {
            pos = mapGridPositions[gridX, gridZ];
        }
        return pos;
    }
    
}
