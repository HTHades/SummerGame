using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponInventory : MonoBehaviour
{
    [SerializeField] public List<Weapon> UpgradeableActiveWeapons = new();
    [SerializeField] public List<Weapon> InactiveWeapons = new();
    [SerializeField] private List<Weapon> UpgradeableWeapons = new();
    public List<Weapon> MaxLevelWeapons= new();
    private void Start()
    {
        if (InactiveWeapons.Count == 0)
        {
            Debug.LogWarning("PlayerWeaponInventory không có vũ khí");
            return;
        }
        int randomIndex = Random.Range(0, InactiveWeapons.Count);
        GetRandomStartingWeapon(randomIndex);
    }
    public void ActivateWeapon(Weapon weapon)
    {
        weapon.gameObject.SetActive(true);
        UpgradeableActiveWeapons.Add(weapon);
        InactiveWeapons.Remove(weapon);
    }
    public void GetRandomStartingWeapon(int index)
    {
        UpgradeableActiveWeapons.Add(InactiveWeapons[index]);
        InactiveWeapons[index].gameObject.SetActive(true);
        InactiveWeapons.RemoveAt(index);
    }
    public List<Weapon> GetUpgradeableWeapons()
    {
        UpgradeableWeapons.Clear();
        if( UpgradeableActiveWeapons.Count > 0)
        {
            UpgradeableWeapons.AddRange(UpgradeableActiveWeapons);
        }
        if( InactiveWeapons.Count > 0)
        {
            UpgradeableWeapons.AddRange(InactiveWeapons);
        }
        return UpgradeableWeapons;
    }
    public void UpgradeWeapon(Weapon weapon) // chỉ thực hiện tăng cấp
    {
        if( weapon == null)
        {
            return;
        }

        if( !UpgradeableActiveWeapons.Contains(weapon))
        {
            return;
        }
        if( weapon.TryLevelUp() == false)
        {
            return;
        }
        if( weapon.IsMaxLevel)
        {
            UpgradeableActiveWeapons.Remove(weapon); // để phục việc công việc của GetUpgradeableWeapons();
            if(!MaxLevelWeapons.Contains(weapon))
            {
                MaxLevelWeapons.Add(weapon);
            }
        }
    }
    public void ApplyUpgrade(Weapon weapon) // phân loại yêu cầu
    {
        if (weapon == null)
        {
            return;
        }

        if (UpgradeableActiveWeapons.Contains(weapon))
        {
            UpgradeWeapon(weapon);
        }
        else if (InactiveWeapons.Contains(weapon))
        {
            ActivateWeapon(weapon);
        }
    }
   
}
