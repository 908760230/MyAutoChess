using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;

public class ChampionController : MonoBehaviour
{
    public static int TEAMID_PLAYER = 0;
    public static int TEAMID_AI = 1;


    public GameObject levelupEffectPrefab;
    public GameObject projectileStart;

    [HideInInspector]
    public int gridType = 0;
    [HideInInspector]
    public int gridPositionX = 0;
    [HideInInspector]
    public int gridPositionZ = 0;

    [HideInInspector]
    ///Team of this champion, can be player = 0, or enemy = 1
    public int teamID = 0;


    [HideInInspector]
    public Champion champion;

    [HideInInspector]
    ///Maximum health of the champion
    public float maxHealth = 0;

    [HideInInspector]
    ///current health of the champion 
    public float currentHealth = 0;

    [HideInInspector]
    ///Current damage of the champion deals with a attack
    public float currentDamage = 0;

    [HideInInspector]
    ///The upgrade level of the champion
    public int lvl = 1;

    private Map map;
    private GamePlayController gamePlayController;
    private AIOpponent aIopponent;
    private ChampionAnimation championAnimation;
    private WorldCanvasController worldCanvasController;

    private NavMeshAgent navMeshAgent;

    private Vector3 gridTargetPosition;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDragged)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // 射线距离
            float enter = 100.0f;

            if (map.mPlane.Raycast(ray, out enter))
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
            if (gamePlayController.currentGameStage == GameStage.Preparation)
            {
                float distance = Vector3.Distance(gridTargetPosition, this.transform.position);
                if (distance > 0.25f)
                {
                    this.transform.position = Vector3.Lerp(this.transform.position, gridTargetPosition, 0.1f);
                }
                else this.transform.position = gridTargetPosition;
            }
        }

        if(isInCombat && isStuned == false)
        {
            if(target == null)
            {   // 旧目标 已死亡 寻找新的目标
                combatTimer += Time.deltaTime;
                if (combatTimer > 0.5f)
                {
                    combatTimer = 0;
                    TryAttackNewTarget();
                }
            }
            else
            {
                // 战斗状态

                // 朝向 目标
                this.transform.LookAt(target.transform, Vector3.up);

                if(target.GetComponent<ChampionController>().isDead == true)
                {
                    target = null;
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    if(isAttacking == false)
                    {
                        float distance = Vector3.Distance(this.transform.position, target.transform.position);
                        if(distance < champion.attackRange)
                        {
                            DoAttack();
                        }else
                        {
                            navMeshAgent.destination = target.transform.position;
                        }
                    }
                }
            }
        }
    }
    public void OnCombatStart()
    {
        IsDragged = false;
        this.transform.position = gridTargetPosition;
        if(gridType == Map.GRID_TYPE_HEXA_MAP)
        {
            isInCombat = true;
            navMeshAgent.enabled = true;
            TryAttackNewTarget();
        }
    }
    private void TryAttackNewTarget()
    {
        target = FindTarget();
        if(target != null)
        {
            navMeshAgent.destination = target.transform.position;
            navMeshAgent.isStopped = false;
        }
    }
    private GameObject FindTarget()
    {
        GameObject closestEnemy = null;
        float bestDistance = 1000;

        if (teamID == TEAMID_PLAYER)
        {
            for (int x = 0; x < Map.hexMapSizeX; x++)
            {
                for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
                {
                    if (aIopponent.gridChampionsArray[x, z] != null)
                    {
                        ChampionController championController = aIopponent.gridChampionsArray[x, z].GetComponent<ChampionController>();
                        if (championController.isDead == false)
                        {
                            float distance = Vector3.Distance(this.transform.position, aIopponent.gridChampionsArray[x, z].transform.position);
                            if (distance < bestDistance)
                            {
                                bestDistance = distance;
                                closestEnemy = aIopponent.gridChampionsArray[x, z];
                            }
                        }
                    }
                }
            }
        }
        else if (teamID == TEAMID_AI)
        {
            for (int x = 0; x < Map.hexMapSizeX; x++)
            {
                for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
                {
                    if (gamePlayController.gridChampionsArray[x, z] != null)
                    {
                        ChampionController championController = gamePlayController.gridChampionsArray[x, z].GetComponent<ChampionController>();
                        if(championController.isDead == false)
                        {
                            //calculate distance
                            float distance = Vector3.Distance(this.transform.position, gamePlayController.gridChampionsArray[x, z].transform.position);

                            //if new this champion is closer then best distance
                            if (distance < bestDistance)
                            {
                                bestDistance = distance;
                                closestEnemy = gamePlayController.gridChampionsArray[x, z];
                            }
                        }
                    }
                }
            }
        }
        return closestEnemy;
    }

    public void Reset()
    {
        //set active
        this.gameObject.SetActive(true);

        //reset stats
        //maxHealth = champion.health * lvl;
        //currentHealth = champion.health * lvl;
        isDead = false;
        isInCombat = false;
        target = null;
        isAttacking = false;

        //reset position
        SetWorldPosition();
        SetWorldRotation();

        //remove all effects
        foreach (Effect e in effects)
        {
            e.Remove();
        }

        effects = new List<Effect>();
    }
    public void RemoveEffect(Effect effect)
    {
        effects.Remove(effect);
        effect.Remove();
    }

    public void SetWorldPosition()
    {
        navMeshAgent.enabled = false;

        Vector3 worldPosition = GetWorldPosition();
        this.transform.position = worldPosition;
        //gridTargetPosition = worldPosition;
    }
    public void SetWorldRotation()
    {
        Vector3 rotation = Vector3.zero;
        if (teamID == 0) rotation = new Vector3(0, 200, 0);
        else if (teamID == 1) rotation = new Vector3(0, 20, 0);
        this.transform.rotation = Quaternion.Euler(rotation);
    }
    Vector3 GetWorldPosition()
    {
        Vector3 worldPosition = Vector3.zero;
        if(gridType == Map.GRID_TYPE_OWN_INVENTORY)
        {
            worldPosition = map.ownInventoryGridPositions[gridPositionX];
        }else if (gridType == Map.GRID_TYPE_HEXA_MAP)
        {
            worldPosition = map.mapGridPositions[gridPositionX, gridPositionZ];
        }
        return worldPosition;
    }

    public void UpgradeLevel()
    {
        //incrase lvl
        lvl++;

        float newSize = 1;
        maxHealth = champion.health;
        currentHealth = champion.health;


        if (lvl == 2)
        {
            newSize = 1.5f;
            maxHealth = champion.health * 2;
            currentHealth = champion.health * 2;
            currentDamage = champion.damage * 2;

        }

        if (lvl == 3)
        {
            newSize = 2f;
            maxHealth = champion.health * 3;
            currentHealth = champion.health * 3;
            currentDamage = champion.damage * 3;
        }



        //set size
        this.transform.localScale = new Vector3(newSize, newSize, newSize);

        //instantiate level up effect
        GameObject levelupEffect = Instantiate(levelupEffectPrefab);

        //set position
        levelupEffect.transform.position = this.transform.position;

        //destroy effect after finished
        Destroy(levelupEffect, 1.0f);

    }

    public void Init(Champion _champion, int _teamID)
    {
        champion = _champion;
        teamID = _teamID;

        //store scripts
        map = GameObject.Find("Scripts").GetComponent<Map>();
        aIopponent = GameObject.Find("Scripts").GetComponent<AIOpponent>();
        gamePlayController = GameObject.Find("Scripts").GetComponent<GamePlayController>();
        worldCanvasController = GameObject.Find("Scripts").GetComponent<WorldCanvasController>();
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        championAnimation = this.GetComponent<ChampionAnimation>();

        //disable agent
        navMeshAgent.enabled = false;

        //set stats
        maxHealth = champion.health;
        currentHealth = champion.health;
        currentDamage = champion.damage;

        worldCanvasController.AddHealthBar(this.gameObject);

        effects = new List<Effect>();
    }

    public void SetGridPosition(int _gridType, int _gridPositionX, int _gridPositionZ)
    {
        gridType = _gridType;
        gridPositionX = _gridPositionX;
        gridPositionZ = _gridPositionZ;


        //set new target when chaning grid position
        gridTargetPosition = GetWorldPosition();
    }
    private void DoAttack()
    {
        isAttacking = true;
        navMeshAgent.isStopped = true;
        championAnimation.DoAttack(true);
    }
    public void OnAttackAnimationFinished()
    {
        isAttacking = false;
        if(target != null)
        {
            ChampionController targetChampoion = target.GetComponent<ChampionController>();
            List<ChampionBonus> activeBonuses = null;

            if (teamID == TEAMID_PLAYER) activeBonuses = gamePlayController.activeBonusList;
            else if (teamID == TEAMID_AI)
                activeBonuses = aIopponent.activeBonusList;

            float d = 0;
            foreach(ChampionBonus b in activeBonuses)
            {
                d += b.ApplyOnAttack(this, targetChampoion);
            }

            bool isTargetDead = targetChampoion.OnGotHit(d + currentDamage);

            if (isTargetDead) TryAttackNewTarget();

            if(champion.attackProjectile !=null && projectileStart != null)
            {
                GameObject projectile = Instantiate(champion.attackProjectile);
                projectile.transform.position = this.transform.position;
                projectile.GetComponent<Projectile>().Init(target);

            }
        }
    }

    public void OnGotStun(float duration)
    {
        isStuned = true;
        stunTimer = duration;
        championAnimation.IsAnimated(false);
        navMeshAgent.isStopped = true;
    }

    public void OngGotHeal(float f)
    {
        currentHealth += f;
    }

    public void AddEffect(GameObject effectPrefab,float duration)
    {
        if (effectPrefab == null) return;

        bool foundEffect = false;
        foreach(Effect e in effects)
        {
            if(effectPrefab == e.effectPrefab)
            {
                e.duration = duration;
                foundEffect = true;
            }
        }

        if(foundEffect == false)
        {
            Effect effect = this.gameObject.AddComponent<Effect>();
            effect.Init(effectPrefab, this.gameObject, duration);
            effects.Add(effect);
        }
    }

    public bool OnGotHit(float damage)
    {
        List<ChampionBonus> activeBonuses = null;

        if (teamID == TEAMID_PLAYER) activeBonuses = gamePlayController.activeBonusList;
        else if (teamID == TEAMID_AI) activeBonuses = aIopponent.activeBonusList;

        foreach(ChampionBonus b in activeBonuses)
        {
            damage = b.ApplyOnGotHit(this, damage);
        }

        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            this.gameObject.SetActive(false);
            isDead = true;
            aIopponent.OnChampionDeath();
            gamePlayController.OnChampionDeadth();

            
        }
        worldCanvasController.AddDamageText(this.transform.position + new Vector3(0, 2.5f, 0), damage);

        return isDead;
    }
}
