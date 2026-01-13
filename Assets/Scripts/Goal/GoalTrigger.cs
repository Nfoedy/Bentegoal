using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GoalTrigger: MonoBehaviour
{
    [SerializeField] private int pointsPerGoal = 1;
    [SerializeField] private HUDController hud;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        var state = other.GetComponent<BallGoalState>();
        if (state != null && state.HasScored) return;       // Ha già segnato, non fare nulla
        
        if (state != null)         {
            state.MarkScored();  // Segna che ha segnato
        }


        Debug.Log("Goal scored!");
        hud.AddGoal(1);


    }
}
