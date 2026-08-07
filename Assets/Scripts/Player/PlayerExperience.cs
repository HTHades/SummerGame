using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
   [SerializeField] public List<int> levels;
   public int CurrentExperience{get; private set;}
   public int currentLevel{ get; private set;}
   [SerializeField] private int maxLevel;
   [SerializeField] private LevelUpController levelUpController;
   
   void Start()
    {
        for(int i = levels.Count; i < maxLevel; i++)
        {
            //levels.Add(Mathf.CeilToInt((levels[levels.Count - 1])* 1.5f + 30));
            levels.Add(Mathf.CeilToInt( levels[levels.Count - 1]+ 30));
        }
        UIController.Instance.UpdateExpSlider(CurrentExperience, levels[currentLevel]);
    }
     public void AddExperience(int amount)
    {
        if( currentLevel >= levels.Count)
        {
           return;
        }
        CurrentExperience += amount;
        UIController.Instance.UpdateExpSlider(CurrentExperience, levels[currentLevel]);
        if( CurrentExperience >= levels[currentLevel])
        {
            LevelUp();
        }
    }
    private void LevelUp()
    {
        CurrentExperience -= levels[currentLevel];
        currentLevel++;

        levelUpController.OpenLevelUp();
        if (currentLevel < levels.Count)
        {
            UIController.Instance.UpdateExpSlider(CurrentExperience, levels[currentLevel]);
        }
    }
   
}
