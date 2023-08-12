using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private bool saveHighscore;

    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private EnemySpawner enemySpawnerPrefab;
    [SerializeField] private GameObject UICanvasPrefab;

    private void Awake()
    {
        var canvasObject = Instantiate(UICanvasPrefab);
        var canvas = canvasObject.GetComponent<UICanvas>();
        var scoreManager = canvasObject.GetComponent<ScoreManager>();

        var player = Instantiate(playerPrefab.gameObject);
        var playerController = player.GetComponent<PlayerController>();
        playerController.OnDamageReceive += canvas.PlayerReceivedDamage;
        
        var enemySpawner = Instantiate(enemySpawnerPrefab.gameObject);
        enemySpawner.GetComponent<EnemySpawner>().Init(scoreManager);

        if (PlayerPrefs.HasKey("highscore") == !saveHighscore)
            PlayerPrefs.SetInt("highscore", 0);
    }
}
