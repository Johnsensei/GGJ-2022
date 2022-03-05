using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDataLoader : MonoBehaviour
{
    [SerializeField]
    private List<LevelScriptableObject> Levels;
    [SerializeField]
    private LevelTemplateDisplayer LevelTemplatePrefab;
    public float MarginRight = 20;

    private void Start()
    {
        LoadData();
    }

    void LoadData()
    {
        if (Levels == null)
            return;

        var levelDataWidth = LevelTemplatePrefab.GetComponent<RectTransform>().rect.width;

        for (int i = 0; i < Levels.Count; i++)
        {
            var levelData = Levels[i];
            var levelTemplate = Instantiate(LevelTemplatePrefab, transform);
            levelTemplate.LoadLevelTemplate(levelData);
            var offset = i * (levelDataWidth + MarginRight) + levelDataWidth / 2;
            levelTemplate.GetComponent<RectTransform>().position += Vector3.right * offset;
        }

        var allLevelTemplatesWidth = (levelDataWidth + MarginRight) * Levels.Count;
        var rectTransform = GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();
        var currentWidth = rectTransform.rect.width;
        GetComponent<RectTransform>().offsetMax += Vector2.right * (allLevelTemplatesWidth - currentWidth);
    }
}
