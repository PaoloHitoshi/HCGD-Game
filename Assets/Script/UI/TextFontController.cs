using UnityEngine;
using TMPro;

public class TextFontController : MonoBehaviour
{
    [SerializeField] private TMP_FontAsset cursive_font;
    [SerializeField] private TMP_FontAsset stick_font;

    [SerializeField] private QuizDataSO quizData;

    private void Start()
    {
        UpdateFonts(quizData.Data.FontStyle);
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
