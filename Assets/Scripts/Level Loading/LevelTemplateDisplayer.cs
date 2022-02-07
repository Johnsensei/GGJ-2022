using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTemplateDisplayer : MonoBehaviour
{
    public Image PreviewImage;
    public TextMeshProUGUI LevelName;
    public LevelScoreDisplayer ScoreDisplayer;
    public Button LoadLevelButton;

    public void LoadLevelTemplate(LevelScriptableObject templateData)
    {
        if (templateData == null)
            return;
         
        if (templateData.Image != null)
            PreviewImage.sprite = templateData.Image;
        
        LevelName.text = templateData.Name;
        ScoreDisplayer.SetScore(templateData.Score, LevelScriptableObject.MaxScore);
        LoadLevelButton.onClick.AddListener(() => SceneLoader.LoadScene(templateData.BuildIndex));
    }
}
