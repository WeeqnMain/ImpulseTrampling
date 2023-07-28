using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] TextMeshProUGUI scoreLabel;

    public void IncreaseScore(int value = 1)
    {
        score += value;
        scoreLabel.text = score.ToString();
    }
}
