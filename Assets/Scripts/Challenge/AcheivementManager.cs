using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public static AchievementManager Instance;
    private int currentThresholdIndex = 0;

    [SerializeField] private AchievementSO[] achievements;
    [SerializeField] private AchievementView achievementView;

    private void Awake()
    {
        Instance = this;
        RocketMovementC.OnHighScoreChanged += CheckAchievement;
    }

    private void Start()
    {
        achievementView.CreateAchievementSlots(achievements);  // UI 생성
    }

    // 최고 높이를 달성했을 때 업적 달성 판단, 이벤트 기반으로 설계할 것
    private void CheckAchievement(float height)
    {
        if (height >= achievements[currentThresholdIndex].threshold)
        {
            currentThresholdIndex++;
            // TODO 업적 추가 실행
        }
    }
}