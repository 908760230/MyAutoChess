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


    // ������������
    public static int GRID_TYPE_OWN_INVENTORY = 0;
    public static int GRID_TYPE_OPENENT_INVENTORY = 1;
    public static int GRID_TYPE_HEXA_MAP = 2;

    public static int hexMapSizeX = 7;
    public static int hexMapSizeZ = 8;
    public static int invetorysize = 9;

    public Plane mPlane;

    // ��ʼλ��
    public Transform ownInventoryStartPosition;
    public Transform oponentInventoryStartPosition;
    public Transform mapStartPosition;

    // ָʾ��
    public GameObject squareIndicator;
    public GameObject hexaIndicator;
    // ָʾ����ɫ
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
        // 9 �� ��ά����   �����ѹ���Ӣ�۵� Ӣ�������ܹ�9������ 
        ownInventoryGridPositions = new Vector3[invetorysize];
        oponentInventoryGridPositions = new Vector3[invetorysize];
        // 7x8����ά����   ��ͼ�ĸ�������  һ��7������  ���� ��4��
        mapGridPositions = new Vector3[hexMapSizeX, hexMapSizeZ];

        // ��������� ��Ʒ��inventory grid����λ��
        for(int i=0;i<invetorysize;i++)
        {   //  ÿ�����ӵ�ƫ������ 2.5
            float offsetX = i * -2.5f;
            //  �����������9�� Ӣ�����ϵ�λ��
            Vector3 position = GetMapHitPoint(ownInventoryStartPosition.position + new Vector3(offsetX, 0, 0));
            ownInventoryGridPositions[i] = position;
        }
        // �������ֵ� Ӣ����λ��
        for (int i = 0; i < invetorysize; i++)
        {
            //  ÿ�����ӵ�ƫ������ 2.5
            float offsetX = i * -2.5f;
            //  �����������9�� Ӣ�����ϵ�λ��
            Vector3 position = GetMapHitPoint(oponentInventoryStartPosition.position + new Vector3(offsetX, 0, 0));
            oponentInventoryGridPositions[i] = position;
        }
        // ������ͼ�� λ��
        for(int x =0;x < hexMapSizeX; x++)
        {
            for(int z = 0; z < hexMapSizeZ; z++)
            {
                int rowOffset = z % 2;
                //  ��������Ҫһ���Ķ���ƫ��
                float offsetX = x * -3f + rowOffset * 1.5f;
                float offsetZ = z * -2.5f;
                Vector3 position = GetMapHitPoint(mapStartPosition.position + new Vector3(offsetX, 0, offsetZ));
                mapGridPositions[x, z] = position;

            }
        }
    }
    // ��������ָʾ��������
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

    // ������ͼ�� ������ָʾ��
    protected void CreateIndicators()
    {   // ����ָʾ��������
        indicatorContainer = new GameObject();
        indicatorContainer.name = "IndicatorContainer";
        indicatorContainer.transform.SetParent(GameMain.Instance().transform);

        //  Ϊ���д�������������
        GameObject triggerContainer = new GameObject();
        triggerContainer.name = "TriggerContainer";
        triggerContainer.transform.SetParent(GameMain.Instance().transform);

        //�洢ָʾ���� ����
        ownIndicatorArray = new GameObject[invetorysize];
        oponentIndicatorArray = new GameObject[invetorysize];
        mapIndicatorArray = new GameObject[hexMapSizeX, hexMapSizeZ];

        ownTriggerArray = new TriggerInfo[invetorysize];
        mapGridTriggerArray = new TriggerInfo[hexMapSizeX, hexMapSizeZ];

        // Ϊ�ҷ�Ӣ������������ָʾ�� �ʹ�����
        for(int i = 0; i < invetorysize; i++)
        {   // ��������ָʾ��ʵ��
            GameObject indicatorGO = Instantiate(squareIndicator);
            // �� Ӣ������λ�ø���ָʾ��
            indicatorGO.transform.position = ownInventoryGridPositions[i];
            indicatorGO.transform.parent = indicatorContainer.transform;
            ownIndicatorArray[i] = indicatorGO;

            // ����������
            GameObject trigger = CreateBoxTrigger(GRID_TYPE_OWN_INVENTORY, i);
            trigger.transform.parent = triggerContainer.transform;
            trigger.transform.position = ownInventoryGridPositions[i];

            ownTriggerArray[i] = trigger.GetComponent<TriggerInfo>();
        }

        // Ϊ��ͼ����������ָʾ���ʹ�����
        for(int x = 0; x < hexMapSizeX; x++)
        {
            for(int z = 0; z < hexMapSizeZ; z++)
            {   //�½�һ�� ָʾ��
                GameObject indicatorGO = Instantiate(hexaIndicator);
                indicatorGO.transform.position = mapGridPositions[x, z];
                indicatorGO.transform.parent = indicatorContainer.transform;
                mapIndicatorArray[x, z] = indicatorGO;

                // ����������
                GameObject trigger = CreateSphereTrigger(GRID_TYPE_HEXA_MAP, x, z);
                trigger.transform.parent = triggerContainer.transform;
                trigger.transform.position = mapGridPositions[x, z];

                // �洢 triggerinfo
                mapGridTriggerArray[x, z] = trigger.GetComponent<TriggerInfo>();
            }
        }
    }
    Vector3 GetMapHitPoint(Vector3 p)
    {
        Vector3 newPos = p;
        //�洢������ײ���ĵ�һ���������Ϣ
        RaycastHit hit;
        // ������ʼ�������ƶ� 10�� �������£����ߵ���󳤶�Ϊ15
        if (Physics.Raycast(newPos + new Vector3(0, 10, 0), Vector3.down, out hit, 15))
        {
            newPos = hit.point; // ������ײ���λ��
        }

        return newPos;
    }

    GameObject CreateBoxTrigger(int type,int x)
    {
        GameObject trigger = new GameObject();
        BoxCollider collider = trigger.AddComponent<BoxCollider>();

        //������ײ��� ��С
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
        // ���� ������ײ��
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
