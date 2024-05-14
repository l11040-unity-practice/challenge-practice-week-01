using System;
using System.Collections.Generic;
using UnityEngine;

public class AchievementView : MonoBehaviour
{
    [SerializeField] private GameObject achievementSlotPrefab;  // 업적 슬롯 프리팹
    private Dictionary<int, AchievementSlot> achievementSlots = new();

    public void CreateAchievementSlots(AchievementSO[] achievements)
    {
        for (int i = 0; i < achievements.Length; i++)
        {
            AchievementSlot slot = Instantiate(achievementSlotPrefab, this.gameObject.transform).GetComponent<AchievementSlot>();
            slot.Init(achievements[i]);
            achievementSlots.Add(achievements[i].threshold, slot);
        }
    }

    public void UnlockAchievement(int threshold)
    {
        // UI 반영 로직
        achievementSlots.TryGetValue(threshold, out AchievementSlot slot);
        if (slot != null)
        {
            slot.MarkAsChecked();
        }
    }
}