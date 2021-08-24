using UnityEngine;
using TMPro;

public class TextFontController : MonoBehaviour
{
    [SerializeField] private string genre;
    
    [SerializeField] private TMP_FontAsset cursive_font;
    [SerializeField] private TMP_FontAsset stick_font;

    private void Start()
    {
        if(CurrentGameSession.TryGetGameOf(genre, out GameDataContainer gameData))
        {
            UpdateFonts(gameData.FontStyle);
        }
    }

    [ContextMenu("Update Font to stick")]
    public void UpdateFontToStick()
    {
        UpdateFonts(false);
    }

    [ContextMenu("Update Font to cursive")]
    public void UpdateFontToCursive()
    {
        UpdateFonts(true);
    }

    public void UpdateFonts(bool cursive)
    {
        TextMeshProUGUI[] textMeshes = FindObjectsOfType<TextMeshProUGUI>();

        TMP_FontAsset chosenFont;
        FontStyles fontStyles;
        if (cursive)
        {
            chosenFont = cursive_font;
            fontStyles = FontStyles.LowerCase;
        }
        else
        {
            chosenFont = stick_font;
            fontStyles = FontStyles.UpperCase;
        }
        
        foreach(var textMesh in textMeshes)
        {
            textMesh.font = chosenFont;
            textMesh.fontStyle = fontStyles;
        }
    }

}
