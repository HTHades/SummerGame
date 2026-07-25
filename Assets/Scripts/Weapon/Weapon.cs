using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStats> Stats;
    public Sprite weaponImage;
    public void LevelUp()
    {
        if(weaponLevel < Stats.Count -1)
        {
            weaponLevel++;
        }
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
    public string WeaponDescription;
}
