using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private string mainMenuSceneName = "MainMenu";
    
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused) Resume();
            else Pause();
        }

    }

    private void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;        
    }


    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }


    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuSceneName);
    }

}
