﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using NFSDK;
using NFrame;

public class ChessController : MonoBehaviour
{
    public GameObject levelupEffectPrefab;
    public GameObject projectileStart;
    public NFGUID id;

    [HideInInspector]
    public int gridType = 0;
    [HideInInspector]
    public int gridPositionX = 0;
    [HideInInspector]
    public int gridPositionZ = 0;

    [HideInInspector]
    public Champion champion;

    private ChampionAnimation championAnimation;
    private WorldCanvasController worldCanvasController;
    private GameObject hpBar;

    public Vector3 gridTargetPosition;

    private bool _isDragged = false;

    [HideInInspector]
    public bool isAttacking = false;

    [HideInInspector]
    public bool isDead = false;

    private bool isInCombat = false;
    private float combatTimer = 0;

    private bool isStuned = false;
    private float stunTimer = 0;

    private List<Effect> effects;

    private GameObject target;

    public bool IsDragged
    {
        get { return _isDragged; }
        set { _isDragged = value; }
    }

    private ChessPlane chessPlane;

    NFSceneModule sceneModule;
    NFIKernelModule mKernelModule;

    private void Awake()
    {
        GameObject perfab = Resources.Load<GameObject>("Prefabs/UI/HPCanvas");
        hpBar = Instantiate(perfab);
        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        hpBar.transform.SetParent(transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        NFIPluginManager xPluginManager = GameMain.Instance().GetPluginManager();
        sceneModule = xPluginManager.FindModule<NFSceneModule>();
        mKernelModule = xPluginManager.FindModule<NFIKernelModule>();

        NFGUID masterID = mKernelModule.FindProperty(id, NFrame.NPC.MasterID).QueryObject();
        chessPlane = sceneModule.chessPlaneDict[masterID];
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDragged)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 射线距离
            float enter = 100.0f;

            if (chessPlane.mPlane.Raycast(ray, out enter))
            {
                // 获取点击的位置
                Vector3 hitPoint = ray.GetPoint(enter);

                // 角色的位置
                Vector3 p = new Vector3(hitPoint.x, 1.0f, hitPoint.z);

                // 线性插值
                this.transform.position = Vector3.Lerp(this.transform.position, p, 0.1f);
            }

        }
        else
        {
            if (GameMain.Instance().currentGameStage == GameStage.Preparation && gridTargetPosition != Vector3.zero)
            {
                float distance = Vector3.Distance(gridTargetPosition, this.transform.position);
                if (distance > 0.25f)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, gridTargetPosition, 0.1f);
                }
                else this.transform.position = gridTargetPosition;
            }
        }
        /*
        if (isInCombat && isStuned == false)
        {
            if (target == null)
            {   // 旧目标 已死亡 寻找新的目标
                combatTimer += Time.deltaTime;
                if (combatTimer > 0.5f)
                {
                    combatTimer = 0;
                    //TryAttackNewTarget();
                }
            }
            else
            {
                // 战斗状态

                // 朝向 目标
                this.transform.LookAt(target.transform, Vector3.up);

                if (target.GetComponent<ChampionController>().isDead == true)
                {
                    target = null;
                    //navMeshAgent.isStopped = true;
                }
                else
                {
                    if (isAttacking == false)
                    {
                        float distance = Vector3.Distance(this.transform.position, target.transform.position);
                        if (distance < champion.attackRange)
                        {
                            //DoAttack();
                        }
                        else
                        {
                            //navMeshAgent.destination = target.transform.position;
                        }
                    }
                }
            }
        }*/
        updateHPBarPosition();
    }

    public void SetGridPosition(int gridType, int gridX, int gridZ, Vector3 pos)
    {
        this.gridType = gridType;
        this.gridPositionX = gridX;
        this.gridPositionZ = gridZ;

        gridTargetPosition = pos;
    }

    void updateHPBarPosition()
    {
        hpBar.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        Debug.Log("hp bar " + hpBar.transform.position.ToString());
    }
}