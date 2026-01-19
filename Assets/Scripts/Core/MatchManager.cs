using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [Header("Match Settings")]
    [SerializeField] private float matchDurationSeconds = 90; 

    [Header("References")]
    [SerializeField] private HUDController hud;
    [SerializeField] private EndGameController endGameController;

    [Header("Audio")]
    [SerializeField] private AudioClip matchStartSound;


    private float timeRemaining;
    private bool matchEnded = false;

    private AudioSource audioSource;

    private void Start()
    {
        Time.timeScale = 1f;
        timeRemaining = matchDurationSeconds;

        if(hud != null) hud.SetTimeRemaining(timeRemaining);

        audioSource = GetComponent<AudioSource>();
        if (audioSource != null ) audioSource.PlayOneShot(matchStartSound);
        
    }

    private void Update()
    {
        if (matchEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndMatch();
        }

        if (hud != null) hud.SetTimeRemaining(timeRemaining);
        
    }

    private void EndMatch()
    {
        matchEnded = true;

        // condizione ? Valore se vero : Valore se falso 
        int finalGoals = (hud != null) ? hud.GetGoals() : 0;

        if (endGameController != null) endGameController.ShowEndGame(finalGoals);
        else Debug.LogWarning("EndGameController not assigned in MatchManager.");

    }

    public bool IsMatchEnded() { return matchEnded; }

}
