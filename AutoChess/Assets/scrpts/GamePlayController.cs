using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameStage { Preparation, Combat, Loss };

public class GamePlayController : MonoBehaviour
{
    
    public GameStage        currentGameStage;
    private float           timer = 0;
    public Map              map;
    public UIController     uiController;
    public InputController  inputController;
    public GameData         gameData;
    public AIOpponent       aiOpponent;
    public ChampionShop     championShop;

    // 准备时间
    public int preparationStageDuration = 16;
    // 战斗时间
    public int combatStateDuration = 60;
    // 每回合 必获得的金币数量
    public int baseGoldIncome = 5;


    [HideInInspector]
    public GameObject[] ownChampionInventoryArray;
    [HideInInspector]
    public GameObject[] oponentChampionInventoryArray;
    [HideInInspector]
    public GameObject[,] gridChampionsArray;


    [HideInInspector]
    public int currentChampionLimit = 3;
    [HideInInspector]
    public int currentChampionCount = 0;
    [HideInInspector]
    public int currentGold = 5;
    [HideInInspector]
    public int currentHP = 100;
    [HideInInspector]
    public int timerDisplay = 0;


    private GameObject draggedChampion = null;
    private TriggerInfo dragStartTrigger = null;

    public Dictionary<ChampionType, int> championTypeCount;
    public List<ChampionBonus> activeBonusList;

