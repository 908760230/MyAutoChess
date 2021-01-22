using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DefaultChampion", menuName = "AutoChess/Champion", order = 1)]
public class Champion : ScriptableObject
{
    public GameObject prefab;
    // 发射物 (子弹)
    ///Projectile prefab to create when champion is attacking
    public GameObject attackProjectile;

    ///The champion name displayed on the UI frames
    public string uiname;

    ///The buy gold cost of the champion from the shop
    public int cost;

    ///The type of the champion
    public ChampionType type1;

    ///The type of the champion
    public ChampionType type2;

    ///The champion character starting health point
    public float health = 100;

    ///The champion character damage done on succesful attack
    public float damage = 10;

    ///The range the champion can start attack from
    public float attackRange = 1;
}
