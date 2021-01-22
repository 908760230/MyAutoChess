using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public ChampionShop championShop;
    public GamePlayController gamePlayController;

    public GameObject[] championsFrameArray;
    public GameObject[] bonusPanels;


    public Text timerText;
    public Text championCountText;
    public Text goldText;
    public Text hpText;

    public GameObject shop;
    public GameObject restartButton;
    public GameObject placementText;
    public GameObject gold;
    public GameObject bonusContainer;
    public GameObject bonusUIPrefab;

    public void OnChampionClicked()
    {
        //Debug.Log("OnChampionClicked");
        // 获取 ui的名字
        string name = EventSystem.current.currentSelectedGameObject.transform.parent.name;
        string defaultName = "champion container ";
        int championFrameIndex = int.Parse(name.Substring(defaultName.Length, 1));
        //championShop
        championShop.OnChampionFrameClicked(championFrameIndex);
    }

    public void RefreshButtonClicked()
    {

    }

    public void BuyXPButtonClicked()
    {

    }
    public void RestartButtonClicked()
    {

    }

    public void HideChampionFrame(int index)
    {
        championsFrameArray[index].transform.Find("champion").gameObject.SetActive(false);
    }

    public void ShowShopItems()
    {
        for (int i = 0; i < championsFrameArray.Length; i++)
        {
            championsFrameArray[i].transform.Find("champion").gameObject.SetActive(true);
        }
    }
    // 根据索引在UI上展示英雄信息
    public void LoadShopItem(Champion champion, int index)
    {
        Transform championUI = championsFrameArray[index].transform.Find("champion");
        Transform top = championUI.Find("top");
        Transform bottom = championUI.Find("bottom");
        Transform type1 = top.Find("type 1");
        Transform type2 = top.Find("type 2");
        Transform name = bottom.Find("Name");
        Transform cost = bottom.Find("Cost");
        Transform icon1 = top.Find("icon 1");
        Transform icon2 = top.Find("icon 2");

        name.GetComponent<Text>().text = champion.uiname;
        cost.GetComponent<Text>().text = champion.cost.ToString();
        type1.GetComponent<Text>().text = champion.type1.displayName;
        type2.GetComponent<Text>().text = champion.type2.displayName;
        icon1.GetComponent<Image>().sprite = champion.type1.icon;
        icon2.GetComponent<Image>().sprite = champion.type2.icon;
    }

    public void UpdateUI()
    {
        // 当前金钱数量
        goldText.text = gamePlayController.currentGold.ToString();
        // 当前英雄数量
        championCountText.text = gamePlayController.currentChampionCount.ToString() + " / " + gamePlayController.currentChampionLimit.ToString();
        // 当前生命值
        hpText.text = "HP " + gamePlayController.currentHP.ToString();


        //hide bonusus UI  隐藏奖励界面
        foreach (GameObject go in bonusPanels)
        {
            go.SetActive(false);
        }

        if (gamePlayController.championTypeCount != null)
        {
            int i = 0;
            foreach(KeyValuePair<ChampionType,int> m in gamePlayController.championTypeCount)
            {
                GameObject bonusUI = bonusPanels[i];
                bonusUI.transform.SetParent(bonusContainer.transform);
                bonusUI.transform.Find("icon").GetComponent<Image>().sprite = m.Key.icon;
                bonusUI.transform.Find("name").GetComponent<Text>().text = m.Key.displayName;
                bonusUI.transform.Find("count").GetComponent<Text>().text = m.Value.ToString();
                bonusUI.SetActive(true);
                i++;
            }
        }
    }
    public void UpdateTimerText()
    {
        timerText.text = gamePlayController.timerDisplay.ToString();
    }
    public void SetTimerTextActive(bool b)
    {
        timerText.gameObject.SetActive(b);
        placementText.SetActive(b);
    }
    public void ShowLossScreen()
    {

    }
    public void ShowGameScreen()
    {

    }
}
