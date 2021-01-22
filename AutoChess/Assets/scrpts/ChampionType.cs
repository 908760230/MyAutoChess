using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DefaultChampionType",menuName = "AutoChess/ChampionType",order =2)]
public class ChampionType : ScriptableObject
{
    // 显示在 UI上的名字
    public string displayName = "name";
    // 显示在 UI上的图标
    public Sprite icon;
    // 英雄类型所具备的奖励效果
    public ChampionBonus championBonus;
}
