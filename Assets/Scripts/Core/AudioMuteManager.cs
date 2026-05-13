using UnityEngine;
public class AudioMuteManager : MonoBehaviour
{
    private const string PREF_KEY = "BENTEGOAL_MUTED";

    public static bool IsMuted { get; private set; }

    private void Awake()
    {
        IsMuted = PlayerPrefs.GetInt(PREF_KEY, 0) == 1;
        Apply();
    }

    public static void ToggleMute()
    {
        SetMuted(!IsMuted);
    }

    public static void SetMuted(bool muted)
    {
        IsMuted = muted;
        PlayerPrefs.SetInt(PREF_KEY, muted ? 1 : 0);
        PlayerPrefs.Save();
        Apply();
    }

    private static void Apply()
    {
        AudioListener.volume = IsMuted ? 0f : 1f;
    }
}