using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class ChampionShop : MonoBehaviour
{
    public UIController uiController;
    public GamePlayController gamePlayController;
    public GameData gameData;

    // 存储能够被购买的英雄
    private Champion[] availableChampionArray;
    // Start is called before the first frame update
    void Start()
    {
        RefreshShop(true);
    }

    public void RefreshShop(bool isFree)
    {
        if (gamePlayController.currentGold < 2 && isFree == false) return;

        availableChampionArray = new Champion[5];

        for(int i = 0; i < availableChampionArray.Length; i++)
        {
            Champion champion = GetRandomChampionInfo();
            availableChampionArray[i] = champion;

            uiController.LoadShopItem(champion, i);

            uiController.ShowShopItems();
        }

    }
    public Champion GetRandomChampionInfo()
    {   // 从英雄数组里面随机选一个
        //randomise a number
        int rand = Random.Range(0, gameData.championsArray.Length);

        //return from array
        return gameData.championsArray[rand];
    }
    // 点击在 单个英雄框上
    public void OnChampionFrameClicked(int index)
    {
        bool isSuccess = gamePlayController.BuyChampionFromShop(availableChampionArray[index]);

        if (isSuccess) uiController.HideChampionFrame(index);
    }

}
