using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int minEnemyMoveSpeed;
    [SerializeField] private int maxEnemyMoveSpeed;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float timeToEnemySpawn;

    [SerializeField] private ScoreManager scoreManager;

    private EnemySpawnPerimeter enemySpawnPerimeter;

    [SerializeField] private Transform leftBottomPoint;
    [SerializeField] private Transform leftTopPoint;
    [SerializeField] private Transform rightTopPoint;
    [SerializeField] private Transform rightBottomPoint;

    private void Awake()
    {
        enemySpawnPerimeter = new EnemySpawnPerimeter(leftBottomPoint, leftTopPoint, rightTopPoint, rightBottomPoint);
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        Vector3 enemySpawnPoint = enemySpawnPerimeter.GetRandomSpawnPoint();
        print(enemySpawnPoint);
        Vector3 enemyDirection = new Vector3(0, Random.Range(0, 360), 0);

        var instance = Instantiate(enemyPrefab, enemySpawnPoint, Quaternion.Euler(enemyDirection));

        var enemy = instance.GetComponent<EnemyController>();
        enemy.Init(minEnemyMoveSpeed, maxEnemyMoveSpeed);
        enemy.Destroyed += OnEnemyDestroyed;

        yield return new WaitForSeconds(timeToEnemySpawn);
        StartCoroutine(SpawnEnemy());
    }

    private void OnEnemyDestroyed(EnemyController enemy)
    {
        enemy.Destroyed -= OnEnemyDestroyed;
        scoreManager.IncreaseScore();
    }
}
