using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{

    [Header("UI References")]
    [SerializeField] private GameObject endPanel;
    [SerializeField] private TMP_Text finalGoalText;
    [SerializeField] private GameObject hudPanel;

    [Header("Scene")]
    [SerializeField] private string gameSceneName = "MainScene";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Audio")]
    [SerializeField] private AudioClip matchEndSound;


    private AudioSource audioSource;


    private void Awake()
    {
        endPanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }


    public void ShowEndGame(int goals)
    {
        if (hudPanel != null) hudPanel.SetActive(false);
        endPanel.SetActive(true);
        if (audioSource != null) audioSource.PlayOneShot(matchEndSound);
        Time.timeScale = 0f; // Pausa il gioco
        if (finalGoalText != null) finalGoalText.text = $"Goals : {goals}";
    }


    public void Replay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameSceneName);
    }


    public void ExitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
