using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int score;
    [SerializeField] TextMeshProUGUI scoreLabel;

    private void IncreaseScore(int value = 1)
    {
        score += value;
        scoreLabel.text = score.ToString();
    }

    public void OnEnemyDestroyed(EnemyController enemy)
    {
        enemy.Destroyed -= OnEnemyDestroyed;
        IncreaseScore();
    }
}
