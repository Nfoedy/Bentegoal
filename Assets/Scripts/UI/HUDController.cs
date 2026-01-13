using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEditor;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text goalText;
    [SerializeField] private TMP_Text timeText;

    private int goals = 0;
    private float elapsedSeconds = 0f;

    private void Start()
    {
        UpdateGoalUI();
        UpdateTimeUI(0f);
    }

    private void Update()
    {
        // tempo che cresce
        elapsedSeconds += Time.deltaTime;
        UpdateTimeUI(elapsedSeconds);
    }


    public void AddGoal(int amount = 1)
    {
        goals += amount;
        UpdateGoalUI();
    }



    private void UpdateGoalUI()
    {
        if (goalText != null)
        {
            goalText.text = $"Gols: {goals}";
        }
    }


    private void UpdateTimeUI(float seconds)
    {
        if (timeText == null) return;

        int total = Mathf.FloorToInt(seconds);
        int minutes = total / 60;
        int secs = total % 60;
        timeText.text = $"Time: {minutes:00}:{secs:00}";
    }
}
