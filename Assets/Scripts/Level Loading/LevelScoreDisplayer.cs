using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScoreDisplayer : MonoBehaviour
{
    [SerializeField]
    private List<Image> ScoreDisplayers;
   
    public void SetScore(float value, float maxValue)
    {
        if (ScoreDisplayers == null || ScoreDisplayers.Count == 0)
            return;

        foreach (var image in ScoreDisplayers)
            image.fillAmount = 0;

        var displayerAmount = ScoreDisplayers.Count;
        var scoreProportion = value / maxValue;
        var scoreToShow = scoreProportion * displayerAmount;
        var filledDisplayers = Mathf.FloorToInt(scoreToShow);
        var lastDisplayerValue = scoreToShow - filledDisplayers;

        var displayerIndex = filledDisplayers;
        while(displayerIndex > 0)
        {
            ScoreDisplayers[ScoreDisplayers.Count - displayerIndex--].fillAmount = 1;
        }

        if(filledDisplayers < ScoreDisplayers.Count)
        {
            ScoreDisplayers[filledDisplayers].fillAmount = lastDisplayerValue;
        }
    }
}
