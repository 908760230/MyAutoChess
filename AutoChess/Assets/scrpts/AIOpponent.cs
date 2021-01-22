using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    public ChampionShop championShop;
    public Map map;
    public UIController uIController;
    public GamePlayController gamePlayController;

    public GameObject[,] gridChampionsArray;

    public Dictionary<ChampionType, int> championTypeCount;
    public List<ChampionBonus> activeBonusList;

   
    public int championDamage = 2;  // 输一把 生命值减2

    public void OnMapReady()
    {
        gridChampionsArray = new GameObject[Map.hexMapSizeX, Map.hexMapSizeZ / 2];

        AddRandomChampion();
    }

    public void OnGameStageComplete(GameStage stage)
    {
        if(stage == GameStage.Preparation)
        {   // 开启战斗
            for(int x =0;x < Map.hexMapSizeX; x++)
            {
                for (int z = 0; z < Map.hexMapSizeZ/2; z++)
                {
                    if(gridChampionsArray[x,z] != null)
                    {
                        // 获取 角色
                        ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();
                        championController.OnCombatStart();
                    }
                }
            }
        }else if(stage == GameStage.Combat)
        {
            // 玩家总共受到的伤害 
            int damage = 0;
            // 在战斗的状态下
            for(int x = 0; x < Map.hexMapSizeX; x++)
            {
                for (int z = 0; z < Map.hexMapSizeZ/2; z++)
                {
                    if (gridChampionsArray[x, z] != null)
                    {
                        ChampionController championController = gridChampionsArray[x, z].GetComponent<ChampionController>();
                        // 计算 每个 champion 造成的伤害
                        if (championController.currentHealth > 0)
                            damage += championDamage;
                    }
                }
            }
            gamePlayController.TakeDamage(damage);

        }
    }

    private void GetEmptySlot(out int emptyIndexX, out int emptyIndexZ)
    {
        emptyIndexX = -1;
        emptyIndexZ = -1;

        //get first empty inventory slot
        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                if (gridChampionsArray[x, z] == null)
                {
                    emptyIndexX = x;
                    emptyIndexZ = z;
                    break;
                }
            }
        }
    }
    public void AddRandomChampion()
    {
        // 获取空槽的坐标
        int indexX, indexZ;
        GetEmptySlot(out indexX, out indexZ);

        if (indexX == -1 || indexZ == -1) return;

        Champion champion = championShop.GetRandomChampionInfo();
        GameObject championPrefab = Instantiate(champion.prefab);
        gridChampionsArray[indexX, indexZ] = championPrefab;

        ChampionController championController = championPrefab.GetComponent<ChampionController>();

        championController.Init(champion, ChampionController.TEAMID_AI);
        championController.SetGridPosition(Map.GRID_TYPE_OPENENT_INVENTORY, indexX, indexZ+4);
        //set position and rotation
        championController.SetWorldPosition();
        championController.SetWorldRotation();

        //check for champion upgrade
        List<ChampionController> championList_lvl_1 = new List<ChampionController>();
        List<ChampionController> championList_lvl_2 = new List<ChampionController>();

        for (int x = 0; x < Map.hexMapSizeX; x++)
        {
            for (int z = 0; z < Map.hexMapSizeZ / 2; z++)
            {
                //there is a champion
                if (gridChampionsArray[x, z] != null)
                {
                    //get character
                    ChampionController cc = gridChampionsArray[x, z].GetComponent<ChampionController>();

                    //check if is the same type of champion that we are buying
                    if (cc.champion == champion)
                    {
                        if (cc.lvl == 1)
                            championList_lvl_1.Add(cc);
                        else if (cc.lvl == 2)
                            championList_lvl_2.Add(cc);
                    }
                }

            }
        }

        //if we have 3 we upgrade a champion and delete rest
        if (championList_lvl_1.Count == 3)
        {
            //upgrade
            championList_lvl_1[2].UpgradeLevel();

            //destroy gameobjects
            Destroy(championList_lvl_1[0].gameObject);
            Destroy(championList_lvl_1[1].gameObject);

            //we upgrade to lvl 3
            if (championList_lvl_2.Count == 2)
            {
                //upgrade
                championList_lvl_1[2].UpgradeLevel();

                //destroy gameobjects
                Destroy(championList_lvl_2[0].gameObject);
                Destroy(championList_lvl_2[1].gameObject);
            }
        }
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

                    //set position and rotation
                    championController.Reset();



                }

            }
        }
    }
    public void Restart()
    {

    }
    public void OnChampionDeath()
    {
        bool allDead = IsAllChampionDead();
        if (allDead) gamePlayController.EndRound();
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
    private void CalculateBonuses()
    {

    }

}
