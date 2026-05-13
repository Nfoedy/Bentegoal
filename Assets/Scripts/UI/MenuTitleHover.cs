using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class MenuTitleHover : MonoBehaviour
{
    [Header("Colors")]
    [SerializeField] private Color32 normalColor = new Color32(255, 255, 255, 255);
    [SerializeField] private Color32 hoverColor = new Color32(255, 235, 0, 255);

    private TMP_Text textComponent;
    private Canvas parentCanvas;
    private Camera uiCamera; // null se Overlay
    private int lastCharIndex = -1;

    private void Awake()
    {
        textComponent = GetComponent<TMP_Text>();
        parentCanvas = GetComponentInParent<Canvas>();

        // Scegli la camera corretta in base al Render Mode
        if (parentCanvas != null && parentCanvas.renderMode != RenderMode.ScreenSpaceOverlay)
            uiCamera = parentCanvas.worldCamera;  // Screen Space - Camera / World Space
        else
            uiCamera = null;                      // Screen Space - Overlay

        textComponent.ForceMeshUpdate();
    }

    private void Update()
    {
        int charIndex = TMP_TextUtilities.FindIntersectingCharacter(
            textComponent,
            Input.mousePosition,
            uiCamera,
            true
        );

        // Debug veloce (puoi commentarlo dopo)
        // Debug.Log("charIndex: " + charIndex);

        if (charIndex == lastCharIndex) return;

        if (lastCharIndex != -1) SetCharacterColor(lastCharIndex, normalColor);
        if (charIndex != -1) SetCharacterColor(charIndex, hoverColor);

        lastCharIndex = charIndex;
    }

    private void SetCharacterColor(int index, Color32 color)
    {
        textComponent.ForceMeshUpdate();
        TMP_TextInfo textInfo = textComponent.textInfo;

        if (index < 0 || index >= textInfo.characterCount) return;

        TMP_CharacterInfo charInfo = textInfo.characterInfo[index];
        if (!charInfo.isVisible) return;

        int materialIndex = charInfo.materialReferenceIndex;
        int vertexIndex = charInfo.vertexIndex;

        Color32[] vertexColors = textInfo.meshInfo[materialIndex].colors32;
        vertexColors[vertexIndex + 0] = color;
        vertexColors[vertexIndex + 1] = color;
        vertexColors[vertexIndex + 2] = color;
        vertexColors[vertexIndex + 3] = color;

        textComponent.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }
}