using UnityEngine;
using UnityEngine.UI;

public class MuteButtonUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image iconImage;

    [Header("Sprites")]
    [SerializeField] private Sprite speakerOnSprite;
    [SerializeField] private Sprite speakerOffSprite;

    private const string PREF_KEY = "BENTEGOAL_MUTED";
    private bool isMuted;

    private void Awake()
    {
        isMuted = PlayerPrefs.GetInt(PREF_KEY, 0) == 1;
        ApplyMute();
        RefreshIcon();
    }

    public void ToggleMute()
    {
        isMuted = !isMuted;
        PlayerPrefs.SetInt(PREF_KEY, isMuted ? 1 : 0);
        PlayerPrefs.Save();

        ApplyMute();
        RefreshIcon();
    }

    private void ApplyMute()
    {
        AudioListener.volume = isMuted ? 0f : 1f;
    }

    private void RefreshIcon()
    {
        if (iconImage == null) return;

        iconImage.sprite = isMuted ? speakerOffSprite : speakerOnSprite;
        iconImage.enabled = true; // assicura che sia visibile
    }
}