using UnityEngine;

public class GameStarter : MonoBehaviour
{
    [SerializeField] private bool saveHighscore;
    [SerializeField] private bool isInputMobile;

    [SerializeField] private PlayerController playerPrefab;
    [SerializeField] private EnemySpawner enemySpawnerPrefab;
    [SerializeField] private GameObject UICanvasPrefab;

    private void Awake()
    {
        var canvasObject = Instantiate(UICanvasPrefab);
        var canvas = canvasObject.GetComponent<UICanvas>();
        var scoreManager = canvasObject.GetComponent<ScoreManager>();
        var mobileInput = canvasObject.GetComponent<MobileInput>();

        var player = Instantiate(playerPrefab.gameObject);
        var playerController = player.GetComponent<PlayerController>();

        if (isInputMobile)
            playerController.Init(mobileInput);
        else
            playerController.Init();

        playerController.OnDamageReceive += canvas.PlayerReceivedDamage;
        
        var enemySpawner = Instantiate(enemySpawnerPrefab.gameObject);
        enemySpawner.GetComponent<EnemySpawner>().Init(scoreManager);

        if (PlayerPrefs.HasKey("highscore") == !saveHighscore)
            PlayerPrefs.SetInt("highscore", 0);
    }
}
