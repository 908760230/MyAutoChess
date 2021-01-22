using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChampionBonusType { Damage, Defense, Stun, Heal };
public enum BonusTarget { Self, Enemy };

// 当有足够的 相同类型英雄时，控制获得奖励的数量
[System.Serializable]
public class ChampionBonus
{
    public int championCount = 0;
    // 就是 map上 有几个相同类型的英雄会触发的效果
    // 类型框的类型
    public ChampionBonusType championBonusType;
    // 类型框的对象
    public BonusTarget bonusTarget;

    // 类型框的值
    public float bonusValue = 0;

    //持续时间
    public float duration;
    // 类型框的 特效
    public GameObject effectPrefab;
    //攻击时 计算bonus
    public float ApplyOnAttack(ChampionController champion, ChampionController targetChampion)
    {
        float bonusDamge = 0;
        bool addEffect = false;
        switch (championBonusType)
        {
            case ChampionBonusType.Damage: bonusDamge += bonusValue; break;
            case ChampionBonusType.Stun:
                int rand = Random.Range(0, 100);
                if(rand < bonusValue)
                {
                    targetChampion.OnGotStun(duration);
                    addEffect = true;
                }
                break;
            case ChampionBonusType.Heal:
                champion.OngGotHeal(bonusValue);
                break;
        }

        if (addEffect)
        {
            if (bonusTarget == BonusTarget.Self) champion.AddEffect(effectPrefab, duration);
            else if (bonusTarget == BonusTarget.Enemy) targetChampion.AddEffect(effectPrefab, duration);
        }
        return bonusDamge;
    }
    // 被击中时 计算 bonues
    public float ApplyOnGotHit(ChampionController champion, float damage)
    {
        switch (championBonusType)
        {
            case ChampionBonusType.Defense:
                damage = ((100 - bonusValue) / 100) * damage;
                break;
            default:
                break;
        }
        return damage;
    }
}
