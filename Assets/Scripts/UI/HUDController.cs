using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text goalText;
    [SerializeField] private TMP_Text timeText;

    private int goals = 0;

    private void Start()
    {
        UpdateGoalUI();
        UpdateTimeUI(0f);
    }


    public void AddGoal(int amount = 1)
    {
        goals += amount;
        UpdateGoalUI();
    }


    public void SetTimeRemaining(float seconds)
    {
        UpdateTimeUI(seconds);
    }

    
    public int GetGoals() { return goals; }


    public void ResetHUD()
    {
        goals = 0;
        UpdateGoalUI();
        UpdateTimeUI(0f);
    }


    private void UpdateGoalUI()
    {
        if (goalText != null) goalText.text = $"Goals: {goals}";
    }

    
    private void UpdateTimeUI(float seconds)
    {
        if(timeText == null) return;

        if(seconds < 0f) seconds = 0f;

        // Converte i secondi (float) in minuti e secondi interi
        int total = Mathf.FloorToInt(seconds);
        int minutes = total / 60;
        int secs = total % 60;

        timeText.text = $"Time: {minutes:00}:{secs:00}";
    }

}
