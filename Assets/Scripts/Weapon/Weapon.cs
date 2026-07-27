using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int weaponLevel;
    public List<WeaponStats> Stats;
    public Sprite weaponImage;
    public string basicDescription;
    public void LevelUpWeapon()
    {
        if(weaponLevel < Stats.Count -1)
        {
            weaponLevel++;
            if(weaponLevel >= Stats.Count -1)
            {
                PlayerController.Instance.MaxLevelWeapons.Add(this);
                PlayerController.Instance.ActiveWeapons.Remove(this);
            }
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
    public float amount;
    public string WeaponDescription;
}
