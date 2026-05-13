using UnityEngine;

public class GoalTrigger: MonoBehaviour
{
    [SerializeField] private int pointsPerGoal = 1;
    [SerializeField] private HUDController hud;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Ball")) return;

        var state = other.GetComponent<BallGoalState>();
        if (state != null && state.HasScored) return;    
        
        if (state != null) state.MarkScored();

        if(hud != null) hud.AddGoal(pointsPerGoal);

    }
}
