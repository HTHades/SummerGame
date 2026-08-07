using System;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private int weaponLevel;
    private List<WeaponStats> Stats;
    private Sprite weaponImage;
    private string basicDescription;
    public Sprite WeaponImage
    {
        get
        {
            return weaponImage;
        }
    }
    public String BasicDescription
    {
        get
        {
            return basicDescription;
        }
    }
    public WeaponStats CurrentStats
    {
        get
        {
            return Stats[weaponLevel];
        }
    }
    public String CurrentDescription
    {
        get
        {
           return CurrentStats.WeaponDescriptionLevel;
        }
    }
    public bool IsMaxLevel
    {
        get
        {
            return weaponLevel >= Stats.Count - 1;
        }
    }
    public bool TryLevelUp()
    {
        if(weaponLevel < Stats.Count -1)
        {   
            weaponLevel++;
            return true;
        }
        return false;
    }
}

[System.Serializable]
public class WeaponStats
{
    public float cooldown;
    public float duration;
    public float Damage;
    public float range;
    public float Speed;
    public int amount;
    public string WeaponDescriptionLevel;
}
