using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Slider enemyKillCountSlider;
    [SerializeField] private TempSpawner _tempSpawner;

    private void OnEnable()
    {
        // Подписываемся на событие изменения enemyKillCount
        _tempSpawner.OnEnemyKillCountChanged += UpdateSlider;
    }

    private void OnDisable()
    {
        // Отписываемся от события при отключении объекта
        _tempSpawner.OnEnemyKillCountChanged -= UpdateSlider;
    }

    private void UpdateSlider(int currentCount, int maxCount)
    {
        if (enemyKillCountSlider != null)
        {
            float fillValue = (float)currentCount / maxCount;
            enemyKillCountSlider.value = fillValue;
        }
    }
}
