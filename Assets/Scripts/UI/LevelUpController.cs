using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpController : MonoBehaviour
{
    [SerializeField] private PlayerWeaponInventory playerWeaponInventory;
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private LevelUpButton[] Buttons;
    public void OpenLevelUp()
    {
        List<Weapon> weapons = playerWeaponInventory.GetUpgradeableWeapons();
        for( int i = 0 ; i < Buttons.Length ; i++)
        {
            if(i < weapons.Count)
            {
               Buttons[i].gameObject.SetActive(true);
               Buttons[i].ActivateButton(weapons[i]); 
            }
            else
            {
                Buttons[i].gameObject.SetActive(false);
            }
        }
        UIController.Instance.LevelUpPanelOpen();
        GameManager.Instance.RefreshTimeScale();
    }
    public void SelectWeapon( Weapon weapon)
    {
        playerWeaponInventory.ApplyUpgrade(weapon);  
        FinishSelection();
    }
    public void SelectHealthUpgrade(int amount)
    {
        playerHealth.IncreaseMaxHp(amount);
        FinishSelection();
    }
    private void FinishSelection()
    {
        AudioController.Instance.PlaySound(SoundType.SelectUpgrade);
        UIController.Instance.LevelUpPanelClose();
        GameManager.Instance.RefreshTimeScale();
    }
}
