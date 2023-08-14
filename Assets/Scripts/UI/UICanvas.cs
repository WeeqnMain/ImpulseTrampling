using UnityEngine;

[RequireComponent(typeof(ScoreManager))]
public class UICanvas : MonoBehaviour
{
    [SerializeField] private DefeatScreen defeatScreen;
    [SerializeField] private ScoreManager scoreManager;

    public void PlayerReceivedDamage()
    {
        scoreManager.SaveScore();
        scoreManager.HideScoreLabel();
        defeatScreen.Show(scoreManager.score, scoreManager.isHighscoreBeaten);
    }
}