    // Start is called before the first frame update
    void Start()
    {   // 当前为准备阶段
        currentGameStage = GameStage.Preparation;

        ownChampionInventoryArray = new GameObject[Map.invetorysize];
        oponentChampionInventoryArray = new GameObject[Map.invetorysize];
        gridChampionsArray = new GameObject[Map.hexMapSizeX, Map.hexMapSizeZ / 2];

        uiController.UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGameStage == GameStage.Preparation)
        {
            timer += Time.deltaTime;
            timerDisplay = (int)(preparationStageDuration - timer);

            uiController.UpdateTimerText();

            if(timer > preparationStageDuration)
            {
                timer = 0;
                OnGameStageComplete();
            }
        }else if(currentGameStage == GameStage.Combat)
        {
            timer += Time.deltaTime;
            timerDisplay = (int)timer;
            if(timer > combatStateDuration)
            {
                timer = 0;
                OnGameStageComplete();
            }
        }
    }

    void OnGameStageComplete()
    {
        // 设置 游戏当前阶段已经结束
        aiOpponent.OnGameStageComplete(currentGameStage);

        if(currentGameStage == GameStage.Preparation)
        {
            currentGameStage = GameStage.Combat;
            map.HideIndicators();
            uiController.SetTimerTextActive(false);
            // 取消拖拽的英雄
            if (draggedChampion != null)
            {   
                draggedChampion.GetComponent<ChampionController>().IsDragged = false;
                draggedChampion = null;
            }

            // 没有英雄 直接结束当前回合
            if (IsAllChampionDead())
                EndRound();

            for (int i = 0; i < ownChampionInventoryArray.Length; i++)
            {
                if (ownChampionInventoryArray[i] != null)
                {
                    ChampionController championController = ownChampionInventoryArray[i].GetComponent<ChampionController>();
                    championController.OnCombatStart();
                }
            }

            for (int x = 0; x < Map.hexMapSizeX; x++)
            {
                for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
                {
                    //there is a champion
                    if (gridChampionsArray[x, z] != null)
                    {
                        //get character
                        ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();

                        //start combat
                        championController.OnCombatStart();
                    }

                }
            }
        }else if(currentGameStage == GameStage.Combat)
        {
            currentGameStage = GameStage.Preparation;
            uiController.SetTimerTextActive(true);

            ResetChampions();

            for(int i = 0; i < gameData.championsArray.Length; i++)
            {
                TryUpgradeChampion(gameData.championsArray[i]);
            }

            currentGold += baseGoldIncome;

            uiController.UpdateUI();

            if(currentHP <= 0)
            {
                currentGameStage = GameStage.Loss;
                uiController.ShowLossScreen();
            }
        }
        

        
    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        uiController.UpdateUI();
    }
     
    private bool IsAllChampionDead()
    {
        int championCount = 0;
        int championDead = 0;
        //start own champion combat
        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                //there is a champion
                if (gridChampionsArray[x, z] != null)
                {
                    //get character
                    ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();


                    championCount++;

                    if (championController.isDead)
                        championDead++;

                }

            }
        }

        if (championDead == championCount)
            return true;

        return false;
    }
    public void EndRound()
    {
        timer = combatStateDuration - 3;
    }

    private void ResetChampions()
    {
        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                //there is a champion
                if (gridChampionsArray[x, z] != null)
                {
                    //get character
                    ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();

                    //reset
                    championController.Reset();
                }

            }
        }
    }

    private void TryUpgradeChampion(Champion champion)
    {
        //check for champion upgrade
        List<ChampionController> championList_lvl_1 = new List<ChampionController>();
        List<ChampionController> championList_lvl_2 = new List<ChampionController>();
        // 将英雄栏里的 相同类型英雄 根据等级添加到相应的 list 里面
        for (int i = 0; i < ownChampionInventoryArray.Length; i++)
        {
            //there is a champion
            if (ownChampionInventoryArray[i] != null)
            {
                //get character
                ChampionController championController = ownChampionInventoryArray[i].GetComponent<ChampionController>();

                //check if is the same type of champion that we are buying
                if (championController.champion == champion)
                {
                    if (championController.lvl == 1)
                        championList_lvl_1.Add(championController);
                    else if (championController.lvl == 2)
                        championList_lvl_2.Add(championController);
                }
            }

        }
        // 保存map里面的英雄
        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                //there is a champion
                if (gridChampionsArray[x, z] != null)
                {
                    //get character
                    ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();

                    //check if is the same type of champion that we are buying
                    if (championController.champion == champion)
                    {
                        if (championController.lvl == 1)
                            championList_lvl_1.Add(championController);
                        else if (championController.lvl == 2)
                            championList_lvl_2.Add(championController);
                    }
                }

            }
        }

        //有三个相同类型的英雄就升级
        if (championList_lvl_1.Count > 2)
        {
            //upgrade
            championList_lvl_1[2].UpgradeLevel();

            //remove from array
            RemoveChampionFromArray(championList_lvl_1[0].gridType, championList_lvl_1[0].gridPositionX, championList_lvl_1[0].gridPositionZ);
            RemoveChampionFromArray(championList_lvl_1[1].gridType, championList_lvl_1[1].gridPositionX, championList_lvl_1[1].gridPositionZ);

            //destroy gameobjects
            Destroy(championList_lvl_1[0].gameObject);
            Destroy(championList_lvl_1[1].gameObject);

            //we upgrade to lvl 3
            if (championList_lvl_2.Count > 1)
            {
                //upgrade
                championList_lvl_1[2].UpgradeLevel();

                //remove from array
                RemoveChampionFromArray(championList_lvl_2[0].gridType, championList_lvl_2[0].gridPositionX, championList_lvl_2[0].gridPositionZ);
                RemoveChampionFromArray(championList_lvl_2[1].gridType, championList_lvl_2[1].gridPositionX, championList_lvl_2[1].gridPositionZ);

                //destroy gameobjects
                Destroy(championList_lvl_2[0].gameObject);
                Destroy(championList_lvl_2[1].gameObject);
            }
        }



        currentChampionCount = GetChampionCountOnHexGrid();

        //update ui
        uiController.UpdateUI();

    }

    private void RemoveChampionFromArray(int type,int x,int z)
    {
        if (type == Map.GRID_TYPE_OWN_INVENTORY) ownChampionInventoryArray[x] = null;
        if (type == Map.GRID_TYPE_HEXA_MAP) gridChampionsArray[x, z] = null;
    }

    private int GetChampionCountOnHexGrid()
    {
        int count = 0;
        for(int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ/2; z++)
            {
                if (gridChampionsArray[x, z] != null) count++;
            }
        }
        return count;
    }

    public bool BuyChampionFromShop(Champion champion)
    {
        //Debug.Log("BuyChampionFromShop");
        int emptyIndex = -1; 
        // 获取英雄栏上的 空槽位置的索引
        for(int i = 0; i < ownChampionInventoryArray.Length; i++)
        {
            if (ownChampionInventoryArray[i] == null)
            {
                emptyIndex = i;
                break;
            }
        }
        if (emptyIndex == -1 || currentGold < champion.cost) return false;

        GameObject championPrefab = Instantiate(champion.prefab);
        ChampionController championController = championPrefab.GetComponent<ChampionController>();
        championController.Init(champion, ChampionController.TEAMID_PLAYER);

        championController.SetGridPosition(Map.GRID_TYPE_OWN_INVENTORY, emptyIndex, -1);

        championController.SetWorldPosition();
        championController.SetWorldRotation();

        StoreChampionInArray(Map.GRID_TYPE_OWN_INVENTORY, map.ownTriggerArray[emptyIndex].gridX, -1, championPrefab);

        if (currentGameStage == GameStage.Preparation) TryUpgradeChampion(champion);

        currentGold -= champion.cost;
        uiController.UpdateUI();

        return true;
    }

    public void StartDrag()
    {
        //Debug.Log("Drag ---------------");
        if (currentGameStage != GameStage.Preparation) return;

        TriggerInfo triggerInfo = inputController.triggerInfo;

        if (triggerInfo != null)
        {
            dragStartTrigger = triggerInfo;
            GameObject championGO = GetChampionFromTriggerInfo(triggerInfo);
            if(championGO != null)
            {
                map.ShowIndicators();
                draggedChampion = championGO;
                championGO.GetComponent<ChampionController>().IsDragged = true;
            }
        }
       
    }

    public void StopDrag()
    {
        map.HideIndicators();

        int championsOnField = GetChampionCountOnHexGrid();

        if(draggedChampion != null)
        {
            draggedChampion.GetComponent<ChampionController>().IsDragged = false;
            TriggerInfo triggerInfo = inputController.triggerInfo;

            if (triggerInfo != null)
            {
                GameObject currentTriggerChampion = GetChampionFromTriggerInfo(triggerInfo);
                if(currentTriggerChampion != null)
                {
                    // 保存 champion 的开始位置
                    StoreChampionInArray(dragStartTrigger.gridType, dragStartTrigger.gridX, dragStartTrigger.gridZ, currentTriggerChampion);
                    // 保存 champion 的拖拽位置
                    StoreChampionInArray(triggerInfo.gridType, triggerInfo.gridX, triggerInfo.gridZ, draggedChampion);

                }
                else
                {
                    if (triggerInfo.gridType == Map.GRID_TYPE_HEXA_MAP)
                    {
                        if (championsOnField < currentChampionLimit || dragStartTrigger.gridType == Map.GRID_TYPE_HEXA_MAP)
                        {
                            RemoveChampionFromArray(dragStartTrigger.gridType, dragStartTrigger.gridX, dragStartTrigger.gridZ);
                            StoreChampionInArray(triggerInfo.gridType, triggerInfo.gridX, triggerInfo.gridZ, draggedChampion);
                            if (dragStartTrigger.gridType != Map.GRID_TYPE_HEXA_MAP)
                                championsOnField++;
                        }
                    }
                    else if (triggerInfo.gridType == Map.GRID_TYPE_OWN_INVENTORY)
                    {
                        RemoveChampionFromArray(dragStartTrigger.gridType, dragStartTrigger.gridX, dragStartTrigger.gridZ);
                        StoreChampionInArray(triggerInfo.gridType, triggerInfo.gridX, triggerInfo.gridZ, draggedChampion);
                        if (dragStartTrigger.gridType == Map.GRID_TYPE_HEXA_MAP)
                            championsOnField--;
                    }
                }
                
            }
            CalculateBonuses();
            currentChampionCount = GetChampionCountOnHexGrid();
            uiController.UpdateUI();
            draggedChampion = null;
        }
            

        
    }

    private GameObject GetChampionFromTriggerInfo(TriggerInfo triggerinfo)
    {
        GameObject championGO = null;

        if (triggerinfo.gridType == Map.GRID_TYPE_OWN_INVENTORY)
        {
            championGO = ownChampionInventoryArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == Map.GRID_TYPE_OPENENT_INVENTORY)
        {
            championGO = oponentChampionInventoryArray[triggerinfo.gridX];
        }
        else if (triggerinfo.gridType == Map.GRID_TYPE_HEXA_MAP)
        {
            championGO = gridChampionsArray[triggerinfo.gridX, triggerinfo.gridZ];
        }

        return championGO;
    }

    private void StoreChampionInArray(int gridType, int gridX, int gridZ, GameObject champion)
    {
        //assign current trigger to champion
        ChampionController championController = champion.GetComponent<ChampionController>();
        championController.SetGridPosition(gridType, gridX, gridZ);

        if (gridType == Map.GRID_TYPE_OWN_INVENTORY)
        {
            ownChampionInventoryArray[gridX] = champion;
        }
        else if (gridType == Map.GRID_TYPE_HEXA_MAP)
        {
            gridChampionsArray[gridX, gridZ] = champion;
        }
    }

    private void CalculateBonuses()
    {
        championTypeCount = new Dictionary<ChampionType, int>();

        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                if(gridChampionsArray[x,z] != null)
                {
                    Champion c = gridChampionsArray[x, z].GetComponent<ChampionController>().champion;
                    if (championTypeCount.ContainsKey(c.type1))
                    {
                        int count = 0;
                        championTypeCount.TryGetValue(c.type1, out count);
                        count++;
                        championTypeCount[c.type1] = count;
                    }
                    else championTypeCount.Add(c.type1, 1);

                    if (championTypeCount.ContainsKey(c.type2))
                    {
                        int cCount = 0;
                        championTypeCount.TryGetValue(c.type2, out cCount);

                        cCount++;

                        championTypeCount[c.type2] = cCount;
                    }
                    else
                    {
                        championTypeCount.Add(c.type2, 1);
                    }
                }
            }
        }

        activeBonusList = new List<ChampionBonus>();

        foreach (KeyValuePair<ChampionType, int> m in championTypeCount)
        {
            ChampionBonus championBonus = m.Key.championBonus;

            //have enough champions to get bonus
            if (m.Value >= championBonus.championCount)
            {
                activeBonusList.Add(championBonus);
            }
        }
    }

    public void OnChampionDeadth()
    {
        bool allDead = IsAllChampionDead();

        if (allDead)
            EndRound();
    }
}
