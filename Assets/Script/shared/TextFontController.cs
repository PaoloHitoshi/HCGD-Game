using UnityEngine;
using TMPro;

public class TextFontController : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset cursive_font;
    [SerializeField] private TMP_FontAsset stick_font;

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

        TMP_FontAsset chosenFont = cursive ? cursive_font : stick_font;
        
        foreach(var textMesh in textMeshes)
        {
            textMesh.font = chosenFont;
        }
    }

}
